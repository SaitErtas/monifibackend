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
        public static BaseStatus ToBaseStatus(this string status)
        {
            return status switch
            {
                BaseStatusStrings.ACTIVE => BaseStatus.Active,
                BaseStatusStrings.PASSIVE => BaseStatus.Passive,
                BaseStatusStrings.DELETED => BaseStatus.Deleted,
                _ => throw new NotImplementedException()
            };
        }
        public static string ToBaseStatus(this BaseStatus status)
        {
            return status switch
            {
                BaseStatus.Active => BaseStatusStrings.ACTIVE,
                BaseStatus.Passive => BaseStatusStrings.PASSIVE,
                BaseStatus.Deleted => BaseStatusStrings.DELETED,
                _ => throw new NotImplementedException()
            };
        }
    }
}
