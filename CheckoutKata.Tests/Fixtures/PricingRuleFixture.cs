using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Fixtures
{
    public static class PricingRuleFixture
    {
        private static readonly SpecialPrice specialPrice = SpecialPriceFixture.GetSpecialPrice();
        public static PricingRule GetRegularPricingRule() => new("A", 50);

        public static PricingRule GetSpecialPricingRule() => new("A", 50, specialPrice);
    }
}