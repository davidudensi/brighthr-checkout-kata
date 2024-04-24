using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using FluentAssertions;

namespace CheckoutKata.Tests.Systems.Services
{
    public class TestCheckoutService
    {
        [Fact]
        public void Constructor_IfRulesAreNullOrEmptyShould_ThrowNullException()
        {
            Assert.Throws<NullReferenceException>(() => new CheckoutService(""));
        }

        [Fact]
        public void Constructor_IfRulesAreWrongDtoFormatShould_ThrowException()
        {
            var rules_string = File.ReadAllText("rules-wrong-format.json");
            Assert.Throws<Exception>(() => new CheckoutService(rules_string));
        }

        [Fact]
        public void Constructor_ShouldSkipNullEmptySKUs_CreateOneNotTwo()
        {
            var rules_string = File.ReadAllText("rules-two.json");
            var sut = new CheckoutService(rules_string);
            sut.PricingRules.Should().HaveCount(1);
        }

        [Fact]
        public void Constructor_ShouldReplaceOldRule_WithNewerRule_UnitPrice_ShouldBe75()
        {
            var rules_string = File.ReadAllText("rules-replace.json");
            var sut = new CheckoutService(rules_string);
            var rule = sut.PricingRules.Where(x => x.SKU == "A").FirstOrDefault();
            Assert.Equal(75, rule?.UnitPrice);
        }

        [Fact]
        public void Scan_WhenCalled_UpdatesCart_WithSKUs()
        {
            var sut = new CheckoutService("");
            sut.CartItems.Clear();
            sut.Scan("ABCD");
            sut.CartItems.Should().HaveCount(4);
        }

        // [Fact]
        // public void GetTotalPrice_WhenCalled_ShouldReturn_Integer()
        // {
        //     var sut = new CheckoutService("");
        //     var result = sut.GetTotalPrice();
        //     Assert.True(result.GetType() == typeof(int));
        // }

        // [Fact]
        // public void Scan_WhenCalled_ShouldCreateNonEmpty_ListOfPricingRules()
        // {
        //     var rules_string = File.ReadAllText("rules.json");
        //     var sut = new CheckoutService(rules_string);
        //     var result = sut.PricingRules;
        //     result.Should().BeOfType<List<PricingRule>>();
        // }
    }
}