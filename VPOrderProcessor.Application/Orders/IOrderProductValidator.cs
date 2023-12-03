using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public interface IOrderProductValidator
    {
        public Task ValidateProductAsync(Product product, CancellationToken cancelToken);
    }
}
