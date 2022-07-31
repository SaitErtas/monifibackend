using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MonifiBackend.UserModule.Application.Notifications.Commands.CreateNotification;

public class CreateNotificationCommand : ICommand<CreateNotificationCommandResponse>
{
    public CreateNotificationCommand(int userId, string message)
    {
        UserId = userId;
        Message = message;
    }
    [JsonIgnore]
    public int UserId { get; set; }
    public string Message { get; set; }
}

internal class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId alanı boş bırakılamaz.");
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message alanı boş bırakılamaz.");
    }
}