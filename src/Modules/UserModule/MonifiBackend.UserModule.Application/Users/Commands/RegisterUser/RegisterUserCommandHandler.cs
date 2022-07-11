using MediatR;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Application.Users.Events.UserRegisterComplited;
using MonifiBackend.UserModule.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, IJwtUtils jwtUtils, IMediator mediator)
        {
            _userQueryDataPort = userQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
            _jwtUtils = jwtUtils;
            _mediator = mediator;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserEmail = await _userQueryDataPort.CheckUserEmailAsync(request.Email);
            AppRule.False(isUserEmail, new BusinessValidationException("User already exist.", $"User already exist. Email: {request.Email}"));

            var referanceCodeUser = await _userQueryDataPort.GetReferanceCodeUserAsync(request.ReferanceCode);
            AppRule.ExistsAndActive(referanceCodeUser, new BusinessValidationException("Referance Code not found.", $"Referance Code not found. ReferanceCode: {request.ReferanceCode}"));

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var referanceCode = await GenerateReferanceCode();
            var confirmationCode = await GenerateConfirmationCode();

            var user = User.CreateNew(request.Email, passwordHash, request.Terms, referanceCodeUser.Id, referanceCode, confirmationCode, Role.User, BaseStatus.Passive);
            var userId = await _userCommandDataPort.CreateAsync(user);
            AppRule.NotNegativeOrZero<BusinessValidationException>(userId);

            //TODO: Send E-Mail Event
            var registerComplitedEvent = new UserRegisterComplitedEvent(user.Email);
            await _mediator.Publish(registerComplitedEvent);

            user = await _userQueryDataPort.GetAsync(request.Email, passwordHash);
            AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));

            // authentication successful so generate jwt token
            JwtSecurityToken jwtSecurityToken = await _jwtUtils.GenerateJwtToken(user);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new RegisterUserCommandResponse(user, jwtToken);
        }
        private async Task<string> GenerateReferanceCode()
        {
            string referanceCode;
        TekrarOlustur:
            referanceCode = RandomKeyGenerator.RandomKey(6);
            //Böyle bir referans kodu var mı?
            var isReferanceCode = await _userQueryDataPort.CheckUserReferanceCodeAsync(referanceCode);
            if (!isReferanceCode)
                return referanceCode;
            else
                goto TekrarOlustur;
        }
        private async Task<string> GenerateConfirmationCode()
        {
            string confirmationCode;
        TekrarOlustur:
            confirmationCode = RandomKeyGenerator.RandomKey(6);
            //Böyle bir referans kodu var mı?
            var isConfirmationCode = await _userQueryDataPort.CheckUserConfirmationCodeAsync(confirmationCode);
            if (!isConfirmationCode)
                return confirmationCode;
            else
                goto TekrarOlustur;
        }
    }
}
