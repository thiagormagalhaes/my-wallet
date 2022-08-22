using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class RecommendedWallet : Entity<long>
    {
        public string Description { get; private set; }
        public decimal Weight { get; private set; }
        public bool Active { get; private set; }
        public virtual IList<Recommendation> Recommendations { get; private set; } = new List<Recommendation>();

        protected RecommendedWallet() { }

        public RecommendedWallet(RecommendedWalletDto recommendedWalletDto)
        {
            Description = recommendedWalletDto.Description;
            Weight = recommendedWalletDto.Weight;
            Active = recommendedWalletDto.Active;
        }

        public void AddRecommendation(Recommendation recommendation)
        {
            Recommendations.Add(recommendation);
        }

        public DateTime? LastPriceUpdate() => Recommendations.Select(x => x.Ticker.UpdateDate).Min();
    }
}
