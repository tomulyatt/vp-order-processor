namespace VPOrderProcessor.Domain.OrderPayments.ValueObject
{
    public sealed class OrderPaymentId
    {
        public Guid Value { get; private set; }

        private OrderPaymentId(Guid id)
        {
            Value = id;
        }

        public static OrderPaymentId Create(Guid id)
        {
            return new(id);
        }

        public static OrderPaymentId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}
