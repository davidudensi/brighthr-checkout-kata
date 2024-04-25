using CheckoutKata.Core.Models;
using CheckoutKata.Tests.Fixtures;
using FluentAssertions;

namespace CheckoutKata.Tests.Systems.Models
{
    public class TestPricingRule
    {
        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("D")]
        public void CreatePricingRule_SKU_IsSet(string sku)
        {
            var sut = new PricingRule(sku, 50);
            sut.SKU.Should().Be(sku);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(75)]
        [InlineData(90)]
        public void CreatePricingRule_UnitPrice_IsSet(int unitPrice)
        {
            var sut = new PricingRule("A", unitPrice);
            sut.UnitPrice.Should().Be(unitPrice);
        }

        [Fact]
        public void CreatePricingRule_SpecialPrice_IsSet()
        {
            var specialPrice = SpecialPriceFixture.GetSpecialPrice();
            var sut = new PricingRule("A", 50, specialPrice);
            sut.SpecialPrice.Should().Be(specialPrice);
        }

        [Fact]
        public void CreatePricingRule_ForRegularRule_SpecialPrice_ShouldBeNull()
        {
            var sut = new PricingRule("A", 50);
            Assert.Null(sut.SpecialPrice);
        }
    }
}
