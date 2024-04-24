using CheckoutKata.Core.Dtos;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        public List<PricingRule> PricingRules { get; set; }
        public List<CartItem> CartItems { get; set; }
        public CheckoutService(string rules)
        {
            if (string.IsNullOrEmpty(rules)) throw new NullReferenceException();

            try
            {
                PricingRules = new List<PricingRule>();
                CartItems = new List<CartItem>();

                var pricingDtos = JsonConvert.DeserializeObject<List<PricingRuleDto>>(rules);
                if (pricingDtos == null) return;

                foreach (var newRule in pricingDtos)
                {
                    if (string.IsNullOrEmpty(newRule.SKU)) continue;

                    var existingRule = PricingRules.Where(x => x.SKU == newRule.SKU).FirstOrDefault();
                    if (existingRule != null) PricingRules.Remove(existingRule);

                    if (newRule.SpecialPrice != null)
                        PricingRules.Add(new PricingRule(newRule.SKU, newRule.UnitPrice, newRule.SpecialPrice));
                    else
                        PricingRules.Add(new PricingRule(newRule.SKU, newRule.UnitPrice));
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

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}