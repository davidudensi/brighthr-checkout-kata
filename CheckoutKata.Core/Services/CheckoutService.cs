using CheckoutKata.Core.Dtos;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        public List<PricingRule> Rules { get; set; }
        public List<CartItem> CartItems { get; set; }
        public CheckoutService(string rules)
        {
            if (string.IsNullOrEmpty(rules)) throw new NullReferenceException();

            try
            {
                Rules = new List<PricingRule>();
                CartItems = new List<CartItem>();

                var pricingDtos = JsonConvert.DeserializeObject<List<PricingRuleDto>>(rules);
                if (pricingDtos == null) return;

                foreach (var newRule in pricingDtos)
                {
                    if (string.IsNullOrEmpty(newRule.SKU)) continue;

                    newRule.SKU = newRule.SKU.ToUpper();
                    var existingRule = Rules.Where(x => x.SKU == newRule.SKU).FirstOrDefault();
                    if (existingRule != null) Rules.Remove(existingRule);

                    if (newRule.SpecialPrice != null)
                        Rules.Add(new PricingRule(newRule.SKU, newRule.UnitPrice, newRule.SpecialPrice));
                    else
                        Rules.Add(new PricingRule(newRule.SKU, newRule.UnitPrice));
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

        public void Scan(string items)
        {
            if (string.IsNullOrEmpty(items)) throw new NullReferenceException();

            List<char> skus = items.ToList();
            foreach (char sku in skus)
            {
                var sku_string = sku.ToString();
                var rule = Rules.Where(x => x.SKU == sku_string).FirstOrDefault();
                if (rule == null) continue;

                // var existingItem = CartItems['']
            }
        }
    }
}