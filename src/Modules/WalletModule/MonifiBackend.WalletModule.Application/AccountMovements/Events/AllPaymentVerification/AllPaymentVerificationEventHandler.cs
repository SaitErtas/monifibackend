using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.BscScans;
using MonifiBackend.Core.Domain.BscScans.Accounts;
using MonifiBackend.Core.Domain.TronNetworks;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;
using MonifiBackend.WalletModule.Domain.Users;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Events.AllPaymentVerification;

internal class AllPaymentVerificationEventHandler : IEventHandler<AllPaymentVerificationEvent>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    private readonly IAccountMovementCommandDataPort _accountMovementCommandDataPort;
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    private readonly IBscScanAccountsDataPort _bscScanAccountsDataPort;
    private readonly ITronNetworkAccountsDataPort _tronNetworkAccountsDataPort;
    private readonly IUserQueryDataPort _userQueryDataPort;

    private const int BSCSCAN_VALUE = 1;
    private const int TRONNETWORK_VALUE = 2;
    private const string BSCSCAN_ADDRESS = "0x21D4356Bb2822A5307A9470fa3bb7524f4067cfE";//TODO: database setting
    private const string BSCSCAN_TOKEN_SYMBOL = "BSC-USD";//TODO: database setting
    private const string TRONNETWORK_ADDRESS = "TErTFxBLsDVxMTaqEhDFL9CbE79YKwpuhY";//TODO: database setting
    private const string TRON_TOKEN_SYMBOL = "USDT";//TODO: database setting
    public AllPaymentVerificationEventHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, IAccountMovementCommandDataPort accountMovementCommandDataPort, IPackageQueryDataPort packageQueryDataPort, IBscScanAccountsDataPort bscScanAccountsDataPort, ITronNetworkAccountsDataPort tronNetworkAccountsDataPort, IUserQueryDataPort userQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _accountMovementCommandDataPort = accountMovementCommandDataPort;
        _packageQueryDataPort = packageQueryDataPort;
        _bscScanAccountsDataPort = bscScanAccountsDataPort;
        _tronNetworkAccountsDataPort = tronNetworkAccountsDataPort;
        _userQueryDataPort = userQueryDataPort;
    }
    public async Task Handle(AllPaymentVerificationEvent request, CancellationToken cancellationToken)
    {
        var packages = await _packageQueryDataPort.GetsAsync();
        var userAddedBonusList = new List<AccountMovement>();
        // AccountMovient Listesini Al (TransactionStatus Pending olan)
        var accountMovements = await _accountMovementQueryDataPort.GetAllMovementAsync(TransactionStatus.Pending);
        // Foreach ile döngüye sok
        foreach (var accountMovement in accountMovements)
        {
            // Network türüne göz at
            // Network türüne göre ödemeyi kontrol et
            // Koşullar sağlanıyorsa UpdatedList'e at
            if (accountMovement.Wallet.CryptoNetwork.Id == BSCSCAN_VALUE)
            {
                var bep20TokenTransferEventsRequest = new Bep20TokenTransferEventsRequest
                {
                    Address = accountMovement.Wallet.WalletAddress
                };
                var bep20TokenTransferEventsResult = await _bscScanAccountsDataPort.GetBep20TokenTransferEventsByAddressAsync(bep20TokenTransferEventsRequest);
                var appropriateTransfers = bep20TokenTransferEventsResult.Result.Where(w =>
                w.To.ToLower() == BSCSCAN_ADDRESS.ToLower() &&
                w.TokenSymbol == BSCSCAN_TOKEN_SYMBOL &&
                AmountCheck(IntToDec(w.Value, w.TokenDecimal), accountMovement.Amount)).ToList();

                foreach (var appropriateTransfer in appropriateTransfers)
                {
                    //Daha Önce bu hash ile işlme var mı?
                    var userMoments = await _accountMovementQueryDataPort.GetUserMovementAsync(accountMovement.Wallet.UserId);
                    if (userMoments.Count == 0 || userMoments.Any(x => x.Hash != appropriateTransfer.Hash))
                    {
                        //Yoksa AccountMovement'a hash'i set et işlemi başarılı de
                        accountMovement.SetTransactionStatus(TransactionStatus.Successful);
                        accountMovement.SetHash(appropriateTransfer.Hash);
                        accountMovement.SetTransferTime(appropriateTransfer.TimesStamp.UnixTimeStampToDateTime());
                        accountMovement.SetTokenSymbol(appropriateTransfer.TokenSymbol);
                        break;
                    }
                }
                await Task.Delay(10000);
            }
            else if (accountMovement.Wallet.CryptoNetwork.Id == TRONNETWORK_VALUE)
            {
                var tronTransfers = await _tronNetworkAccountsDataPort.GetTransfersAsync(accountMovement.Wallet.WalletAddress);
                var appropriateTransfers = tronTransfers.TokenTransfers.Where(x =>
                x.ToAddress.ToLower() == TRONNETWORK_ADDRESS.ToLower() &&
                x.TokenInfo.TokenAbbr == TRON_TOKEN_SYMBOL &&
                x.Confirmed == true &&
                AmountCheck(IntToDec(x.Quant, x.TokenInfo.TokenDecimal.ToString()), accountMovement.Amount)).ToList();

                foreach (var appropriateTransfer in appropriateTransfers)
                {
                    //Daha Önce bu hash ile işlme var mı?
                    //Yoksa AccountMovement'a hash'i set et işlemi başarılı de
                    var userMoments = await _accountMovementQueryDataPort.GetUserMovementAsync(accountMovement.Wallet.UserId);
                    if (userMoments.Count == 0 || userMoments.Any(x => x.Hash != appropriateTransfer.TransactionId))
                    {
                        accountMovement.SetTransactionStatus(TransactionStatus.Successful);
                        accountMovement.SetHash(appropriateTransfer.TransactionId);
                        accountMovement.SetTransferTime(appropriateTransfer.BlockTs.UnixTimeStampToDateTime());
                        accountMovement.SetTokenSymbol(appropriateTransfer.TokenInfo.TokenAbbr);
                        break;
                    }
                }
                await Task.Delay(10000);
            }

            if (accountMovement.TransactionStatus == TransactionStatus.Successful)
            {
                // Kişinin ilk satın alması ise Üst kişiye bonus ekle
                var userAccountMovement = await _accountMovementQueryDataPort.GetUserMovementAsync(accountMovement.Wallet.UserId);
                if (userAccountMovement.Count == 0)
                {
                    var mainUser = await _userQueryDataPort.GetUserAsync(accountMovement.Wallet.UserId);
                    var referanceUser = await _userQueryDataPort.GetUserAsync(mainUser.ReferanceUser);
                    // Paketin changedDay değerini al
                    var package = packages.FirstOrDefault(x => x.Details.Any(y => y.Id == accountMovement.PackageDetail.Id));
                    var bonusAmount = ((accountMovement.Amount * package.Bonus) / 100);
                    var bonusDetail = packages.FirstOrDefault(x => x.Id == 5).Details.FirstOrDefault();
                    var bonus = AccountMovement.CreateNew(bonusAmount, Core.Domain.Base.BaseStatus.Active, TransactionStatus.Pending, ActionType.Bonus, bonusDetail, referanceUser.Wallet, string.Empty, string.Empty, DateTime.Now.AddDays(package.ChangePeriodDay + 1));
                    userAddedBonusList.Add(bonus);
                }
                // Database kayıt işlemlerini gerçekleştir
                // Notification bildirimlerini ekle
            }
        }

        await _accountMovementCommandDataPort.BulkSaveAsync(accountMovements);
        await _accountMovementCommandDataPort.BulkSaveAsync(userAddedBonusList);
    }
    private decimal IntToDec(string x, string powBy)
    {
        return Convert.ToInt64(x) / (decimal)Math.Pow(10.00, Convert.ToInt16(powBy));
    }
    private bool AmountCheck(decimal cryptoAmount, decimal expectedAmount)
    {
        if (expectedAmount > cryptoAmount)
            return false;

        if (((expectedAmount * 10) / 100) >= cryptoAmount)
            return true;

        return false;
    }
}