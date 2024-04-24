using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;

namespace CheckoutKata.Tests.Systems.Services
{
    public class TestCheckoutService
    {
        [Fact]
        public void GetTotalPrice_WhenCalled_ShouldReturn_Integer()
        {
            var sut = new CheckoutService();
            var result = sut.GetTotalPrice();
            Assert.True(result.GetType() == typeof(int));
        }

        [Fact]
        public void Scan_WhenCalled_ShouldCreateNonEmpty_ListOfPricingRules()
        {
            var sut = new CheckoutService("");
            var result = sut.PricingRules;
            result.Should().BeOfType<List<PricingRule>>();
        }
    }
}