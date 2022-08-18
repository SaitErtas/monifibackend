using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Domain.Localize;

namespace MonifiBackend.Core.Domain.Base;

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
    public static string ToActionType(this ActionType status, IStringLocalizer<Resource> stringLocalizer)
    {
        return status switch
        {
            ActionType.Sale => stringLocalizer["Sale"],
            ActionType.Refund => stringLocalizer["Refund"],
            ActionType.Void => stringLocalizer["Void"],
            ActionType.Bonus => stringLocalizer["Bonus"],
            _ => throw new NotImplementedException()
        };
    }
}
