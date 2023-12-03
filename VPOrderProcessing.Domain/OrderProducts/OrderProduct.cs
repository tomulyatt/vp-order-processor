using VPOrderProcessor.Domain.OrderProducts.ValueObjects;

namespace VPOrderProcessor.Domain.OrderProducts
{
    public class OrderProduct
    {
        public OrderProductId ProductId { get; private set; }

        public string ProductName { get; private set; }

        //Have this as a double in the case you can sell sub-units of an item
        public double ProductQuantity { get; private set; }
        
        //Wraps the style information for a product
        public string ProductCode { get; private set; }

        //Contains extended colour and size information
        public string ProductSKU { get; private set; }

        
        //For the below elements (that handle price), the make up will be dependant on the given domain
        //May wish to encapsulate the tax information, not allowing this to be provided by the front-end
        //Or, the front-end can specify duty-free areas (airports, selling to business customers (for tax rebate) etc)
        //Need to also consider if we want to contain information about discounts / promotions / coupons

        //Depending on the domain, may not want to encapsulate the given currency data
        //Possibly assume the currency based on the given order, where the order is placed in
        //Or in the case the product is specific to a selling region (UK / ROI/ EU), based on a master stock tabled related to the product
        public Price UnitPrice { get; private set; }

        //Sell price of the entire line (unit * quantity)
        public Price SellPrice { get; private set; }

        public Price UnitTax { get; private set; }

        public Price ItemTax { get; private set; }


        private OrderProduct(
            OrderProductId productId, 
            string productName, 
            double productQuantity, 
            string productCode, 
            string productSKU, 
            Price unitPrice, 
            Price sellPrice, 
            Price unitTax, 
            Price itemTax
            )
        {
            ProductId = productId;
            ProductName = productName;
            ProductQuantity = productQuantity;
            ProductCode = productCode;
            ProductSKU = productSKU;
            UnitPrice = unitPrice;
            SellPrice = sellPrice;
            UnitTax = unitTax;
            ItemTax = itemTax;
        }

        public static OrderProduct Create(
            string productName, 
            double productQuantity,
            string productCode,
            string productSKU,
            Price unitPrice,
            Price sellPrice,
            Price unitTax,
            Price itemTax)
        {
            OrderProduct product = new(
                OrderProductId.CreateUnique(),
                productName,
                productQuantity,
                productCode,
                productSKU,
                unitPrice,
                sellPrice,
                unitTax,
                itemTax);

            return product;
        }

        private OrderProduct() { }
    }
}
