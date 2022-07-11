using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    public class UserQueryResponse
    {
        public UserQueryResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role.ToRole();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
