using Microsoft.EntityFrameworkCore;
using VPOrderProcessor.Domain.Customers;
using VPOrderProcessor.Domain.Orders;
using VPOrderProcessor.Domain.OrderProducts;
using VPOrderProcessor.Domain.Products;

namespace VPOrderProcessor.Infrastructure
{
    public sealed class VPOrderProcessorDbContext : DbContext
    {
        public VPOrderProcessorDbContext(
            DbContextOptions<VPOrderProcessorDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(VPOrderProcessorDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
