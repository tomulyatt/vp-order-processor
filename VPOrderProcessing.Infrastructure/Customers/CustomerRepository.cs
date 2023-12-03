using Microsoft.EntityFrameworkCore;
using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Domain.Customers;

namespace VPOrderProcessor.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly VPOrderProcessorDbContext _dbContext;
        
        private readonly IList<Customer> _customers = new List<Customer>();

        public CustomerRepository(VPOrderProcessorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> GetCustomerByEmailAddressAsync(string emailAddress, CancellationToken cancelToken)
        {
            //return await _dbContext.Customers
            //    .SingleOrDefaultAsync(c => c.Email ==  emailAddress, cancelToken);

            return await Task.FromResult(_customers.SingleOrDefault(c => c.Email == emailAddress));
        }
    }
}
