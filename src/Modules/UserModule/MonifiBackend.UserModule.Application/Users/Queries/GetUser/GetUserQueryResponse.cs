using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetUser
{
    public class GetUserQueryResponse
    {
        public GetUserQueryResponse(User user)
        {
            Id = user.Id;
            Role = user.Role.ToRole();
            Username = user.Username;
            FullName = user.FullName;
            Email = user.Email;
            Country = user.Country.Name;
            WalletAddress = user.Wallet.WalletAddress;
            WalletAddressId = user.Wallet.Id;
            Language = user.Language.Name;
            ReferenceCode = user.ReferanceCode;
            Phone = user.Phones?.FirstOrDefault()?.Number;
            Avatar = "/images/avatars/1.png";
            Company = "TestCompany";
            CryptoNetworkId = user.Wallet.CryptoNetwork.Id;
            CryptoNetwork = user.Wallet.CryptoNetwork.Name;
            CountryId = user.Country.Id;
            LanguageId = user.Language.Id;


        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? WalletAddressId { get; set; }
        public string WalletAddress { get; set; }
        public string ReferenceCode { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string Company { get; set; }
        public int? CryptoNetworkId { get; set; }
        public string CryptoNetwork { get; set; }
        public string Phone { get; set; }
        public int? PhoneId { get; set; }
        public string Country { get; set; }
        public int? CountryId { get; set; }
        public string Language { get; set; }
        public int? LanguageId { get; set; }
    }
}
