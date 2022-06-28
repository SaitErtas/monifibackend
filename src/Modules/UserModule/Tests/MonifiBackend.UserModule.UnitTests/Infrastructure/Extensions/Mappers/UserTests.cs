using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Data.Infrastructure.Entities;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Users.Phones;
using MonifiBackend.UserModule.Infrastructure.Extensions.Mappers;
using Xunit;

namespace MonifiBackend.UserModule.UnitTests.Infrastructure.Extensions.Mappers
{
    public class UserTests
    {
        [Fact]
        public void Map_Should_Return_DefaultObject_When_DomainIsNull()
        {
            var entity = default(UserEntity);
            var domain = entity.Map();

            Assert.False(domain.IsExist());
        }
        [Fact]
        public void Map_Should_Return_ValidEntityObject()
        {
            var phones = new List<UserPhoneEntity>
            {
                new UserPhoneEntity
                {
                    Number ="55555555",
                    PhoneType= 1,
                    Status = BaseStatus.Active.ToInt(),
                    UserId = 1,
                }
            };

            UserEntity entity = new UserEntity
            {
                Id = 1,
                Name = "Hakan",
                Surname = "GÜZEL",
                Phones = phones,
                ModifiedAt = new DateTime(2022, 01, 03),
                CreatedAt = new DateTime(2022, 01, 03),
                Status = BaseStatus.Active.ToInt()
            };
            var domain = entity.Map();

            Assert.Equal(domain.Id, entity.Id);
            Assert.Equal(domain.Name, entity.Name);
            Assert.Equal(domain.Surname, entity.Surname);
            Assert.Equal(domain.CreatedAt, entity.CreatedAt);
            Assert.Equal(domain.ModifiedAt, entity.ModifiedAt);
            Assert.Equal(domain.Status.ToInt(), entity.Status);
            Assert.Equal(domain.Phones.Count, entity.Phones.Count);
        }
        [Fact]
        public void Map_Should_Contact_Null_Return_ValidEntityObject()
        {
            UserEntity entity = new UserEntity
            {
                Id = 1,
                Name = "Hakan",
                Surname = "GÜZEL",
                Phones = null,
                ModifiedAt = new DateTime(2022, 01, 03),
                CreatedAt = new DateTime(2022, 01, 03),
                Status = BaseStatus.Active.ToInt()
            };
            var domain = entity.Map();

            Assert.Equal(domain.Id, entity.Id);
            Assert.Equal(domain.Name, entity.Name);
            Assert.Equal(domain.Surname, entity.Surname);
            Assert.Equal(domain.CreatedAt, entity.CreatedAt);
            Assert.Equal(domain.ModifiedAt, entity.ModifiedAt);
            Assert.Equal(domain.Status.ToInt(), entity.Status);
        }
        [Fact]
        public void Map_Should_Return_ValidDomainObject()
        {
            var domain = User.CreateNew(
                email: "hakan-guzel@outlook.com",
                password: "123456",
                name: "Hakan",
                surname: "GÜZEL",
                role: Role.Administrator,
                status: BaseStatus.Active);
            domain.AddPhone("5555555555", PhoneType.Mobile);
            var entity = domain.Map();

            Assert.Equal(domain.Name, entity.Name);
            Assert.Equal(domain.Surname, entity.Surname);
            Assert.Equal(domain.CreatedAt, entity.CreatedAt);
            Assert.Equal(domain.ModifiedAt, entity.ModifiedAt);
            Assert.Equal(domain.Status.ToInt(), entity.Status);
            Assert.Equal(domain.Phones.Count, entity.Phones.Count);
        }
        [Fact]
        public void Map_Should_Contact_Null_Return_ValidDomainObject()
        {
            var domain = User.CreateNew(
                email: "hakan-guzel@outlook.com",
                password: "123456",
                name: "Hakan",
                surname: "GÜZEL",
                role: Role.Administrator,
                status: BaseStatus.Active);
            var entity = domain.Map();

            Assert.Equal(domain.Name, entity.Name);
            Assert.Equal(domain.Surname, entity.Surname);
            Assert.Equal(domain.CreatedAt, entity.CreatedAt);
            Assert.Equal(domain.ModifiedAt, entity.ModifiedAt);
            Assert.Equal(domain.Status.ToInt(), entity.Status);
            Assert.Equal(domain.Phones.Count, entity.Phones.Count);
        }
    }
}
