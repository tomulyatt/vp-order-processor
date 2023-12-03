using VPOrderProcessor.Domain.Products.ValueObjects;

namespace VPOrderProcessor.Domain.Products
{
    /// <summary>
    /// Master stock-product level information
    /// </summary>
    public class Product
    {
        public ProductId ProductId { get; private set; }

        public string ProductSKU { get; private set; }
        public string ProductName { get; private set;}

        //May want to extend to include tax, ticket price information & fulfillment data

        private Product(
            ProductId productId,
            string productSKU, 
            string productName)
        {
            ProductId = productId;
            ProductSKU = productSKU;
            ProductName = productName;
        }


        public static Product Create(
            string productSku,
            string productName)
        {
            Product product = new Product(
                ProductId.CreateUnique(),
                productSku,
                productName);

            return product;
        }
    }
}
