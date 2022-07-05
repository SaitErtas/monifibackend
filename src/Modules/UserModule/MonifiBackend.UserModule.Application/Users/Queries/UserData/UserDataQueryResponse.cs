using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    public class UserDataQueryResponse
    {
        public UserDataQueryResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Role = user.Role.ToRole();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
