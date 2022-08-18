using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Domain.Localize;

namespace MonifiBackend.UserModule.Domain.Users
{
    public enum Role
    {
        Administrator = 1,
        Owner = 2,
        User = 3
    }
    public class RoleStrings
    {
        public const string ADMINISTRATOR = "admin";
        public const string OWNER = "owner";
        public const string USER = "user";
    }
    public static class RoleExtensions
    {
        public static string ToRole(this Role status, IStringLocalizer<Resource> stringLocalizer)
        {
            return status switch
            {
                Role.Administrator => stringLocalizer["Admin"],
                Role.Owner => stringLocalizer["Owner"],
                Role.User => stringLocalizer["User"],
                _ => throw new NotImplementedException()
            };
        }
    }
}
