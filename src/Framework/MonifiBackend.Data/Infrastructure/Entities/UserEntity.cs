using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Domain.Entities.Configurations;

namespace MonifiBackend.Data.Infrastructure.Entities
{
    public class UserEntity : BaseActivityEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ResetPasswordCode { get; set; }
        public virtual int Role { get; set; }
        public virtual ICollection<UserPhoneEntity> Phones { get; set; }
    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.ResetPasswordCode).IsRequired();

            builder.HasMany(x => x.Phones).WithOne(x => x.User);

            BaseActivityConfiguration.Configure(builder);
        }
    }
}
