using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Systems.Models
{
    public class TestSpecialPrice
    {
        [Theory]
        [InlineData(3)]
        [InlineData(62)]
        [InlineData(15)]
        [InlineData(70)]
        [InlineData(10)]
        public void SpecialPrice_WhenCreatedShouldHave_Units(int units)
        {
            var sut = new SpecialPrice(units, 130);
            Assert.Equal(units, sut.Units);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(147)]
        [InlineData(159)]
        [InlineData(700)]
        [InlineData(1)]
        public void SpecialPrice_WhenCreatedShouldHave_Prices(int prices)
        {
            var sut = new SpecialPrice(3, prices);
            Assert.Equal(prices, sut.Price);
        }

        [Fact]
        public void SpecialPrice_IfUnitsEquals0_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SpecialPrice(0, 130));
        }

        [Fact]
        public void SpecialPrice_IfPriceEquals0_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SpecialPrice(3, 0));
        }
    }
}