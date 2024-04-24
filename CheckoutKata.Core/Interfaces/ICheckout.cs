namespace CheckoutKata.Core.Interfaces
{
    public interface ICheckout
    {
        public void Scan(string item);
        public int GetTotalPrice();
    }
}