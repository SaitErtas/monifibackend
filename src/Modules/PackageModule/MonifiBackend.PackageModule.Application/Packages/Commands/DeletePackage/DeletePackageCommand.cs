using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.DeletePackage;

public class DeletePackageCommand : ICommand<DeletePackageCommandResponse>
{
    public DeletePackageCommand(int id)
    {
        Id = id;
    }
    [JsonIgnore]
    public int Id { get; private set; }
}

internal class DeletePackageCommandValidator : AbstractValidator<DeletePackageCommand>
{
    public DeletePackageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is not null.");
    }
}