using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class Ticker : Entity<long>
    {
        public string Code { get; private set; }
        public long CompanyId { get; private set; }
        public decimal? Price { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public virtual Company Company { get; private set; }
        public virtual IList<Earning> Earnings { get; private set; } = new List<Earning>();

        protected Ticker() { }

        public Ticker(TickerDto tickerDto)
        {
            Code = tickerDto.Code.ToUpper();
            CompanyId = tickerDto.CompanyId;

            if (tickerDto.Price.HasValue)
            {
                Update(tickerDto.Price.Value);
            }
        }

        public Ticker(string code)
        {
            Code = code;
        }

        public void Update(decimal price)
        {
            Price = price;
            UpdateDate = DateTime.Now;
        }
    }
}
