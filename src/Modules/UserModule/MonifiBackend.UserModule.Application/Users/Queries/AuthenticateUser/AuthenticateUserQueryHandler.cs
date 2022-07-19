using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Queries.AuthenticateUser
{
    internal class AuthenticateUserQueryHandler : IQueryHandler<AuthenticateUserQuery, AuthenticateUserQueryResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IJwtUtils _jwtUtils;
        public AuthenticateUserQueryHandler(IUserQueryDataPort userQueryDataPort, IJwtUtils jwtUtils)
        {
            _userQueryDataPort = userQueryDataPort;
            _jwtUtils = jwtUtils;
        }
        public async Task<AuthenticateUserQueryResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQueryDataPort.GetEmailAsync(request.Email);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));

            //var userPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            AppRule.True(verified, new BusinessValidationException("User Not Verified Exception.", $"User Not Verified Exception. Email: {request.Email}"));

            // authentication successful so generate jwt token
            JwtSecurityToken jwtSecurityToken = await _jwtUtils.GenerateJwtToken(user);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new AuthenticateUserQueryResponse(user, jwtToken);
        }
    }
}
