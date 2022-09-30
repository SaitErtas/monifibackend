using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Application.Bots.Commands.CreateBot;

public class CreateBotCommand : ICommand<CreateBotCommandResponse>
{
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Day { get; set; }
    public int Amount { get; set; }
}
internal class CreateBotCommandValidator : AbstractValidator<CreateBotCommand>
{
    public CreateBotCommandValidator()
    {
    }
}