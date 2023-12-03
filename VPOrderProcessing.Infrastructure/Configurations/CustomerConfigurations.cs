using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPOrderProcessor.Domain.Customers;
using VPOrderProcessor.Domain.Customers.ValueObjects;

namespace VPOrderProcessor.Infrastructure.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ConfigureCustomersTable(builder);
        }

        private static void ConfigureCustomersTable(EntityTypeBuilder<Customer> builder)
        {
            builder
                .ToTable("Customers");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                value => CustomerId.Create(value));

            builder.OwnsOne(c => c.CustomerAddress);
        }
    }
}
