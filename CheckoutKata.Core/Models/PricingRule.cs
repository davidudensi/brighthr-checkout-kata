namespace CheckoutKata.Core.Models
{
    public class PricingRule
    {
        public char SKU { get; set; }
        public int UnitPrice { get; set; }
        private bool HasSpecialPrice { get; set; } = false;
        public SpecialPrice? SpecialPrice { get; set; }

        public PricingRule()
        {
            HasSpecialPrice = SpecialPrice != null;
        }
    }
}