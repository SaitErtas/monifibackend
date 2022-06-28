using MonifiBackend.Core.Domain.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users.Phones;

namespace MonifiBackend.UserModule.Domain.Users
{
    public sealed class User : BaseActivityDomain<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ResetPasswordCode { get; private set; }
        public Role Role { get; private set; }

        private List<UserPhone> _phones = new();
        public IReadOnlyCollection<UserPhone> Phones => _phones.AsReadOnly();

        public void SetName(string name)
        {
            AppRule.NotNullOrEmpty<DomainException>(name, "Name Cannot Be Null Or Empty");
            Name = name;
        }
        public void SetResetPasswordCode(string resetPasswordCode)
        {
            ResetPasswordCode = resetPasswordCode;
        }

        public void SetSurname(string surname)
        {
            AppRule.NotNullOrEmpty<DomainException>(surname, "Surname Cannot Be Null Or Empty");
            Surname = surname;
        }
        public void SetEmail(string email)
        {
            AppRule.NotNullOrEmpty<DomainException>(email, "Email Cannot Be Null Or Empty");
            Name = email;
        }

        public void SetPassword(string password)
        {
            AppRule.NotNullOrEmpty<DomainException>(password, "Password Cannot Be Null Or Empty");
            Password = password;
        }
        public void SetRole(Role role) => Role = role;

        #region Phone
        public bool IsPhoneExists(string number, PhoneType phoneType) => _phones.Any(x => x.Number == number && x.PhoneType == phoneType);

        public void AddPhone(string number, PhoneType phoneType)
        {
            AppRule.False<DomainException>(IsPhoneExists(number, phoneType), "Phone Already Exists", $"Phone Already Exists. UserId: {Id}, Number: {number}");
            var phone = UserPhone.CreateNew(number, phoneType);
            _phones.Add(phone);
        }

        public void DeletePhone(string number, PhoneType phoneType)
        {
            AppRule.True<DomainException>(IsPhoneExists(number, phoneType), "Phone Not Exists", $"Phone Not Exists.  UserId: {Id}, Number: {number}");
            var phone = _phones.FirstOrDefault(x => x.Number == number && x.PhoneType == phoneType);
            phone.MarkAsDeleted();
        }
        #endregion

        public static User Default() => new();

        public static User CreateNew(
            string email,
            string password,
            string name,
            string surname,
            Role role,
            BaseStatus status)
        {
            AppRule.NotNullOrEmpty<DomainException>(email, "Email Cannot Be Null Or Empty", $"Email Cannot Be Null Or Empty. Email: {email}");
            AppRule.NotNullOrEmpty<DomainException>(password, "Password Cannot Be Null Or Empty", $"Password Cannot Be Null Or Empty. Password: {password}");
            AppRule.NotNullOrEmpty<DomainException>(name, "Name Cannot Be Null Or Empty", $"Name Cannot Be Null Or Empty. Name: {name}");
            AppRule.NotNullOrEmpty<DomainException>(surname, "Surname Cannot Be Null Or Empty", $"Surname Cannot Be Null Or Empty. Surname: {surname}");
            return new User()
            {
                Email = email,
                Password = password,
                Name = name,
                Surname = surname,
                Status = status,
                Role = role,
                ResetPasswordCode = string.Empty,
            };
        }
        public static User Map(
            int id,
            BaseStatus status,
            string email,
            string password,
            string name,
            string surname,
            string resetPassword,
            DateTime createdAt,
            DateTime modifiedAt,
            Role role,
            List<UserPhone> phones)
        {
            return new User()
            {
                Id = id,
                Status = status,
                Email = email,
                Password = password,
                Name = name,
                Surname = surname,
                CreatedAt = createdAt,
                ModifiedAt = modifiedAt,
                Role = role,
                ResetPasswordCode = resetPassword,
                _phones = phones,
            };
        }

    }
}
