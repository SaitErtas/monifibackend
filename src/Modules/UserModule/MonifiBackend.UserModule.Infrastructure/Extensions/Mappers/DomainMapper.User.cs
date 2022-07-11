using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Data.Infrastructure.Entities;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Users.Phones;

namespace MonifiBackend.UserModule.Infrastructure.Extensions.Mappers
{
    public static partial class DomainMapper
    {
        #region User to UserEntity 
        public static UserEntity Map(this User domain)
        {
            var contacts = domain.Phones != null ? domain.Phones.Select(x => x.Map()).ToList() : null;

            return new UserEntity()
            {
                Id = domain.Id,
                Email = domain.Email,
                Password = domain.Password,
                Terms = domain.Terms,
                Status = domain.Status.ToInt(),
                CreatedAt = domain.CreatedAt,
                ModifiedAt = domain.ModifiedAt,
                Phones = contacts,
                Role = domain.Role.ToInt(),
                ResetPasswordCode = domain.ResetPasswordCode,
                ReferanceUser = domain.ReferanceUser,
                ConfirmationCode = domain.ConfirmationCode,
                ReferanceCode = domain.ReferanceCode,
            };
        }
        #endregion
        #region UserEntity to User 
        public static User Map(this UserEntity entity)
        {
            if (entity == null)
                return User.Default();

            var contacts = entity.Phones != null ? entity.Phones.Select(x => x.Map()).ToList() : new List<UserPhone>();

            return User.Map(entity.Id,
                entity.Status.ToEnum<BaseStatus>(),
                entity.Email,
                entity.Password,
                entity.Username,
                entity.Terms,
                entity.ResetPasswordCode,
                entity.ReferanceUser,
                entity.ReferanceCode,
                entity.ConfirmationCode,
                entity.CreatedAt,
                entity.ModifiedAt,
                entity.Role.ToEnum<Role>(),
                contacts);
        }
        #endregion
    }
}
