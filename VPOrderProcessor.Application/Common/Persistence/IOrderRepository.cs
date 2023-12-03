using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Application.Common.Persistence
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Task AddAsync(Order order, CancellationToken cancelToken);
        Task<Order?> GetOrderByExternalOrderIdAsync(string customerOrderId, CancellationToken cancelToken);
    }
}
