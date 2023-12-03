using FluentAssertions;
using TestUtilities;
using VPOrderProcessor.Application.Orders.Mappers;
using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.UnitTests
{
    public class OrderMapperTests
    {
        private readonly IOrderMapper _orderMapper;

        public OrderMapperTests()
        {
            _orderMapper = new OrderMapper();
        }

        [Fact]
        public void ShouldReturnValidDomainOrder_GivenValidOrderRequest_WithNoPayment()
        {
            CreateOrderRequest testRequest = CreateOrderRequestUtilities.CreateOrderRequest();

            var domainOrder = _orderMapper.ToDomainOrder(testRequest);

            domainOrder.Should().NotBeNull();
            domainOrder.Products.Should().NotBeNull().And.HaveCount(1);
            domainOrder.Payments.Should().NotBeNull().And.HaveCount(0);
            domainOrder.ExternalOrderId.Should().BeSameAs(testRequest.ExternalOrderId);
            domainOrder.Customer.Should().NotBeNull();
        }

        [Fact]
        public void ShouldReturnValidDomainOrder_GivenValidOrderRequest_WithSinglePayment()
        {
            CreateOrderRequest testRequest = CreateOrderRequestUtilities.CreateOrderRequest(orderPayments: CreateOrderRequestUtilities.CreateOrderPayments());

            var domainOrder = _orderMapper.ToDomainOrder(testRequest);

            domainOrder.Should().NotBeNull();
            domainOrder.Products.Should().NotBeNull().And.HaveCount(1);
            domainOrder.Payments.Should().NotBeNull().And.HaveCount(1);
            domainOrder.ExternalOrderId.Should().BeSameAs(testRequest.ExternalOrderId);
            domainOrder.Customer.Should().NotBeNull();
        }
    }
}