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
            set { SetAmount(value); }
        }

        private void SetAmount(int quantity)
        {
            try
            {
                if (quantity >= Rule.SpecialPrice?.Units)
                {
                    int band = quantity / Rule.SpecialPrice?.Units ?? 0;
                    int rem = quantity % Rule.SpecialPrice?.Units ?? 0;

                    Amount = (band * Rule.SpecialPrice?.Price) + (rem * Rule.UnitPrice) ?? 0;
                }
                else
                {
                    Amount = quantity * Rule.UnitPrice;
                }
                _quantity = quantity;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}