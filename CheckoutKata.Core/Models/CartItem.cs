namespace CheckoutKata.Core.Models
{
    public class CartItem
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}