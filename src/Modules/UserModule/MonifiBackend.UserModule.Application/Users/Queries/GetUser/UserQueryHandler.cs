using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    internal class UserQueryHandler : IQueryHandler<UserQuery, UserQueryResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IJwtUtils _jwtUtils;
        public UserQueryHandler(IUserQueryDataPort userQueryDataPort, IJwtUtils jwtUtils)
        {
            _userQueryDataPort = userQueryDataPort;
            _jwtUtils = jwtUtils;
        }
        public async Task<UserQueryResponse> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQueryDataPort.GetAsync(request.UserId);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.UserId}"));

            return new UserQueryResponse(user);
        }
    }
}
