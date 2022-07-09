using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;

public class CreatePackageCommand : ICommand<CreatePackageCommandResponse>
{
    public CreatePackageCommand(string name, int duration, int commission)
    {
        Name = name;
        Duration = duration;
        Commission = commission;
    }
    public string Name { get; set; }
    public int Duration { get; set; }
    public int Commission { get; set; }
}

internal class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
{
    public CreatePackageCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is not null.");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is not null.");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage("Commission is not null.");
    }
}
