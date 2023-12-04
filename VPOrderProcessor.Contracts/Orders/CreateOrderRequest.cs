using System.ComponentModel.DataAnnotations;

namespace VPOrderProcessor.Contracts.Orders
{
    public record CreateOrderRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string ExternalOrderId { get; init; }

        [Required]
        public DateTime OrderCreatedDateTime { get; init; }

        [Required]
        public OrderPrice OrderTotal { get; init; }

        [Required]
        public Customer Customer { get; init; }

        [Required]
        public List<Product> Products { get; init; }
        public List<CreateOrderPayment>? Payments { get; init; }
    }

    public record OrderPrice 
    {
        [Range(0, int.MaxValue)]
        public long Amount { get; init; }

        [Required(AllowEmptyStrings =false)]
        public string CurrencyId { get; init; }
    }
    public record Customer
    {
        [Required(AllowEmptyStrings = false)]
        public string Forename { get; init; }


        [Required(AllowEmptyStrings = false)]
        public string Surname { get; init; }


        [Required(AllowEmptyStrings = false)]
        public string Email { get; init; }


        [Required(AllowEmptyStrings = false)]
        public string TelephoneNumber { get; init; }
        public Address CustomerAddress { get; init; }
    }
    public record Address
    {
        [Required(AllowEmptyStrings = false)]
        public string Street1 { get; init; }
        public string? Street2 { get; init; }
        public string? Street3 { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string PostCode { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string City { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string Country { get; init; }    
    }
    public record Product
    {
        [Required(AllowEmptyStrings = false)]
        public string ProductName { get; init; }

        [Range(0, double.MaxValue)]
        public double ProductQuantity { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string ProductCode { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string ProductSKU { get; init; }
        public ProductPrice UnitPrice { get; init; }
        public ProductPrice SellPrice { get; init; }
        public ProductPrice UnitTax { get; init; }
        public ProductPrice ItemTax { get; init; }
    }
    
    
    public record ProductPrice
    {
        [Range(0, int.MaxValue)]
        public long Amount { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string CurrencyId { get; init; }
    }
    public record CreateOrderPayment
    {
        [Range(0, int.MaxValue)]
        public long Amount { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string MethodOfPaymentId { get; init; }
    }
}
