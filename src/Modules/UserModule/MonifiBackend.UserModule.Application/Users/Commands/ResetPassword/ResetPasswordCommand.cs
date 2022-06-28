using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : ICommand<ResetPasswordCommandResponse>
    {
        public string Email { get; set; }
    }
}
