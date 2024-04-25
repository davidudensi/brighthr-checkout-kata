namespace CheckoutKata.Core.Models
{
    public class CartItem
    {
        public string SKU { get; set; }
        public double Amount { get; private set; }
        public PricingRule Rule { get; set; }

        public CartItem(string sku, PricingRule rule)
        {
            SKU = sku;
            Rule = rule;
            Quantity = 1;
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= Rule.SpecialPrice?.Units)
                {
                    int band = value / Rule.SpecialPrice?.Units ?? 0;
                    int rem = value % Rule.SpecialPrice?.Units ?? 0;

                    Amount = (band * Rule.SpecialPrice?.Price) + (rem * Rule.UnitPrice) ?? 0;
                }
                else
                {
                    Amount = value * Rule.UnitPrice;
                }
                _quantity = value;
            }
        }
    }
}