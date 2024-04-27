using CheckoutKata.Core.Models;

namespace CheckoutKata.Tests.Fixtures
{
    public static class SpecialPriceFixture
    {
        public static SpecialPrice GetSpecialPrice() => new(3, 130);
    }
}