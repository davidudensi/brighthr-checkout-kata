using CheckoutKata.Core.Dtos;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        public Dictionary<string, PricingRule> Rules { get; set; }
        public Dictionary<string, CartItem> CartItems { get; set; }
        public CheckoutService(string rules)
        {
            if (string.IsNullOrEmpty(rules)) throw new NullReferenceException();

            try
            {
                Rules = new Dictionary<string, PricingRule>();
                CartItems = new Dictionary<string, CartItem>();

                var pricingDtos = JsonConvert.DeserializeObject<List<PricingRuleDto>>(rules);
                if (pricingDtos == null) return;

                foreach (var newRule in pricingDtos)
                {
                    if (string.IsNullOrEmpty(newRule.SKU)) continue;

                    newRule.SKU = newRule.SKU.ToUpper();
                    if (Rules.ContainsKey(newRule.SKU)) Rules.Remove(newRule.SKU);
                    Rules.Add(newRule.SKU, new PricingRule(newRule.SKU, newRule.UnitPrice, newRule.SpecialPrice));
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
                if (!Rules.ContainsKey(sku_string)) continue;

                var rule = Rules[sku_string];
                if (CartItems.ContainsKey(sku_string))
                    CartItems[sku_string].Quantity++;
                else
                {
                    var item = new CartItem(sku_string, rule);
                    CartItems[sku_string] = item;
                }
            }
        }
    }
}