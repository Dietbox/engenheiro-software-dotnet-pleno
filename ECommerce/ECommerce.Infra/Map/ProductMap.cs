using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).HasColumnName("ProductId").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.BarCode).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.LastUpdate).IsRequired();
            builder.HasOne(x => x.Company);
        }
    }
}
