using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;

namespace MonifiBackend.UserModule.Domain.Users.Phones
{
    public sealed class UserPhone : BaseActivityDomain<int>
    {
        private UserPhone() { }
        public PhoneType PhoneType { get; private set; }
        public string Number { get; private set; }
        public static UserPhone CreateNew(string number, PhoneType phoneType)
        {
            AppRule.NotNullOrEmpty<DomainException>(number, "Number not null or empty", $"Number not null or empty. Number: {number}");

            return new UserPhone()
            {
                PhoneType = phoneType,
                Number = number
            };
        }

        public static UserPhone Default() => new();

        public static UserPhone Map(
            int id,
            BaseStatus status,
            string number,
            PhoneType phoneType,
            DateTime createdAt,
            DateTime modifiedAt)
        {
            return new UserPhone()
            {
                Id = id,
                Status = status,
                Number = number,
                PhoneType = phoneType,
                CreatedAt = createdAt,
                ModifiedAt = modifiedAt
            };
        }
    }
}
