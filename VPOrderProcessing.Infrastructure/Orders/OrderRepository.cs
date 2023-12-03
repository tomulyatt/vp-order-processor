using Microsoft.EntityFrameworkCore;
using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Infrastructure.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly VPOrderProcessorDbContext _dbContext;

        //Used for local test purposes only
        private readonly List<Order> _orders = new List<Order>();

        public OrderRepository(VPOrderProcessorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order?> GetOrderByExternalOrderIdAsync(string customerOrderId, CancellationToken cancelToken)
        {
            //return await _dbContext.Orders
            //    .SingleOrDefaultAsync(o => o.ExternalOrderId == customerOrderId, cancelToken);

            return await Task.FromResult(_orders.SingleOrDefault(o => o.ExternalOrderId == customerOrderId));
        }

        public void Add(Order order)
        {
            //_dbContext.Add(order);
            //_dbContext.SaveChanges();

            _orders.Add(order);
        }

        public async Task AddAsync(Order order, CancellationToken cancelToken)
        {
            //await _dbContext.AddAsync(order, cancelToken);
            //await _dbContext.SaveChangesAsync(cancelToken);

            _orders.Add(order);
            await Task.CompletedTask;
        }
    }
}
