using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.UserModule.Domain.Users;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Queries.AuthenticateUser
{
    internal class AuthenticateUserQueryHandler : IQueryHandler<AuthenticateUserQuery, AuthenticateUserQueryResponse>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;
        private readonly IJwtUtils _jwtUtils;
        public AuthenticateUserQueryHandler(IUserCommandDataPort userCommandDataPort, IUserQueryDataPort userQueryDataPort, IJwtUtils jwtUtils, IStringLocalizer<Resource> stringLocalizer)
        {
            _userQueryDataPort = userQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
            _jwtUtils = jwtUtils;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<AuthenticateUserQueryResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQueryDataPort.GetEmailAsync(request.Email);
            AppRule.Exists(user, new BusinessValidationException(string.Format(_stringLocalizer["NotFound"], request.Email), $"{string.Format(_stringLocalizer["NotFound"], request.Email)} Email: {request.Email}"));
            AppRule.ExistsAndActive(user, new BusinessValidationException(string.Format(_stringLocalizer["NotActivetedUser"], request.Email), $"{string.Format(_stringLocalizer["NotActivetedUser"], request.Email)} Email: {request.Email}"));

            //var userPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            AppRule.True(verified, new BusinessValidationException($"{_stringLocalizer["UserNotVerified"]}", $"{_stringLocalizer["UserNotVerified"]}. Email: {request.Email}"));

            user.AddUserIP(request.IpAddress, "Login");

            Thread.CurrentThread.CurrentUICulture = new CultureInfo($"{user.Language.ShortName}");
            user.AddNotification($"{string.Format(_stringLocalizer["LoginNotification"], DateTime.Now.ToString("d"), request.IpAddress)}", user.FullName, default(decimal));

            await _userCommandDataPort.SaveAsync(user);
            // authentication successful so generate jwt token
            JwtSecurityToken jwtSecurityToken = await _jwtUtils.GenerateJwtToken(user);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new AuthenticateUserQueryResponse(user, jwtToken);
        }
    }
}
