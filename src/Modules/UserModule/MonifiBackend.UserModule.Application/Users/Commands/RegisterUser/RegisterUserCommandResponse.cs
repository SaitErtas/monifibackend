using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandResponse
    {
        public RegisterUserCommandResponse(User user, string accessToken)
        {
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
            Role = user.Role.ToRole();
            AccessToken = accessToken;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
