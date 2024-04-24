using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Fixtures
{
    public static class PricingRuleFixture
    {
        public static PricingRule GetRegularPricingRule() => new()
        {
            SKU = 'A',
            UnitPrice = 50,
        };

        public static PricingRule GetSpecialPricingRule() => new(SpecialPriceFixture.GetSpecialPrice())
        {
            SKU = 'A',
            UnitPrice = 50,
        };
    }
}