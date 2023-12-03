namespace VPOrderProcessor.Domain.Orders.ValueObjects
{
    public sealed class OrderId
    {
        public Guid Value { get; private set; }

        private OrderId(Guid id)
        {
            Value = id;
        }

        public static OrderId Create(Guid id)
        {
            return new(id);
        }

        public static OrderId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}
