using VPOrderProcessor.Domain.Customers;

namespace VPOrderProcessor.Application.Common.Persistence
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetCustomerByEmailAddressAsync(string emailAddress, CancellationToken cancelToken);
    }
}
