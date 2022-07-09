using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
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
    public UpdatePackageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is not null.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is not null.");
        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is not null.");
        RuleFor(x => x.Commission)
            .NotEmpty().WithMessage("Commission is not null.");
    }
}