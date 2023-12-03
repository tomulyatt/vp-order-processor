namespace VPOrderProcessor.Domain.Products.ValueObjects
{
    public sealed class ProductId
    {
        public Guid Value { get; private set; }

        private ProductId(Guid id)
        {
            Value = id;
        }

        public static ProductId Create(Guid id)
        {
            return new(id);
        }

        public static ProductId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}
