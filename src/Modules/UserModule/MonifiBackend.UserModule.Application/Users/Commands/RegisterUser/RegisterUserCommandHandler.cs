using MediatR;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Application.Users.Events.UserRegisterComplited;
using MonifiBackend.UserModule.Domain.Localizations;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Wallets;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IWalletQueryDataPort _walletQueryDataPort;
        private readonly ILocalizationQueryDataPort _localizationQueryDataPort;
        private readonly IUserQueryDataPort _userQueryDataPort;
        private readonly IUserCommandDataPort _userCommandDataPort;
        private readonly IMediator _mediator;
        private const int DEFAULT_VALUE = 1;

        public RegisterUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, IMediator mediator, ILocalizationQueryDataPort localizationQueryDataPort, IWalletQueryDataPort walletQueryDataPort)
        {
            _userQueryDataPort = userQueryDataPort;
            _localizationQueryDataPort = localizationQueryDataPort;
            _userCommandDataPort = userCommandDataPort;
            _walletQueryDataPort = walletQueryDataPort;
            _mediator = mediator;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserEmail = await _userQueryDataPort.CheckUserEmailAsync(request.Email);
            AppRule.False(isUserEmail, new BusinessValidationException("User already exist.", $"User already exist. Email: {request.Email}"));

            var referanceCodeUser = await _userQueryDataPort.GetReferanceCodeUserAsync(request.ReferenceCode);
            AppRule.ExistsAndActive(referanceCodeUser, new BusinessValidationException("Referance Code not found.", $"Referance Code not found. ReferanceCode: {request.ReferenceCode}"));

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var referanceCode = await GenerateReferanceCode();
            var confirmationCode = await GenerateConfirmationCode();

            var language = await _localizationQueryDataPort.GetLanguageAsync(DEFAULT_VALUE);
            var country = await _localizationQueryDataPort.GetCountryAsync(DEFAULT_VALUE);
            var network = await _walletQueryDataPort.GetNetworkAsync(DEFAULT_VALUE);

            var wallet = Wallet.CreateNew(string.Empty, network);
            var user = User.CreateNew(request.Email, passwordHash, request.Terms, referanceCodeUser.Id, referanceCode, confirmationCode, language, country, wallet, Role.User, BaseStatus.Passive);
            var userId = await _userCommandDataPort.CreateAsync(user);
            AppRule.NotNegativeOrZero<BusinessValidationException>(userId);

            //TODO: Send E-Mail Event
            var registerComplitedEvent = new UserRegisterComplitedEvent(user.Email);
            await _mediator.Publish(registerComplitedEvent);

            return new RegisterUserCommandResponse();
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
