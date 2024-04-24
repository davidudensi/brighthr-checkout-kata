using CheckoutKata.Core.Dtos;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        public List<PricingRule> PricingRules { get; set; }
        public CheckoutService(string rules)
        {
            if (string.IsNullOrEmpty(rules)) throw new NullReferenceException();

            try
            {
                var pricingDtos = JsonConvert.DeserializeObject<List<PricingRuleDto>>(rules);
                if (pricingDtos == null) return;

                foreach (var pricing in pricingDtos)
                {
                    if(string.IsNullOrEmpty(pricing.SKU)) continue;
                    if (pricing.SpecialPrice != null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public int GetTotalPrice()
        {
            return 0;
        }
    }
}