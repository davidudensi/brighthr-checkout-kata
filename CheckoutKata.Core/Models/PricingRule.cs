namespace CheckoutKata.Core.Models
{
    public class PricingRule
    {
        public string SKU { get; private set; }
        public int UnitPrice { get; private set; }
        public bool HasSpecialPrice { get; private set; }
        public SpecialPrice? SpecialPrice { get; private set; }

        public PricingRule(string sku, int unitPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            HasSpecialPrice = false;
        }

        public PricingRule(string sku, int unitPrice, SpecialPrice specialPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            SpecialPrice = specialPrice;
            HasSpecialPrice = SpecialPrice != null;
        }
    }
}