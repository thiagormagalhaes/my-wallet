using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class Patrimony : Entity<Guid>
    {
        public long TickerId { get; private set; }
        public Ticker Ticker { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        protected Patrimony() { }

        public Patrimony(PatrimonyDto patrimonyDto)
        {

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
    }
}
