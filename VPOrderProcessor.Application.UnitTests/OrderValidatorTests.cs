using FluentAssertions;
using Moq;
using TestUtilities;
using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Application.Orders;
using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.Orders;

namespace VPOrderProcessor.Application.UnitTests
{
    public class OrderValidatorTests
    {
        private IOrderValidator? _orderValidator;

        [Fact]
        public async Task ShouldValidateOrder_GivenNewExternalOrderId()
        {
            _orderValidator = new OrderValidator(
                GiveMockedOrderRepository(new List<string>()).Object,
                GiveMockedOrderProductValidator().Object,
                GiveMockedOrderCustomerValidator().Object);

            CreateOrderRequest orderRequest = CreateOrderRequestUtilities.CreateOrderRequest(externalOrderId: Guid.NewGuid().ToString(), orderPayments: CreateOrderRequestUtilities.CreateOrderPayments());

            await _orderValidator.Invoking(v => v.ValidateOrderRequestAsync(orderRequest, default)).Should().NotThrowAsync();
        }

        [Fact]
        public async Task ShouldThrowException_GivenExistingExternalOrderId()
        {
            string externalOrderId = "Test-Order";

            _orderValidator = new OrderValidator(
                GiveMockedOrderRepository(new List<string>() { externalOrderId }).Object,
                GiveMockedOrderProductValidator().Object,
                GiveMockedOrderCustomerValidator().Object);

            CreateOrderRequest orderRequest = CreateOrderRequestUtilities.CreateOrderRequest(externalOrderId: externalOrderId, orderPayments: CreateOrderRequestUtilities.CreateOrderPayments());

            await _orderValidator.Invoking(v => v.ValidateOrderRequestAsync(orderRequest, default)).Should().ThrowAsync<Exception>();
        }




        private Mock<IOrderRepository> GiveMockedOrderRepository(List<string> alreadyCreatedOrderIds)
        {
            Mock<IOrderRepository> mockRepository = new Mock<IOrderRepository>();

            mockRepository.Setup(r =>
            r.GetOrderByExternalOrderIdAsync(It.Is<string>(x => alreadyCreatedOrderIds.Contains(x) == false), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Order?>(null));

            mockRepository.Setup(r =>
            r.GetOrderByExternalOrderIdAsync(It.Is<string>(x => alreadyCreatedOrderIds.Contains(x)), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Order?>(
                    Order.Create(
                        "",
                        DateTime.Now,
                        new(100, "GBP"),
                        Domain.Customers.Customer.Create(
                            "", "", "", "",
                            new("", "", "", "", "", "")),
                        new List<Domain.OrderProducts.OrderProduct>(),
                        new List<Domain.OrderPayments.OrderPayment>())));

            return mockRepository;
        }

        private Mock<IOrderProductValidator> GiveMockedOrderProductValidator()
        {
            Mock<IOrderProductValidator> mockValidator = new Mock<IOrderProductValidator>();

            mockValidator.Setup(v => v.ValidateProductAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return mockValidator;
        }

        private Mock<IOrderCustomerValidator> GiveMockedOrderCustomerValidator()
        {
            Mock<IOrderCustomerValidator> mockValidator = new Mock<IOrderCustomerValidator>();

            mockValidator.Setup(v => v.ValidateOrderCustomerAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return mockValidator;
        }
    }
}
