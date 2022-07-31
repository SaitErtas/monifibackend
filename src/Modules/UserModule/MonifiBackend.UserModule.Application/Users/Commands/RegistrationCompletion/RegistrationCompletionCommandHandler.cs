using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Accounts;
using MonifiBackend.Core.Domain.BscScans;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.TronNetworks;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.UserModule.Domain.Localizations;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Wallets;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegistrationCompletion;

internal class RegistrationCompletionCommandHandler : ICommandHandler<RegistrationCompletionCommand, RegistrationCompletionCommandResponse>
{
    private const int BSCSCAN_VALUE = 1;
    private const int TRONNETWORK_VALUE = 2;
    private readonly IWalletQueryDataPort _walletQueryDataPort;
    private readonly ILocalizationQueryDataPort _localizationQueryDataPort;
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;
    private readonly IBscScanAccountsDataPort _bscScanAccountsDataPort;
    private readonly ITronNetworkAccountsDataPort _tronNetworkAccountsDataPort;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public RegistrationCompletionCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, ILocalizationQueryDataPort localizationQueryDataPort, IWalletQueryDataPort walletQueryDataPort, IBscScanAccountsDataPort bscScanAccountsDataPort, ITronNetworkAccountsDataPort tronNetworkAccountsDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
        _localizationQueryDataPort = localizationQueryDataPort;
        _walletQueryDataPort = walletQueryDataPort;
        _bscScanAccountsDataPort = bscScanAccountsDataPort;
        _tronNetworkAccountsDataPort = tronNetworkAccountsDataPort;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<RegistrationCompletionCommandResponse> Handle(RegistrationCompletionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user, new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["User"])}", $"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["User"])} UserId: {request.UserId}"));
        AppRule.True(string.IsNullOrEmpty(user.Wallet.WalletAddress), new BusinessValidationException($"{_stringLocalizer["UserAlreadySubscribed"]}", $"{_stringLocalizer["UserAlreadySubscribed"]} UserId: {request.UserId}"));

        var language = await _localizationQueryDataPort.GetLanguageAsync(request.LanguageId);
        var country = await _localizationQueryDataPort.GetCountryAsync(request.CountryId);
        var network = await _walletQueryDataPort.GetNetworkAsync(request.CryptoNetworkId);

        if (network.Id == BSCSCAN_VALUE)
        {
            var bnbBalanceRequest = new BnbBalanceRequest
            {
                Address = request.WalletAddress,
            };
            var bnbBalance = await _bscScanAccountsDataPort.GetBnbBalanceAsync(bnbBalanceRequest);
            AppRule.True(bnbBalance.Status == "1", new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Wallet"])}", $"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Wallet"])} WalletAddress: {request.WalletAddress}"));
        }
        else if (network.Id == TRONNETWORK_VALUE)
        {
            var account = await _tronNetworkAccountsDataPort.GetAccountsAsync(request.WalletAddress);
            AppRule.True(account.Success, new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Wallet"])}", $"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Wallet"])} WalletAddress: {request.WalletAddress}"));
        }


        user.Wallet.SetWalletAddress(request.WalletAddress);
        user.Wallet.SetNetwork(network);

        user.SetUsername(request.Username);
        user.SetFullName(request.FullName);
        user.SetCountry(country);
        user.SetLanguage(language);

        if (request.PhoneId == null) user.AddPhone(request.Phone);


        var status = await _userCommandDataPort.SaveAsync(user);
        AppRule.True<BusinessValidationException>(status);

        return new RegistrationCompletionCommandResponse();
    }
}