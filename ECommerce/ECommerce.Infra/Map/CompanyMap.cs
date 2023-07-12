using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Map
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Id).HasColumnName("CompanyId").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.TradingName).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.TaxId).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.LastUpdate).IsRequired();
            builder.HasOne(x => x.ApplicationUser);
        }
    }
}
