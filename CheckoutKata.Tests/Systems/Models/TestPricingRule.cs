using CheckoutKata.Tests.Fixtures;

namespace CheckoutKata.Tests.Systems.Models
{
    public class TestPricingRule
    {
        [Fact]
        public void HasSpecialPrice_ShouldBeFalse_ForRegularPricingRule()
        {
            var pricing_rule = PricingRuleFixture.GetRegularPricingRule();
            Assert.False(pricing_rule.HasSpecialPrice);
        }

        [Fact]
        public void HasSpecialPrice_ShouldBeTrue_ForSpecialPricingRule()
        {
            var pricing_rule = PricingRuleFixture.GetSpecialPricingRule();
            Assert.True(pricing_rule.HasSpecialPrice);
        }
    }
}
