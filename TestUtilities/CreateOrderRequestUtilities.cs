using System.Globalization;
using VPOrderProcessor.Contracts.Orders;

namespace TestUtilities
{
    public static class CreateOrderRequestUtilities
    {
        public static CreateOrderRequest CreateOrderRequest(
            string? externalOrderId = null,
            DateTime? orderCreatedDate = null,
            OrderPrice? orderPrice = null,
            Customer? customer = null,
            List<Product>? orderProducts = null,
            List<CreateOrderPayment>? orderPayments = null)
            => 
            new CreateOrderRequest(
                externalOrderId ?? TestConstants.ExternalOrderId,
                orderCreatedDate ?? DateTime.Now,
                orderPrice ?? CreateOrderPrice(),
                customer ?? CreateCustomer(),
                orderProducts ?? CreateProducts(),
                orderPayments);

        public static OrderPrice CreateOrderPrice(long amount = 100, string currencyId = "GBP")
            => new OrderPrice(amount, currencyId);

        public static List<Product> CreateProducts(int productCount = 1, long productPrice = 100, string? productName = null, string? productCode = null, string? productSku = null, double productQuantity = 1)
            => Enumerable.Range(1, productCount)
            .Select(i => new Product(
                productName ?? TestConstants.ProductDescription,
                productQuantity,
                productCode ?? TestConstants.ProductCode,
                productSku ?? TestConstants.ProductSKU,
                new ProductPrice((long)(productPrice / productQuantity), TestConstants.SterlingCurrencyId),
                new ProductPrice(productPrice, TestConstants.SterlingCurrencyId),
                new ProductPrice(0, TestConstants.SterlingCurrencyId),
                new ProductPrice(0, TestConstants.SterlingCurrencyId)
                )).ToList();

        public static List<CreateOrderPayment> CreateOrderPayments(int paymentCount = 1, long paymentAmount = 100, string? currencyId = null)
            => Enumerable.Range(1, paymentCount)
            .Select(i => new CreateOrderPayment(paymentAmount, currencyId ?? TestConstants.SterlingCurrencyId)).ToList();


        public static Customer CreateCustomer(string? forename = null, string? surname = null, string? email = null, string? telephoneNumber = null, Address? address = null)
            => new Customer(
                forename ?? TestConstants.Forename, 
                surname ?? TestConstants.Surname, 
                email ?? TestConstants.Email, 
                telephoneNumber ?? TestConstants.TelephoneNumber, 
                address ?? CreateAddress()
                );

        public static Address CreateAddress(string? street1 = null, string? street2 = null, string? street3 = null, string? postCode = null, string? city = null, string? country = null)
            => new Address(
                street1 ?? TestConstants.Street1, 
                street2, 
                street3, 
                postCode ?? TestConstants.PostCode, 
                city ?? TestConstants.City, 
                country ?? TestConstants.Country
                );


    }
}