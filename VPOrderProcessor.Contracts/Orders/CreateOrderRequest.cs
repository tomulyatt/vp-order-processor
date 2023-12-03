namespace VPOrderProcessor.Contracts.Orders
{
    public record CreateOrderRequest(
        string ExternalOrderId,
        DateTime OrderCreatedDateTime,
        OrderPrice OrderTotal,
        Customer Customer,
        List<Product> Products,
        List<CreateOrderPayment>? Payments);

    public record OrderPrice(long Amount, string CurrencyId);
    public record Customer(string Forename, string Surname, string Email, string TelephoneNumber, Address CustomerAddress);
    public record Address(string Street1, string? Street2, string? Street3, string PostCode, string City, string Country);
    public record Product(string ProductName, double ProductQuantity, string ProductCode, string ProductSKU, 
        ProductPrice UnitPrice, ProductPrice SellPrice, ProductPrice UnitTax, ProductPrice ItemTax);
    public record ProductPrice(long Amount, string CurrencyId);
    public record CreateOrderPayment(long Amount, string MethodOfPaymentId);
}
