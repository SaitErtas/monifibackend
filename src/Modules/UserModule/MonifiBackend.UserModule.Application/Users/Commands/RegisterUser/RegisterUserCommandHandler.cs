using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;
        private readonly IJwtUtils _jwtUtils;

        public RegisterUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, IJwtUtils jwtUtils)
        {
            _userQueryDataPort = userQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
            _jwtUtils = jwtUtils;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUser = await _userQueryDataPort.GetAsync(request.Email);
            AppRule.False(isUser, new BusinessValidationException("User already exist.", "User already exist. Email: {request.Email}"));

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = User.CreateNew(request.Email, passwordHash, request.UserName,request.Terms, Role.User, BaseStatus.Active);
            var userId = await _userCommandDataPort.CreateAsync(user);
            AppRule.NotNegativeOrZero<BusinessValidationException>(userId);

            user = await _userQueryDataPort.GetAsync(request.Email, passwordHash);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));

            // authentication successful so generate jwt token
            JwtSecurityToken jwtSecurityToken = await _jwtUtils.GenerateJwtToken(user);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new RegisterUserCommandResponse(user, jwtToken);
        }
    }
}
