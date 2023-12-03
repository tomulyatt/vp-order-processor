using Microsoft.Extensions.DependencyInjection;
using VPOrderProcessor.Application.Orders;
using VPOrderProcessor.Application.Orders.Mappers;

namespace VPOrderProcessor.Application
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderProcessor, OrderProcessor>();

            services.AddScoped<IOrderValidator, OrderValidator>();
            services.AddScoped<IOrderProductValidator, OrderProductValidator>();
            services.AddScoped<IOrderCustomerValidator, OrderCustomerValidator>();

            services.AddScoped<IOrderMapper, OrderMapper>();


            return services;
        }
    }
}
