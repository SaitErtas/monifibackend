using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Commands.ChangedPassword
{
    public class ChangedPasswordCommand : ICommand<ChangedPasswordCommandResponse>
    {
        public string Email { get; set; }
        public string ResetPasswordCode { get; set; }
        public string NewPassword { get; set; }
    }
}
