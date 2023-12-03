using VPOrderProcessor.Application.Common.Persistence;
using VPOrderProcessor.Contracts.Orders;

namespace VPOrderProcessor.Application.Orders
{
    public class OrderCustomerValidator : IOrderCustomerValidator
    {
        private readonly ICustomerRepository _customerRepository;

        public OrderCustomerValidator(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ValidateOrderCustomerAsync(Customer customer, CancellationToken cancellationToken)
        {
            await ValidateCustomerByEmailAddressAsync(customer.Email, cancellationToken);

            //May also want to do some validation based on the given customer address, related to the given order
            //IE, does the web store we have ordered from allow fulfillment to the customers deliver address
        }


        private async Task ValidateCustomerByEmailAddressAsync(string emailAddress, CancellationToken cancelToken)
        {
            Domain.Customers.Customer? customer = await _customerRepository.GetCustomerByEmailAddressAsync(emailAddress, cancelToken);

            if(customer is null)
            {
                //In the case we don't have a recorded customer, we may want to either create an account, or reject the order as they're "anonymous"
                //Will depend on the current business context sign up flow
                //For simplicity, reject the order, but may want to inject a CustomerCreation process

                throw new Exception($"Customer with the email [{emailAddress}] does not exist in our system");
                //Above may want to be altered to obfuscate customer details. Depending on where the API is hosted, and how secure the related actions are
            }
        }
    }
}
