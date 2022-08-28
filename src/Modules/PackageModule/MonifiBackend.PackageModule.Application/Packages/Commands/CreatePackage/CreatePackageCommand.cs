using FluentValidation;
using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Infrastructure.Localize;

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
    public CreatePackageCommandValidator(IStringLocalizer<Resource> stringLocalizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Name))}");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Duration))}");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Commission))}");
    }
}
