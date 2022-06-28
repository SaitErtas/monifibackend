using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MonifiBackend.UserModule.UnitTests")]
[assembly: InternalsVisibleTo("MonifiBackend.UserModule.ArchTests")]
namespace MonifiBackend.UserModule.Application.Users.Commands.ChangedPassword
{
    internal class ChangedPasswordCommandHandler : ICommandHandler<ChangedPasswordCommand, ChangedPasswordCommandResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;

        public ChangedPasswordCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort)
        {
            _userQueryDataPort = userQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
        }

        public async Task<ChangedPasswordCommandResponse> Handle(ChangedPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryDataPort.GetEmailAsync(request.Email);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));
            AppRule.True(user.ResetPasswordCode == request.ResetPasswordCode, new BusinessValidationException("Not Mach PasswordCode Exception.", $"Not Mach PasswordCode Exception. Email: {request.Email}"));

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            user.SetPassword(passwordHash);
            user.SetResetPasswordCode(string.Empty);

            var result = await _userCommandDataPort.SaveAsync(user);
            AppRule.True(result, new BusinessValidationException("User Not Updated Exception.", $"User Not Updated Exception. UserId: {user.Id}"));

            return new ChangedPasswordCommandResponse();
        }
    }
}
