using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Commands.ResetPassword
{
    internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;

        public ResetPasswordCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort)
        {
            _userQueryDataPort = userQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryDataPort.GetEmailAsync(request.Email);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));

            var resetPasswordCode = Guid.NewGuid().ToString();
            user.SetResetPasswordCode(resetPasswordCode);

            var result = await _userCommandDataPort.SaveAsync(user);
            AppRule.True(result, new BusinessValidationException("User Not Updated Exception.", $"User already exist. UserId: {user.Id}"));

            return new ResetPasswordCommandResponse();
        }
    }
}
