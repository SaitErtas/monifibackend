namespace MonifiBackend.UserModule.Domain.Wallets;

public enum ActionType
{
    Sale = 1,
    Refund = 2,
    Void = 3,
    Bonus = 4,
}
public class ActionTypeStrings
{
    public const string SALE = "Sale";
    public const string REFUND = "Refund";
    public const string VOID = "Void";
    public const string BONUS = "Bonus";
}
public static class ActionTypeExtensions
{
    public static ActionType ToActionType(this string status)
    {
        return status switch
        {
            ActionTypeStrings.SALE => ActionType.Sale,
            ActionTypeStrings.REFUND => ActionType.Refund,
            ActionTypeStrings.VOID => ActionType.Void,
            ActionTypeStrings.BONUS => ActionType.Bonus,
            _ => throw new NotImplementedException()
        };
    }
    public static string ToActionType(this ActionType status)
    {
        return status switch
        {
            ActionType.Sale => ActionTypeStrings.SALE,
            ActionType.Refund => ActionTypeStrings.REFUND,
            ActionType.Void => ActionTypeStrings.VOID,
            ActionType.Bonus => ActionTypeStrings.BONUS,
            _ => throw new NotImplementedException()
        };
    }
}
