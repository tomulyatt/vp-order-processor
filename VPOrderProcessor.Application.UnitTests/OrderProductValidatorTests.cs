using FluentAssertions;
using Moq;
using TestUtilities;
using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Application.Orders;
using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.UnitTests
{
    public class OrderProductValidatorTests
    {
        private IOrderProductValidator? _productValidator;

        [Fact]
        public async Task ShouldNotThrowException_GivenValidProductCode()
        {
            List<string> validProducts = new List<string>()
            {
                TestConstants.ProductSKU
            };

            _productValidator = new OrderProductValidator(GiveMockedProductRepository(validProducts).Object);

            Product product = CreateOrderRequestUtilities.CreateProducts(productSku: TestConstants.ProductSKU).First();

            await _productValidator.Invoking(v => v.ValidateProductAsync(product, default)).Should().NotThrowAsync();
        }

        [Fact]
        public async Task ShouldThrowException_GivenInvalidProductCode()
        {
            List<string> validProducts = new List<string>();
            _productValidator = new OrderProductValidator(GiveMockedProductRepository(validProducts).Object);


            Product product = CreateOrderRequestUtilities.CreateProducts(productSku: TestConstants.ProductSKU).First();

            await _productValidator.Invoking(v => v.ValidateProductAsync(product, default)).Should().ThrowAsync<Exception>();
        }

        private Mock<IProductRepository> GiveMockedProductRepository(List<string> validProducts)
        {
            Mock<IProductRepository> mockedProductRepo = new Mock<IProductRepository>();

            mockedProductRepo
                .Setup(r =>
                r.GetByProductSKUAsync(It.Is<string>(p => validProducts.Contains(p)), It.IsAny<CancellationToken>())
                ).ReturnsAsync(
                 Domain.Products.Product.Create("01234567890", "Test product"));

            return mockedProductRepo;
        }
    }
}
