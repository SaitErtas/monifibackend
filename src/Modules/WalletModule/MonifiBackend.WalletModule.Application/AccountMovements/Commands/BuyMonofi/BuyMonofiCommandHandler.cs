using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;

internal class BuyMonofiCommandHandler : ICommandHandler<BuyMonofiCommand, BuyMonofiCommandResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    private readonly IAccountMovementCommandDataPort _accountMovementCommandDataPort;
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public BuyMonofiCommandHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, IAccountMovementCommandDataPort accountMovementCommandDataPort, IPackageQueryDataPort packageQueryDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _accountMovementCommandDataPort = accountMovementCommandDataPort;
        _packageQueryDataPort = packageQueryDataPort;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<BuyMonofiCommandResponse> Handle(BuyMonofiCommand request, CancellationToken cancellationToken)
    {
        //Seçilen Paket Var Mı Kontrol et?
        var package = await _packageQueryDataPort.GetPackageDetailIdAsync(request.PackageDetailId);
        AppRule.ExistsAndActive(package, new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Package"])}", $"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["Package"])} Package: {request.PackageDetailId}"));

        var wallet = await _accountMovementQueryDataPort.GetUserWalletAsync(request.UserId);
        //Seçilen Paket ve Miktarı Hesap Haraketlerine ActionType Sale TransactionStatus Pending olarak kaydet
        var movement = AccountMovement.CreateNew(request.Amount, Core.Domain.Base.BaseStatus.Active, TransactionStatus.Pending, ActionType.Sale, package.Details.FirstOrDefault(x => x.Id == request.PackageDetailId), wallet);
        wallet.AddMovement(movement);
        //Referans Olan Kişiye ActionType Bonus olarak paket ayı kadar hesap hareketi ekle

        var result = await _accountMovementCommandDataPort.SaveAsync(wallet);
        AppRule.True(result, new BusinessValidationException($"{string.Format(_stringLocalizer["NotCreated"], _stringLocalizer["AccountMovement"])}", $"{string.Format(_stringLocalizer["NotCreated"], _stringLocalizer["AccountMovement"])} UserId: {request.UserId}"));

        return new BuyMonofiCommandResponse();
    }
}
