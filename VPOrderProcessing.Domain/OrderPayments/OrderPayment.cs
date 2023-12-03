using VPOrderProcessor.Domain.OrderPayments.ValueObject;

namespace VPOrderProcessor.Domain.OrderPayments
{
    public class OrderPayment
    {
        public OrderPaymentId OrderPaymentId { get; private set; }

        public long PaymentAmount { get; private set; }
        public string MethodOfPaymentId { get; private set; }

        private OrderPayment(OrderPaymentId orderPaymentId, long paymentAmount, string methodOfPaymentId)
        {
            OrderPaymentId = orderPaymentId;
            PaymentAmount = paymentAmount;
            MethodOfPaymentId = methodOfPaymentId;
        }

        public static OrderPayment Create(long paymentAmount, string methodOfPaymentId)
        {
            return new OrderPayment(
                OrderPaymentId.CreateUnique(), 
                paymentAmount, 
                methodOfPaymentId);
        }

        private OrderPayment() { }
    }
}
