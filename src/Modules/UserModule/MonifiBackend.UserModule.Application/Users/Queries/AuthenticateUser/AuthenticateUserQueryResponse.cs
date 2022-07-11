using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryResponse
    {
        public AuthenticateUserQueryResponse(User user, string accessToken)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role.ToRole();
            AccessToken = accessToken;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
