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
    public static TransactionStatus ToTransactionStatus(this string status)
    {
        return status switch
        {
            TransactionStatusStrings.SUCCESSFUL => TransactionStatus.Successful,
            TransactionStatusStrings.PENDING => TransactionStatus.Pending,
            TransactionStatusStrings.FAIL => TransactionStatus.Fail,
            _ => throw new NotImplementedException()
        };
    }
    public static string ToTransactionStatus(this TransactionStatus status)
    {
        return status switch
        {
            TransactionStatus.Successful => TransactionStatusStrings.SUCCESSFUL,
            TransactionStatus.Pending => TransactionStatusStrings.PENDING,
            TransactionStatus.Fail => TransactionStatusStrings.FAIL,
            _ => throw new NotImplementedException()
        };
    }
}

