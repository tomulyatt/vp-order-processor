using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public class OrderProductValidator : IOrderProductValidator
    {
        private readonly IProductRepository _productRepository;

        public OrderProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task ValidateProductAsync(Product product, CancellationToken cancelToken)
        {
            await ValidateProductSkuExistsAsync(product.ProductSKU, cancelToken);
            ValidateProductPriceValues(product);
        }

        private async Task ValidateProductSkuExistsAsync(string productSku, CancellationToken cancelToken)
        {
            Domain.Products.Product? product = await _productRepository.GetByProductSKUAsync(productSku, cancelToken);

            if (product is null)
            {
                //Can't find product code in our master stock table, can't place an order for unknown product, throw error back to client.
                //Depending on business case, may wish to capture partial order and refund back to the customer via a payment gateway
                throw new Exception($"Unable to validate product information against SKU code: [{productSku}]");
            }
        }

        private void ValidateProductPriceValues(Product product)
        {
            //Most of this validation will be based on business context, and how the prices are evaluated with / without tax values + discounts
            double calculatedSellPrice = product.UnitPrice.Amount * product.ProductQuantity;

            if (calculatedSellPrice != product.SellPrice.Amount)
            {
                throw new Exception("Calculated sell price does not match products recorded sale amount." +
                    $"Calculated sell price: [{calculatedSellPrice}] Actual sale price: [{product.SellPrice.Amount}]");
            }
            //May wish to have additional validation based on tax values, or maximum discount rates tied to the product here
        }
    }
}
