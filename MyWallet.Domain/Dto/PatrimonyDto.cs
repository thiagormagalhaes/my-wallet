using MyWallet.Domain.Entities;

namespace MyWallet.Domain.Dto
{
    public class PatrimonyDto
    {
        public long TickerId { get; set; }
        public Ticker Ticker { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
