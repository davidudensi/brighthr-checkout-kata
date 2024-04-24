namespace CheckoutKata.Core.Models
{
    public class CartItem
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public PricingRule Rule { get; set; }

        public CartItem(string sku, int quantity, double amount, PricingRule? rule = null)
        {
            SKU = sku;
            Quantity = quantity;
            Amount = amount;
        }
    }
}