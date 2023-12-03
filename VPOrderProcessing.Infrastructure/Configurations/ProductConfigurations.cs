using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPOrderProcessor.Domain.Products;
using VPOrderProcessor.Domain.Products.ValueObjects;

namespace VPOrderProcessor.Infrastructure.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigureProductsTable(builder);
        }

        private static void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                value => ProductId.Create(value));
        }
    }
}
