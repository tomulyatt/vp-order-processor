using VPOrderProcessor.Domain.Customers.ValueObjects;

namespace VPOrderProcessor.Domain.Customers
{
    public class Customer
    {
        public CustomerId CustomerId { get; private set; }
        public string Forename { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string TelephoneNumber { get; private set; }
        public Address CustomerAddress { get; private set; }

        private Customer(
            CustomerId customerId,
            string forename,
            string surname,
            string email,
            string telephoneNumber,
            Address customerAddress)
        {
            CustomerId = customerId;
            Forename = forename;
            Surname = surname;
            Email = email;
            TelephoneNumber = telephoneNumber;
            CustomerAddress = customerAddress;
        }

        public static Customer Create(
            string forename,
            string surname,
            string email,
            string telephoneNumber,
            Address customerAddress)
        {
            Customer customer = new Customer(CustomerId.CreateUnique(), forename, surname, email, telephoneNumber, customerAddress);
            return customer;
        }

        private Customer() { }
    }
}
