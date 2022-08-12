using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.UserModule.Domain.Localizations;
using MonifiBackend.UserModule.Domain.Users;
using System.Globalization;

namespace MonifiBackend.UserModule.Application.Users.Commands.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly ILocalizationQueryDataPort _localizationQueryDataPort;
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public UpdateUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, ILocalizationQueryDataPort localizationQueryDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
        _localizationQueryDataPort = localizationQueryDataPort;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user,
            new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["User"])}", $"{string.Format(_stringLocalizer["NotFound"], _stringLocalizer["User"])} UserId: {request.UserId}"));

        var language = await _localizationQueryDataPort.GetLanguageAsync(request.LanguageId);
        var country = await _localizationQueryDataPort.GetCountryAsync(request.CountryId);

        user.SetUsername(request.Username);
        user.SetFullName(request.FullName);
        user.SetCountry(country);
        user.SetLanguage(language);

        if (request.PhoneId == null)
            user.Phones.FirstOrDefault().SetPhone(request.Phone);

        Thread.CurrentThread.CurrentUICulture = new CultureInfo($"{user.Language.ShortName}");

        var status = await _userCommandDataPort.SaveAsync(user);
        AppRule.True<BusinessValidationException>(status);

        return new UpdateUserCommandResponse();
    }
}
