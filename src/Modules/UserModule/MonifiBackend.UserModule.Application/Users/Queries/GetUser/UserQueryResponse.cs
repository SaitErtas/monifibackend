using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    public class UserQueryResponse
    {
        public UserQueryResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FullName = user.UserName;
            Email = user.Email;
            Role = user.Role.ToRole();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
