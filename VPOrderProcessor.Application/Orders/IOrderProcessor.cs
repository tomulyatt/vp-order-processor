using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public interface IOrderProcessor
    {
        public Task<Order> ProcessOrderRequestAsync(CreateOrderRequest request, CancellationToken cancellationToken);
    }
}
