using VPOrderProcessor.Contracts.Orders;
using VPOrderProcessor.Domain.OrderPayments;
using VPOrderProcessor.Domain.OrderProducts;
using VPOrderProcessor.Domain.Orders;
using VPOrderProcessor.Domain.Orders.ValueObjects;

namespace VPOrderProcessor.Application.Orders.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public Order ToDomainOrder(CreateOrderRequest orderRequest)
        {
            Order domainOrder = Order.Create(
                orderRequest.ExternalOrderId,
                orderRequest.OrderCreatedDateTime,
                ToDomainOrderPrice(orderRequest.OrderTotal),
                ToDomainCustomer(orderRequest.Customer),
                orderRequest.Products.Select(p => ToDomainOrderProduct(p)).ToList(),
                orderRequest.Payments?.Select(p => ToDomainOrderPayment(p)).ToList() ?? null);

            return domainOrder;
        }

        public Price ToDomainOrderPrice(OrderPrice orderPrice)
        {
            return new Price(orderPrice.Amount, orderPrice.CurrencyId);
        }


        private OrderProduct ToDomainOrderProduct(Product requestProduct)
        {
            return OrderProduct.Create(
                requestProduct.ProductName,
                requestProduct.ProductQuantity,
                requestProduct.ProductCode,
                requestProduct.ProductSKU,
                ToOrderProductPrice(requestProduct.UnitPrice),
                ToOrderProductPrice(requestProduct.SellPrice),
                ToOrderProductPrice(requestProduct.UnitTax),
                ToOrderProductPrice(requestProduct.ItemTax));
        }

        private Domain.OrderProducts.ValueObjects.Price ToOrderProductPrice(ProductPrice requestProductPrice)
        {
            return new Domain.OrderProducts.ValueObjects.Price(requestProductPrice.Amount, requestProductPrice.CurrencyId);
        }

        private OrderPayment ToDomainOrderPayment(CreateOrderPayment requestpayment)
        {
            return OrderPayment.Create(requestpayment.Amount, requestpayment.MethodOfPaymentId);
        }

        private Domain.Customers.Customer ToDomainCustomer(Customer requestCustomer)
        {
            return Domain.Customers.Customer.Create(
                requestCustomer.Forename,
                requestCustomer.Surname,
                requestCustomer.Email,
                requestCustomer.TelephoneNumber,
                ToDomainAddress(requestCustomer.CustomerAddress));
        }

        private Domain.Customers.ValueObjects.Address ToDomainAddress(Address address)
        {
            return new Domain.Customers.ValueObjects.Address(
                address.Street1,
                address.Street2,
                address.Street3,
                address.PostCode,
                address.City,
                address.Country);
        }
    }
}
