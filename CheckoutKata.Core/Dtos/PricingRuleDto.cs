using CheckoutKata.Core.Models;

namespace CheckoutKata.Core.Dtos
{
    public class PricingRuleDto
    {
        public string SKU { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public SpecialPrice? SpecialPrice { get; set; }
    }
}