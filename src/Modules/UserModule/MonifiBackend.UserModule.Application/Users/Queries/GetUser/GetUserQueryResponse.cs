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
            ContractAddress = user.Wallet.WalletAddress;
            Language = user.Language.Name;
            ReferanceCode = user.ReferanceCode;
            Phone = user.Phones?.FirstOrDefault()?.Number;
            Avatar = "/images/avatars/1.png";
            Company = "TestCompany";
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContractAddress { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string ReferanceCode { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string Company { get; set; }
    }
}
