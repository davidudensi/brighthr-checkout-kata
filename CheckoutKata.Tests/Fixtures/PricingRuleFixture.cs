using System.Security;
using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Fixtures
{
    public static class PricingRuleFixture
    {
        private static readonly SpecialPrice specialPrice = SpecialPriceFixture.GetSpecialPrice();
        public static PricingRule GetRegularPricingRule() => new("A", 50);
        public static PricingRule GetSpecialPricingRule() => new("A", 50, specialPrice);
        public const string RULES_PASSING = "[{\"SKU\": \"AAAA\",\"UnitPrice\": 50,\"SpecialPrice\": {\"Units\": 3,\"Price\": 130}},{\"SKU\": \"BBBB\",\"UnitPrice\": 30,\"SpecialPrice\": {\"Units\": 2,\"Price\": 45}},{\"SKU\": \"CCCC\",\"UnitPrice\": 20,\"SpecialPrice\": null},{\"SKU\": \"DDDD\",\"UnitPrice\": 15,\"SpecialPrice\": null}]";
        public const string RULES_WRONG_FORMAT = "{\"SKU\": \"A\",\"UnitPrice\": 50,\"SpecialPrice\": {\"Units\": 3,\"Price\": 130}}";
        public const string RULES_WITH_EMPTY_SKU = "[{\"SKU\": \"A\",\"UnitPrice\": 50,\"SpecialPrice\": {\"Units\": 3,\"Price\": 130}},{\"SKU\": \"\",\"UnitPrice\": 30,\"SpecialPrice\": null}]";
        public const string RULES_WITH_REPEATED_SKUS = "[{\"SKU\": \"A\",\"UnitPrice\": 50,\"SpecialPrice\": {\"Units\": 3,\"Price\": 130}},{\"SKU\": \"A\",\"UnitPrice\": 75,\"SpecialPrice\": {\"Units\": 3,\"Price\": 200}}]";
    }
}