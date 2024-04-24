using CheckoutKata.Core.Services;
using FluentAssertions;

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
    }
}