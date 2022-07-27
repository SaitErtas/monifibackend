using MonifiBackend.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;

public class BuyMonofiCommand : ICommand<BuyMonofiCommandResponse>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public int PackageDetailId { get; set; }
}
