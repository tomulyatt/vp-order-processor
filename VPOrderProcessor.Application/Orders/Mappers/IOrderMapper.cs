using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders.Mappers
{
    public interface IOrderMapper
    {
        public Domain.Orders.Order ToDomainOrder(CreateOrderRequest orderRequest);
    }
}
