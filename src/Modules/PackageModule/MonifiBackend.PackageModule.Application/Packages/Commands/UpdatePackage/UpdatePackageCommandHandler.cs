using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.UpdatePackage;

internal class UpdatePackageCommandHandler : ICommandHandler<UpdatePackageCommand, UpdatePackageCommandResponse>
{
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    private readonly IPackageCommandDataPort _packageCommandDataPort;

    public UpdatePackageCommandHandler(IPackageQueryDataPort packageQueryDataPort, IPackageCommandDataPort packageCommandDataPort)
    {
        _packageQueryDataPort = packageQueryDataPort;
        _packageCommandDataPort = packageCommandDataPort;
    }

    public async Task<UpdatePackageCommandResponse> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageQueryDataPort.GetPackageAsync(request.Id);
        AppRule.ExistsAndActive(package, new BusinessValidationException(BusinessValidationMessageType.NOT_FOUND, nameof(request.Id), request.Id));

        package.SetName(request.Name);

        var status = await _packageCommandDataPort.SaveAsync(package);
        AppRule.True(status, new BusinessValidationException(BusinessValidationMessageType.NOT_UPDATED, nameof(request.Id), request.Id));

        return new UpdatePackageCommandResponse();
    }
}