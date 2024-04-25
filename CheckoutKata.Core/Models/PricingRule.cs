namespace CheckoutKata.Core.Models
{
    public class PricingRule
    {
        public string SKU { get; private set; }
        public double UnitPrice { get; set; }
        public SpecialPrice? SpecialPrice { get; set; }


        public PricingRule(string sku, int unitPrice, SpecialPrice? specialPrice = null)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            SpecialPrice = specialPrice;
        }
    }
}