using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public class OrderValidator : IOrderValidator
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductValidator _orderProductValidator;
        private readonly IOrderCustomerValidator _orderCustomerValidator;
        public OrderValidator(IOrderRepository orderRepository,
            IOrderProductValidator orderProductValidator,
            IOrderCustomerValidator orderCustomerValidator)
        {
            _orderRepository = orderRepository;
            _orderProductValidator = orderProductValidator;
            _orderCustomerValidator = orderCustomerValidator;
        }

        public async Task ValidateOrderRequestAsync(CreateOrderRequest createOrderRequest, CancellationToken cancelToken)
        {
            //Ensure we don't have an order with the same customer-facing ID
            await ValidateExternalOrderIdAsync(createOrderRequest.ExternalOrderId, cancelToken);

            await ValidateOrderTotals(createOrderRequest, cancelToken);

            await ValidateOrderProductsAsync(createOrderRequest.Products, cancelToken);

            await ValidateCustomerAsync(createOrderRequest.Customer, cancelToken);

        }

        private async Task ValidateExternalOrderIdAsync(string externalOrderId, CancellationToken cancelToken)
        {
            Order? order = await _orderRepository.GetOrderByExternalOrderIdAsync(externalOrderId, cancelToken);

            if (order is not null)
            {
                throw new ArgumentException($"External order ID already exists. ID used: [{externalOrderId}]");
            }
        }

        private Task ValidateOrderTotals(CreateOrderRequest createOrderRequest, CancellationToken cancelToken)
        {
            long orderTotalAmount = createOrderRequest.OrderTotal.Amount;
            long orderPaymentAggregate = createOrderRequest.Payments?.Sum(p => p.Amount) ?? 0;

            //May need to consider here some type of currency conversion process. 
            //Depending on domain, may wish to preserver rounding vales from the front-end (determined on basket), or from the back-end process
            //Will also need extension if we want to support deposits
            if (orderPaymentAggregate.Equals(orderTotalAmount) == false)
            {
                throw new Exception($"Order total and order payment do not calculate. " +
                    $"Payment total: [{orderPaymentAggregate}] Order total: [{orderTotalAmount}]");
            }

            return Task.CompletedTask;
        }

        private async Task ValidateOrderProductsAsync(List<Product> products, CancellationToken cancelToken)
        {
            //Depending on code standards, might want to have some type of validation director, which calls to all sub-validators
            foreach(Product product in products)
            {
                await _orderProductValidator.ValidateProductAsync(product, cancelToken);
            }
        }

        private async Task ValidateCustomerAsync(Customer customer, CancellationToken cancelToken)
        {
            await _orderCustomerValidator.ValidateOrderCustomerAsync(customer, cancelToken);
        }
    }
}
