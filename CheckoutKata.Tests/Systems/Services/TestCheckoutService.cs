using CheckoutKata.Core.Services;
using FluentAssertions;
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
            var rules_string = File.ReadAllText("rules-wrong-format.json");
            Assert.Throws<Exception>(() => new CheckoutService(rules_string, logger.Object));
        }

        [Fact]
        public void Constructor_ShouldSkipNullEmptySKUs_CreateOneNotTwo()
        {
            var rules_string = File.ReadAllText("rules-two.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.Rules.Should().HaveCount(1);
        }

        [Fact]
        public void Constructor_ShouldReplaceOldRule_WithNewerRule_UnitPrice_ShouldBe75()
        {
            var rules_string = File.ReadAllText("rules-replace.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            var rule = sut.Rules["A"];
            Assert.Equal(75, rule?.UnitPrice);
        }

        [Theory]
        [InlineData("ABCD", 4)]
        [InlineData("AACD", 3)]
        [InlineData("AAAA", 1)]
        [InlineData("AABB", 2)]
        public void Scan_WhenCalled_UpdatesCart_WithSKUs_CountShouldBeResult(string items, int result)
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.ClearCart();
            sut.Scan(items);
            sut.CartItems.Should().HaveCount(result);
        }

        [Fact]
        public void Scan_WhenCalled_UpdatesCart_WithEmptySKUs_ShouldThrowNullReferenceException()
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.ClearCart();
            Assert.Throws<NullReferenceException>(() => sut.Scan(""));
        }

        [Fact]
        public void ClearCart_WhenCalled_ShouldSetCartItems_Equals0()
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.Scan("ABCD");
            sut.ClearCart();
            sut.CartItems.Should().HaveCount(0);
        }

        [Fact]
        public void GetTotalPrice_WhenCalled_ShouldReturn_Integer()
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.Scan("A");
            var result = sut.GetTotalPrice();
            Assert.True(result.GetType() == typeof(int));
        }

        [Theory]
        [InlineData("A", 50)]
        [InlineData("AA", 100)]
        [InlineData("AAA", 130)]
        [InlineData("AAAA", 180)]
        [InlineData("AAAAA", 230)]
        [InlineData("AAAAAA", 260)]
        public void GetTotalPrice_WhenCalled_ShouldCalculate_Total(string items, int expected)
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.Scan(items);
            var total = sut.GetTotalPrice();
            total.Should().Be(expected);
        }

        [Theory]
        [InlineData("ABCD")]
        [InlineData("AACD")]
        [InlineData("AAAA")]
        [InlineData("AABB")]
        public void GetTotalPrice_WhenCartItems_IsCleared_ShouldReturn0(string items)
        {
            var rules_string = File.ReadAllText("rules.json");
            var sut = new CheckoutService(rules_string, logger.Object);
            sut.Scan(items);
            sut.ClearCart();
            var total = sut.GetTotalPrice();
            total.Should().Be(0);
        }
    }
}