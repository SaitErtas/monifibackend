using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;

internal class BuyMonofiCommandHandler : ICommandHandler<BuyMonofiCommand, BuyMonofiCommandResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    private readonly IAccountMovementCommandDataPort _accountMovementCommandDataPort;
    private readonly IPackageQueryDataPort _packageQueryDataPort;

    public BuyMonofiCommandHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, IAccountMovementCommandDataPort accountMovementCommandDataPort, IPackageQueryDataPort packageQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _accountMovementCommandDataPort = accountMovementCommandDataPort;
        _packageQueryDataPort = packageQueryDataPort;
    }

    public async Task<BuyMonofiCommandResponse> Handle(BuyMonofiCommand request, CancellationToken cancellationToken)
    {
        //Seçilen Paket Var Mı Kontrol et?
        var package = await _packageQueryDataPort.GetPackageAsync(request.PaketId);
        AppRule.ExistsAndActive(package, new BusinessValidationException("Package not found.", $"Package not found exception. Package: {request.PaketId}"));
        var packageDetail = package.Details.FirstOrDefault(x => x.MinValue <= request.Amount && x.MaxValue >= request.Amount);
        AppRule.ExistsAndActive(packageDetail, new BusinessValidationException("PackageDetail not found.", $"PackageDetail not found exception. PackageDetail: {request.PaketId}"));


        var wallet = await _accountMovementQueryDataPort.GetUserWalletAsync(request.UserId);
        //Seçilen Paket ve Miktarı Hesap Haraketlerine ActionType Sale TransactionStatus Pending olarak kaydet
        var movement = AccountMovement.CreateNew(request.Amount, Core.Domain.Base.BaseStatus.Active, TransactionStatus.Pending, ActionType.Sale, packageDetail, wallet);
        wallet.AddMovement(movement);
        //Referans Olan Kişiye ActionType Bonus olarak paket ayı kadar hesap hareketi ekle

        var result = await _accountMovementCommandDataPort.SaveAsync(wallet);
        AppRule.True(result, new BusinessValidationException("AccountMovement Not Created Exception.", $"AccountMovement Not Created Exception. UserId: {request.UserId}"));

        return new BuyMonofiCommandResponse();
    }
}
