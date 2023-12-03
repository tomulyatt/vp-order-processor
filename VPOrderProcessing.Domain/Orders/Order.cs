using VPOrderProcessor.Domain.Customers;
using VPOrderProcessor.Domain.Orders.ValueObjects;
using VPOrderProcessor.Domain.OrderProducts;
using VPOrderProcessor.Domain.OrderPayments;

namespace VPOrderProcessor.Domain.Orders
{
    public class Order
    {
        public OrderId OrderId { get; private set; }
        public string ExternalOrderId { get; private set; }
        public DateTime OrderCreatedDateTime { get; private set; }
        public Price OrderTotal { get; private set;}
        public Customer Customer { get; private set; }
        public IReadOnlyCollection<OrderProduct> Products { get; private set; }

        //may need to consider some type of validation for deposits, depending on business context
        public IReadOnlyCollection<OrderPayment> Payments { get; private set; }

        private Order(
            OrderId orderId,
            string externalOrderId,
            DateTime orderCreatedDateTime,
            Price orderTotal,
            Customer customer,
            List<OrderProduct> products,
            List<OrderPayment> payments)
        {
            OrderId = orderId;
            ExternalOrderId = externalOrderId;
            OrderCreatedDateTime = orderCreatedDateTime;
            OrderTotal = orderTotal;
            Customer = customer;
            Products = products.AsReadOnly();
            Payments = payments.AsReadOnly();
        }

        public static Order Create(
            string externalOrderId,
            DateTime orderCreatedDateTime,
            Price orderTotal,
            Customer customer,
            List<OrderProduct> products,
            List<OrderPayment>? payments)
        {
            Order order = new Order(
                OrderId.CreateUnique(),
                externalOrderId,
                orderCreatedDateTime,
                orderTotal,
                customer,
                products,
                payments ?? new List<OrderPayment>());

            return order;
        }

        private Order()
        {

        }
    }
}