using VPOrderProcessor.Domain.Products;

namespace VPOrderProcessor.Application.Common.Persistence
{
    public interface IProductRepository
    {
        Task<Product?> GetByProductSKUAsync(string productSku, CancellationToken cancelToken);
    }
}
