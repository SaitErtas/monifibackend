using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;

public class BuyMonofiCommand : ICommand<BuyMonofiCommandResponse>
{
    public string Email { get; set; }
}
