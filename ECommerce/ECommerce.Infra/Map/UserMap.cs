using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).HasColumnName("UserId").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.FullName).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.Birthday);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.LastUpdate).IsRequired();
            builder.HasOne(x => x.ApplicationUser);
        }
    }
}
