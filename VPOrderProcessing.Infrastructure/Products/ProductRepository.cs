using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Domain.Products;

namespace VPOrderProcessor.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly VPOrderProcessorDbContext _dbContext;

        private readonly IList<Product> _products = new List<Product>();

        public ProductRepository(VPOrderProcessorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetByProductSKUAsync(string productSku, CancellationToken cancelToken)
        {
            //Consider caching this value in production, by adding to IMemoryCache for a period of time, to reduce calls to dB

            //return await _dbContext.Products
            //    .SingleOrDefaultAsync(p => p.ProductSKU == productSku, cancelToken);

            return await Task.FromResult(_products.SingleOrDefault(p => p.ProductSKU == productSku));
        }
    }
}
