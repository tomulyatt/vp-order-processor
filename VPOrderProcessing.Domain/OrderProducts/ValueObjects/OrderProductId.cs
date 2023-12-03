namespace VPOrderProcessor.Domain.OrderProducts.ValueObjects
{
    public sealed class OrderProductId
    {
        public Guid Value { get; private set; }

        private OrderProductId(Guid id)
        {
            Value = id;
        }

        public static OrderProductId Create(Guid id)
        {
            return new(id);
        }

        public static OrderProductId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}
