using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Fixtures
{
    public static class SpecialPriceFixture
    {
        public static SpecialPrice GetSpecialPrice() => new()
        {
            Units = 3,
            Price = 130
        };
    }
}