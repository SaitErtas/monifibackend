using FluentValidation;
using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Infrastructure.Localize;
using System.Text.Json.Serialization;

namespace MonifiBackend.PackageModule.Application.Packages.Commands.UpdatePackage;

public class UpdatePackageCommand : ICommand<UpdatePackageCommandResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int ChangePeriodDay { get; set; }
    public string Icon { get; set; }
    public int Bonus { get; set; }
    public List<UpdatePackageDetail> Details { get; set; }
}

public class UpdatePackageDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public int Commission { get; set; }
}
internal class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator(IStringLocalizer<Resource> stringLocalizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Id))}");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Name))}");
        RuleFor(x => x.MinValue)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.MinValue))}");
        RuleFor(x => x.MaxValue)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.MaxValue))}");
        RuleFor(x => x.ChangePeriodDay)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.ChangePeriodDay))}");
        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Icon))}");
        RuleFor(x => x.Bonus)
            .NotEmpty().WithMessage(x => $"{string.Format(stringLocalizer["FieldRequired"], nameof(x.Bonus))}");
    }
}