using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public interface IOrderCustomerValidator
    {
        Task ValidateOrderCustomerAsync(Customer customer, CancellationToken cancellationToken);
    }
}
