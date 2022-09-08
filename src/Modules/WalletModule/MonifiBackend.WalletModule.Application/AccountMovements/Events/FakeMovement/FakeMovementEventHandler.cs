using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;
using MonifiBackend.WalletModule.Domain.Users;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Events.FakeMovement;

internal class FakeMovementEventHandler : IEventHandler<FakeMovementEvent>
{
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IAccountMovementCommandDataPort _accountMovementCommandDataPort;
    public FakeMovementEventHandler(IUserQueryDataPort userQueryDataPort, IPackageQueryDataPort packageQueryDataPort, IAccountMovementCommandDataPort accountMovementCommandDataPort)
    {
        _packageQueryDataPort = packageQueryDataPort;
        _userQueryDataPort = userQueryDataPort;
        _accountMovementCommandDataPort = accountMovementCommandDataPort;
    }
    public async Task Handle(FakeMovementEvent request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;
        int hour = now.Hour;
        int minute = now.Minute;
        //Saat 09:00 -  14:00 - 16:00 Seed paket satın al
        if ((hour == 09 && minute == 00)
            || (hour == 14 && minute == 0)
            || (hour == 16 && minute == 0))
        {
            //150$
            await sellMovement(4, 150);
        }

        //Saat 10:30 -  19:20 Growth paket satın al 
        if ((hour == 10 && minute == 30) || (hour == 19 && minute == 20))
        {
            //3.000$
            await sellMovement(8, 3000);
        }
        //Çarşamba günü ve Saat 13:00 Hype paket satın al 
        if ((hour == 13 && minute == 0) && now.DayOfWeek == DayOfWeek.Wednesday)
        {
            //6.000$
            await sellMovement(12, 6000);
        }

        //2 haftada bir Cuma günü ve Saat 15:15 Hype paket satın al 
        if ((hour == 15 && minute == 15) && now.DayOfWeek == DayOfWeek.Friday)
        {
            //12.000$
            await sellMovement(16, 12000);
        }
    }
    private async Task sellMovement(int packageDetailId, decimal amount)
    {
        var user = await _userQueryDataPort.GetRandomMonifiUser();
        var packageDetail = await _packageQueryDataPort.GetPackageDetailAsync(packageDetailId);
        var movement = AccountMovement.CreateNew(amount, BaseStatus.Active, TransactionStatus.Successful, ActionType.Sale, packageDetail, user.Wallet, "Monifi", string.Empty, DateTime.UtcNow);
        user.Wallet.AddMovement(movement);
        var result = await _accountMovementCommandDataPort.SaveAsync(user.Wallet);
    }
}
