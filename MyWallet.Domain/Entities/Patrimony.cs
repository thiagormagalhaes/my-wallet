namespace MyWallet.Domain.Entities
{
    public class Patrimony : Entity<Guid>
    {
        public long TickerId { get; private set; }
        public Ticker Ticker { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        protected Patrimony() { }

        public Patrimony(long tickerId)
        {
            TickerId = tickerId;
        }

        public decimal InvestedAmount()
        {
            return Price * Quantity;
        }

        public decimal CurrentPatrimony()
        {
            if (Ticker.Price.HasValue)
            {
                return Ticker.Price.Value * Quantity;
            }

            return 0;
        }

        public void Increase(decimal price, int quantity)
        {
            var amount = (Price * Quantity) + (price * quantity);

            Quantity += quantity;

            CalculatePrice(amount);
        }
        
        public void Decrease(decimal price, int quantity)
        {
            var amount = (Price * Quantity) - (price * quantity);

            Quantity -= quantity;

            CalculatePrice(amount);

            CheckPrice();
        }

        private void CalculatePrice(decimal amount)
        {
            if (Quantity == 0)
            {
                return;
            }

            var price = (amount / Quantity).ToString("F");

            Price = Convert.ToDecimal(price);
        }

        private void CheckPrice()
        {
            if (Quantity == 0)
            {
                Price = 0;
            }
        }
    }
}
