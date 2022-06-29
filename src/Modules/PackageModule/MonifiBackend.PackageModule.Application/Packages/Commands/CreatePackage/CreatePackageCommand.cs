using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;

public class CreatePackageCommand : ICommand<CreatePackageCommandResponse>
{
    public CreatePackageCommand(string name, int duration, decimal commission)
    {
        Name = name;
        Duration = duration;
        Commission = commission;
    }
    public string Name { get; set; }
    public int Duration { get; set; }
    public decimal Commission { get; set; }
}

internal class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
{
    public CreatePackageCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Mail alanı boş bırakılamaz.");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");
    }
}
