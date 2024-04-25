using CheckoutKata.Core.Models;
using CheckoutKata.Tests.Fixtures;
using FluentAssertions;

namespace CheckoutKata.Tests.Systems.Models
{
    public class TestCartItem
    {
        private readonly PricingRule regular_rule;
        private readonly PricingRule special_rule;
        public TestCartItem()
        {
            regular_rule = PricingRuleFixture.GetRegularPricingRule();  //SKU: A, UnitPrice: 50, SpecialPrice: null
            special_rule = PricingRuleFixture.GetSpecialPricingRule();  //SKU: A, UnitPrice: 50, SpecialPrice: { Units: 3, Price: 130 }
        }

        [Theory]
        [InlineData(1, 50)]
        [InlineData(2, 100)]
        [InlineData(4, 200)]
        [InlineData(7, 350)]
        public void CartItem_WhenCreated_RegularRule_Quantities_SKUAmount_IsSet(int quantity, int result)
        {
            var sut = new CartItem("A", regular_rule);
            sut.Quantity = quantity;
            sut.Amount.Should().Be(result);
        }

        [Theory]
        [InlineData(1, 50)]
        [InlineData(2, 100)]
        [InlineData(3, 130)]
        [InlineData(4, 180)]
        [InlineData(6, 260)]
        public void CartItem_WhenCreated_SpecialRule_Quantities_SKUAmount_IsSet(int quantity, int result)
        {
            var sut = new CartItem("A", special_rule);
            sut.Quantity = quantity;
            sut.Amount.Should().Be(result);
        }
    }
}