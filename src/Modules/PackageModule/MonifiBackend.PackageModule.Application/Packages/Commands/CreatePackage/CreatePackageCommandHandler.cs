using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Localize;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;

internal class CreatePackageCommandHandler : ICommandHandler<CreatePackageCommand, CreatePackageCommandResponse>
{
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    private readonly IPackageCommandDataPort _packageCommandDataPort;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public CreatePackageCommandHandler(IPackageQueryDataPort packageQueryDataPort, IPackageCommandDataPort packageCommandDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _packageQueryDataPort = packageQueryDataPort;
        _packageCommandDataPort = packageCommandDataPort;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<CreatePackageCommandResponse> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var isPackage = await _packageQueryDataPort.GetAsync(request.Duration);
        AppRule.False(isPackage, new BusinessValidationException(BusinessValidationMessageType.ALREADY_EXIST, nameof(request.Duration), request.Duration));

        var package = Package.CreateNew(request.Name, request.Duration, request.Commission, BaseStatus.Active);
        var packageId = await _packageCommandDataPort.CreateAsync(package);
        AppRule.NotNegativeOrZero(packageId, new BusinessValidationException(BusinessValidationMessageType.NOT_CREATED, nameof(request.Name), request.Name));

        return new CreatePackageCommandResponse();
    }
}