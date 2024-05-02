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

            if (string.IsNullOrEmpty(rules))
            {
                _logger.LogDebug("Rules string is null or empty");
                throw new NullReferenceException("Rules string is null or empty");
            }
            Rules = new Dictionary<string, PricingRule>();
            CartItems = new Dictionary<string, CartItem>();

            SetRules(rules);
            _logger.LogInformation("Pricing rules set successfully");
        }
        public int GetTotalPrice()
        {
            if (CartItems == null || CartItems.Count == 0) return 0;
            return CartItems.Values.Sum(c => c.Amount); ;
        }

        public void Scan(string items)
        {
            if (string.IsNullOrEmpty(items))
            {
                _logger.LogDebug("Items to scan is null or empty");
                throw new NullReferenceException("Items to scan is null or empty");
            }
            try
            {
                if (!Rules.ContainsKey(items))
                {
                    _logger.LogDebug($"No pricing rule found for {items}");
                    return;
                }

                var rule = Rules[items];
                if (CartItems.ContainsKey(items))
                    CartItems[items].Quantity++;
                else
                    CartItems[items] = new CartItem(items, rule);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Encountered error {ex}");
                throw new Exception();
            }
        }

        public void ClearCart()
        {
            if (CartItems == null) return;
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