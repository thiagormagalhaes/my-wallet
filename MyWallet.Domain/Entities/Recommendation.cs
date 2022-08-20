namespace MyWallet.Domain.Entities
{
    public class Recommendation : Entity<long>
    {
        public long RecommendedWalletId { get; private set; }
        public RecommendedWallet RecommendedWallet { get; private set; }
        public long TickerId { get; private set; }
        public Ticker Ticker { get; private set; }
        public decimal Weight { get; private set; }
        public decimal LimitePrice { get; private set; }

        protected Recommendation() { }

        public Recommendation(long tickerId, decimal weight, decimal limitePrice)
        {
            TickerId = tickerId;
            Weight = weight;
            LimitePrice = limitePrice;
        }
    }
}
