namespace VPOrderProcessor.Domain.Customers.ValueObjects
{
    public record Address(string Street1, string? Street2, string? Street3, string PostCode, string City, string Country);
}
