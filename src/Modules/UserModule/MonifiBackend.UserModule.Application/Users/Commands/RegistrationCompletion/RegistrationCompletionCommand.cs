using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegistrationCompletion;

public class RegistrationCompletionCommand : ICommand<RegistrationCompletionCommandResponse>
{
    public RegistrationCompletionCommand(int userId, string username, string fullName, string walletAddress, int cryptoNetworkId, string contract, int countryId, int languageId)
    {
        Username = username;
        FullName = fullName;
        WalletAddress = walletAddress;
        CryptoNetworkId = cryptoNetworkId;
        Contract = contract;
        CountryId = countryId;
        LanguageId = languageId;
    }
    [JsonIgnore]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string WalletAddress { get; set; }
    public int CryptoNetworkId { get; set; }
    public string Contract { get; set; }
    public int CountryId { get; set; }
    public int LanguageId { get; set; }
}

internal class RegistrationCompletionCommandValidator : AbstractValidator<RegistrationCompletionCommand>
{
    public RegistrationCompletionCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username alanı boş bırakılamaz.");
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName alanı boş bırakılamaz.");
        RuleFor(x => x.WalletAddress)
            .NotEmpty().WithMessage("ContractAddress alanı boş bırakılamaz.");
        RuleFor(x => x.CryptoNetworkId)
            .NotEmpty().WithMessage("CryptoNetwork alanı boş bırakılamaz.");
        RuleFor(x => x.Contract)
            .NotEmpty().WithMessage("Contract alanı boş bırakılamaz.");
        RuleFor(x => x.CountryId)
            .NotEmpty().WithMessage("Language alanı boş bırakılamaz.");
        RuleFor(x => x.LanguageId)
            .NotEmpty().WithMessage("Language alanı boş bırakılamaz.");
    }
}