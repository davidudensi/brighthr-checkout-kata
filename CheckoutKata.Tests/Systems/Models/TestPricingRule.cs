using CheckoutKata.Core.Models;
using CheckoutKata.Tests.Fixtures;

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
            Assert.Equal(sku, sut.SKU);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(75)]
        [InlineData(90)]
        public void CreatePricingRule_UnitPrice_IsSet(int unitPrice)
        {
            var sut = new PricingRule("A", unitPrice);
            Assert.Equal(unitPrice, sut.UnitPrice);
        }

        [Fact]
        public void CreatePricingRule_SpecialPrice_IsSet()
        {
            var specialPrice = SpecialPriceFixture.GetSpecialPrice();
            var sut = new PricingRule("A", 50, specialPrice);
            Assert.Equal(specialPrice, sut.SpecialPrice);
        }

        [Fact]
        public void CreatePricingRule_ForRegularRule_SpecialPrice_ShouldBeNull()
        {
            var sut = new PricingRule("A", 50);
            Assert.Null(sut.SpecialPrice);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABC")]
        [InlineData("ABCD")]
        public void CreatePricingRule_Accepts_VariableLengthSKUs(string sku)
        {
            var sut = new PricingRule(sku, 50);
            Assert.Equal(sku, sut.SKU);
        }
    }
}
