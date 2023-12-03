namespace VPOrderProcessor.Domain.Customers.ValueObjects
{
    public sealed class CustomerId
    {
        public Guid Value { get; private set; }

        private CustomerId(Guid id)
        {
            Value = id;
        }

        public static CustomerId Create(Guid id)
        {
            return new(id);
        }

        public static CustomerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}
