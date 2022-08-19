using FluentValidation;
using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Localize;
using System.Text.Json.Serialization;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.UpdatePackage;

public class UpdatePackageCommand : ICommand<UpdatePackageCommandResponse>
{
    public UpdatePackageCommand(int id, string name, int duration, int commission)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Commission = commission;
    }
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int Commission { get; private set; }
}

internal class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator(IStringLocalizer<Resource> stringLocalizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Id))}");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Name))}");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Duration))}");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Commission))}");
    }
}