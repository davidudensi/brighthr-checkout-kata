using CheckoutKata.Core.Services;
using CheckoutKata.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace CheckoutKata.Tests.Systems.Services
{
    public class TestCheckoutService
    {
        private readonly Mock<ILogger<CheckoutService>> logger;
        public TestCheckoutService()
        {
            logger = new Mock<ILogger<CheckoutService>>();
        }

        [Fact]
        public void Constructor_IfRulesAreNullOrEmptyShould_ThrowNullException()
        {
            Assert.Throws<NullReferenceException>(() => new CheckoutService("", logger.Object));
        }

        [Fact]
        public void Constructor_IfRulesAreWrongDtoFormatShould_ThrowException()
        {
            Assert.Throws<Exception>(() => new CheckoutService(PricingRuleFixture.RULES_WRONG_FORMAT, logger.Object));
        }

        [Fact]
        public void Constructor_ShouldSkipNullEmptySKUs_CreateOneNotTwo()
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_WITH_EMPTY_SKU, logger.Object);
            Assert.Equal(1, sut?.Rules.Count);
        }

        [Fact]
        public void Constructor_ShouldReplaceOldRule_WithNewerRule_UnitPrice_ShouldBe75()
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_WITH_REPEATED_SKUS, logger.Object);
            var rule = sut.Rules["A"];
            Assert.Equal(75, rule?.UnitPrice);
        }

        [Theory]
        [InlineData(new string[] { "AAAA", "BBBB", "CCCC", "DDDD" }, 4)]
        [InlineData(new string[] { "AAAA", "BBBB", "CCCC" }, 3)]
        [InlineData(new string[] { "AAAA", "BBBB" }, 2)]
        [InlineData(new string[] { "AAAA" }, 1)]
        public void Scan_WhenCalled_UpdatesCart_WithSKUs_CountShouldBeResult(string[] items, int result)
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            foreach (string item in items)
            {
                sut.Scan(item);
            }
            Assert.Equal(result, sut.CartItems.Count);
        }

        [Theory]
        [InlineData(new string[] { "A", "AB", "ABC", "ABCD" }, 4)]
        [InlineData(new string[] { "A", "AB", "ABC" }, 3)]
        [InlineData(new string[] { "A", "AB" }, 2)]
        [InlineData(new string[] { "A" }, 1)]
        public void Scan_WhenCalled_UpdatesCart_WithVariableLengthSKUs_CountShouldBeResult(string[] items, int result)
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_VARIABLE_LENGTH_SKUS, logger.Object);
            foreach (string item in items)
            {
                sut.Scan(item);
            }
            Assert.Equal(result, sut.CartItems.Count);
        }

        [Fact]
        public void Scan_WhenCalled_UpdatesCart_WithEmptySKUs_ShouldThrowNullReferenceException()
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            sut.ClearCart();
            Assert.Throws<NullReferenceException>(() => sut.Scan(""));
        }

        [Fact]
        public void ClearCart_WhenCalled_ShouldSetCartItems_Equals0()
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            sut.Scan("AAAA");
            sut.ClearCart();
            Assert.Equal(0, sut?.CartItems.Count);
        }

        [Fact]
        public void GetTotalPrice_WhenCalled_ShouldReturn_Integer()
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            sut.Scan("A");
            var result = sut.GetTotalPrice();
            Assert.True(result.GetType() == typeof(int));
        }

        [Theory]
        [InlineData(new string[] { "AAAA" }, 50)]
        [InlineData(new string[] { "AAAA", "AAAA" }, 100)]
        [InlineData(new string[] { "AAAA", "AAAA", "AAAA" }, 130)]
        [InlineData(new string[] { "AAAA", "AAAA", "AAAA", "AAAA" }, 180)]
        [InlineData(new string[] { "AAAA", "AAAA", "AAAA", "AAAA", "AAAA" }, 230)]
        [InlineData(new string[] { "AAAA", "AAAA", "AAAA", "AAAA", "AAAA", "AAAA" }, 260)]
        public void GetTotalPrice_WhenCalled_ShouldCalculate_Total(string[] items, int expected)
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            foreach (string item in items)
            {
                sut.Scan(item);
            }
            var total = sut.GetTotalPrice();
            Assert.Equal(expected, total);
        }

        [Theory]
        [InlineData("AAAA")]
        [InlineData("BBBB")]
        [InlineData("CCCC")]
        [InlineData("DDDD")]
        public void GetTotalPrice_WhenCartItems_IsCleared_ShouldReturn0(string items)
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            sut.Scan(items);
            sut.ClearCart();
            var total = sut.GetTotalPrice();
            Assert.Equal(0, total);
        }

        [Theory]
        [InlineData("ABCD")]
        [InlineData("PUYT")]
        [InlineData("ZUIO")]
        [InlineData("HASC")]
        public void Scan_WhenSKUDoesNotExitInRules_ShouldSkipSKU_CartShouldBeEmpty(string item)
        {
            var sut = new CheckoutService(PricingRuleFixture.RULES_PASSING, logger.Object);
            sut.Scan(item);
            Assert.Equal(0, sut?.CartItems.Count);
        }
    }
}