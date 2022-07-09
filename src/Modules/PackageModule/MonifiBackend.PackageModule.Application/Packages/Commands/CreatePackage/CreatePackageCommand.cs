using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;

public class CreatePackageCommand : ICommand<CreatePackageCommandResponse>
{
    public CreatePackageCommand(string userName, int duration, decimal commission)
    {
        UserName = userName;
        Duration = duration;
        Commission = commission;
    }
    public string UserName { get; set; }
    public int Duration { get; set; }
    public decimal Commission { get; set; }
}

internal class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
{
    public CreatePackageCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is not null.");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is not null.");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage("Commission is not null.");
    }
}
