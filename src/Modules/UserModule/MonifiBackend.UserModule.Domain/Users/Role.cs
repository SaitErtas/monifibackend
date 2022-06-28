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
        public const string ADMINISTRATOR = "administrator";
        public const string OWNER = "owner";
        public const string USER = "user";
    }
    public static class RoleExtensions
    {
        public static Role ToRole(this string status)
        {
            return status switch
            {
                RoleStrings.ADMINISTRATOR => Role.Administrator,
                RoleStrings.OWNER => Role.Owner,
                RoleStrings.USER => Role.User,
                _ => throw new NotImplementedException()
            };
        }
        public static string ToRole(this Role status)
        {
            return status switch
            {
                Role.Administrator => RoleStrings.ADMINISTRATOR,
                Role.Owner => RoleStrings.OWNER,
                Role.User => RoleStrings.USER,
                _ => throw new NotImplementedException()
            };
        }
    }
}
