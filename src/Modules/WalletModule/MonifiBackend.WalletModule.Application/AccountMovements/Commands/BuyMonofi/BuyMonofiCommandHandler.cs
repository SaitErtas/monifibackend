using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;

internal class BuyMonofiCommandHandler : ICommandHandler<BuyMonofiCommand, BuyMonofiCommandResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    private readonly IAccountMovementCommandDataPort _accountMovementCommandDataPort;

    public BuyMonofiCommandHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, IAccountMovementCommandDataPort accountMovementCommandDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _accountMovementCommandDataPort = accountMovementCommandDataPort;
    }

    public async Task<BuyMonofiCommandResponse> Handle(BuyMonofiCommand request, CancellationToken cancellationToken)
    {
        //Seçilen Paket Var Mı Kontrol et?
        //Seçilen Paket ve Miktarı Hesap Haraketlerine ActionType Sale TransactionStatus Pending olarak kaydet
        //Referans Olan Kişiye ActionType Bonus olarak paket ayı kadar hesap hareketi ekle

        return new BuyMonofiCommandResponse();
    }
}
