namespace CheckoutKata.Core.Models
{
    public class SpecialPrice
    {
        private int _units;
        private int _price;
        public int Units
        {
            get { return _units; }
            set
            {
                if (value > 0)
                {
                    _units = value;
                }
                else
                {
                    throw new ArgumentException("Units cannot be less than 1");
                }
            }
        }
        public int Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Price cannot be less than 1");
                }
            }
        }
    }
}