using CheckoutKata.Core.Dtos;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        public Dictionary<string, PricingRule> Rules { get; set; }
        public Dictionary<string, CartItem> CartItems { get; set; }
        private readonly ILogger<CheckoutService> _logger;
        public CheckoutService(string rules, ILogger<CheckoutService> logger)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(rules)) throw new NullReferenceException();
            Rules = new Dictionary<string, PricingRule>();
            CartItems = new Dictionary<string, CartItem>();

            SetRules(rules);
            _logger.LogInformation("Pricing rules set successfully");
        }
        public int GetTotalPrice()
        {
            if (CartItems == null || CartItems.Count == 0) return 0;
            int total = CartItems.Values.Sum(c => c.Amount);
            return total;
        }

        public void Scan(string items)
        {
            if (string.IsNullOrEmpty(items)) throw new NullReferenceException();
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError($"Encountered error {ex}");
                throw new Exception();
            }
        }

        public void ClearCart()
        {
            CartItems.Clear();
            _logger.LogInformation("Cart has been cleared");
        }

        private void SetRules(string rules)
        {
            try
            {
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
                _logger.LogError($"Encountered error {ex}");
                throw new Exception();
            }
        }
    }
}