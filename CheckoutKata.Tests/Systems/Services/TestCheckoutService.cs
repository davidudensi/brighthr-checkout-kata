using CheckoutKata.Core.Models;
using CheckoutKata.Core.Services;
using FluentAssertions;

namespace CheckoutKata.Tests.Systems.Services
{
    public class TestCheckoutService
    {
        [Fact]
        public void CheckoutServiceConstructor_IfRulesAreNullOrEmptyShould_ThrowNullException()
        {
            Assert.Throws<NullReferenceException>(() => new CheckoutService(""));
        }

        [Fact]
        public void CheckoutServiceConstructor_IfRulesAreWrongDtoFormatShould_ThrowException()
        {
            var rules_string = File.ReadAllText("rules-wrong-format.json");
            Assert.Throws<Exception>(() => new CheckoutService(rules_string));
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