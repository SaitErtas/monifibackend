using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Localizations;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Wallets;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegistrationCompletion;

internal class RegistrationCompletionCommandHandler : ICommandHandler<RegistrationCompletionCommand, RegistrationCompletionCommandResponse>
{

    private readonly IWalletQueryDataPort _walletQueryDataPort;
    private readonly ILocalizationQueryDataPort _localizationQueryDataPort;
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;

    public RegistrationCompletionCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, ILocalizationQueryDataPort localizationQueryDataPort, IWalletQueryDataPort walletQueryDataPort)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
        _localizationQueryDataPort = localizationQueryDataPort;
        _walletQueryDataPort = walletQueryDataPort;
    }

    public async Task<RegistrationCompletionCommandResponse> Handle(RegistrationCompletionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user, new BusinessValidationException("User not found.", $"User not found. UserId: {request.UserId}"));
        AppRule.True(string.IsNullOrEmpty(user.Wallet.WalletAddress), new BusinessValidationException("Already subscribed user.", $"Already subscribed user UserId: {request.UserId}"));

        var language = await _localizationQueryDataPort.GetLanguageAsync(request.LanguageId);
        var country = await _localizationQueryDataPort.GetCountryAsync(request.CountryId);
        var network = await _walletQueryDataPort.GetNetworkAsync(request.CryptoNetworkId);

        user.Wallet.SetWalletAddress(request.WalletAddress);
        user.Wallet.SetNetwork(network);

        user.SetUsername(request.Username);
        user.SetFullName(request.FullName);
        user.SetCountry(country);
        user.SetLanguage(language);

        user.AddPhone(request.Phone);
        

        var status = await _userCommandDataPort.SaveAsync(user);
        AppRule.True<BusinessValidationException>(status);

        return new RegistrationCompletionCommandResponse();
    }
}