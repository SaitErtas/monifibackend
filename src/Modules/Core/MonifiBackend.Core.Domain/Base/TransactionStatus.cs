using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Domain.Localize;

namespace MonifiBackend.Core.Domain.Base;

public enum TransactionStatus
{
    Successful = 1,
    Pending = 2,
    Fail = 3,
}

public class TransactionStatusStrings
{
    public const string SUCCESSFUL = "Successful";
    public const string PENDING = "Pending";
    public const string FAIL = "Fail";
}
public static class TransactionStatusExtensions
{
    public static string ToTransactionStatus(this TransactionStatus status, IStringLocalizer<Resource> stringLocalizer)
    {
        return status switch
        {
            TransactionStatus.Successful => stringLocalizer["Successful"],
            TransactionStatus.Pending => stringLocalizer["Pending"],
            TransactionStatus.Fail => stringLocalizer["Fail"],
            _ => throw new NotImplementedException()
        };
    }
}

