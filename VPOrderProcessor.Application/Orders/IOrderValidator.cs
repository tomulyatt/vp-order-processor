using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public interface IOrderValidator
    {
        Task ValidateOrderRequestAsync(CreateOrderRequest createOrderRequest, CancellationToken cancelToken);
    }
}