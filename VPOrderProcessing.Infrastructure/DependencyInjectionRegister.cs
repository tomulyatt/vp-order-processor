using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Infrastructure.Customers;
using VPOrderProcessor.Infrastructure.Options;
using VPOrderProcessor.Infrastructure.Orders;
using VPOrderProcessor.Infrastructure.Products;
using VPOrderProcessor.Infrastructure.Services;

namespace VPOrderProcessor.Infrastructure
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>();

            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.ConfigBinding));

            services.AddPersistence();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<VPOrderProcessorDbContext>(optionsAction: (serviceProvider, options) =>
            {
                IConnectionStringBuilder connectionStringBuilder = serviceProvider.GetRequiredService<IConnectionStringBuilder>();
                IOptions<ConnectionStringOptions> connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>();

                string connectionString = connectionStringBuilder.GiveConnectionString(connectionOptions.Value);

                options.UseSqlServer(connectionString);

            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}