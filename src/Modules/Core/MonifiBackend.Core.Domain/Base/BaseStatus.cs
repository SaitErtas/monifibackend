using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Domain.Localize;

namespace MonifiBackend.Core.Domain.Base
{
    public enum BaseStatus
    {
        Active = 1,
        Passive = 2,
        Deleted = -1,
    }
    public class BaseStatusStrings
    {
        public const string ACTIVE = "Active";
        public const string PASSIVE = "Passive";
        public const string DELETED = "Deleted";
    }
    public static class BaseStatusExtensions
    {
        public static string ToBaseStatus(this BaseStatus status, IStringLocalizer<Resource> stringLocalizer)
        {
            return status switch
            {
                BaseStatus.Active => stringLocalizer["Active"],
                BaseStatus.Passive => stringLocalizer["Passive"],
                BaseStatus.Deleted => stringLocalizer["Deleted"],
                _ => throw new NotImplementedException()
            };
        }
    }
}
