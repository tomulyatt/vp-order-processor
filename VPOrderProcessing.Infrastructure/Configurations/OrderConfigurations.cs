using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPOrderProcessor.Domain.OrderPayments.ValueObject;
using VPOrderProcessor.Domain.OrderProducts.ValueObjects;
using VPOrderProcessor.Domain.Orders;
using VPOrderProcessor.Domain.Orders.ValueObjects;

namespace VPOrderProcessor.Infrastructure.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigureOrdersTable(builder);
            ConfigureOrderProductsTable(builder);
            ConfigureOrderPaymentsTable(builder);
        }

        private static void ConfigureOrdersTable(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("Orders");

            builder.HasKey(x => x.OrderId);

            builder.Property(o => o.OrderId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                value => OrderId.Create(value));

            builder.OwnsOne(o => o.OrderTotal);
        }

        private static void ConfigureOrderProductsTable(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsMany(o => o.Products, orderProductsBuilder =>
            {
                orderProductsBuilder.ToTable("OrderProducts");

                orderProductsBuilder.WithOwner()
                    .HasForeignKey("OrderId");

                orderProductsBuilder.Property(p => p.ProductId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                    value => OrderProductId.Create(value));

                orderProductsBuilder.OwnsOne(p => p.UnitPrice);
                orderProductsBuilder.OwnsOne(p => p.SellPrice);
                orderProductsBuilder.OwnsOne(p => p.UnitTax);
                orderProductsBuilder.OwnsOne(p => p.ItemTax);

            });

            builder.Metadata
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureOrderPaymentsTable(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsMany(o => o.Payments, orderPaymentsBuilder =>
            {
                orderPaymentsBuilder.ToTable("OrderPayments");

                orderPaymentsBuilder.WithOwner()
                    .HasForeignKey("OrderId");

                orderPaymentsBuilder.Property(p => p.OrderPaymentId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                    value => OrderPaymentId.Create(value));

            });
        }
    }
}
