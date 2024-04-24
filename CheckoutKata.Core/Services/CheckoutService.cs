using System.Text.Json.Serialization;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Newtonsoft.Json;

namespace CheckoutKata.Core.Services
{
    public class CheckoutService : ICheckout
    {
        
        public CheckoutService(string rules)
        {
            if(string.IsNullOrEmpty(rules)) throw new NullReferenceException();

            try
            {
                var pricingDtos = JsonConvert.DeserializeObject<PricingRuleDto>(rules);  
            }
            catch (Exception)
            {
                
                throw;
            }
                      
        }
        public int GetTotalPrice()
        {
            return 0;
        }
    }
}