namespace CheckoutKata.Core.Models
{
    public class PricingRule
    {
        public string SKU { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public bool HasSpecialPrice { get; private set; }
        public SpecialPrice? SpecialPrice { get; private set; }

        public PricingRule() { }
        public PricingRule(SpecialPrice specialPrice)
        {
            SpecialPrice = specialPrice;
            HasSpecialPrice = SpecialPrice != null;
        }
    }
}