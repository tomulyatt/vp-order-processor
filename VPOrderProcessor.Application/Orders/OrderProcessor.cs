using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Application.Orders.Mappers;
using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IOrderValidator _orderValidator;
        private readonly IOrderMapper _orderMapper;
        private readonly IOrderRepository _orderRepository;
        public OrderProcessor(
            IOrderValidator orderValidator,
            IOrderMapper orderMapper,
            IOrderRepository orderRepository)
        {
            _orderValidator = orderValidator;
            _orderMapper = orderMapper;
            _orderRepository = orderRepository;
        }


        public async Task<Order> ProcessOrderRequestAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            //Handle validation
            await _orderValidator.ValidateOrderRequestAsync(request, cancellationToken);

            //Transform to domain objects
            Order order = _orderMapper.ToDomainOrder(request);

            //It might be better to have the request object to have an "order" property, encapsulating details
            //This would be to allow for extension, passing through identity information to the request body
            await _orderRepository.AddAsync(order, cancellationToken);

            return order;
        }
    }
}