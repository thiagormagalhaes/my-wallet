using MyWallet.Domain.Dto;

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
        public bool Active { get; private set; }

        protected Recommendation() { }

        public Recommendation(RecommendationDto recommendationDto)
        {
            Weight = recommendationDto.Weight;
            LimitePrice = recommendationDto.LimitePrice;
            Active = recommendationDto.Active;
        }

        public void AddTicker(Ticker ticker)
        {
            Ticker = ticker;
        }

        public decimal? Discount()
        {
            if (!Ticker.Price.HasValue)
            {
                return null;
            }

            var percentage = (1 - (Ticker.Price.Value / LimitePrice)) * 100;

            return Math.Round(percentage, 2);
        }

        public bool HasBuyRecommendation() => Ticker.Price < LimitePrice;
    }
}
