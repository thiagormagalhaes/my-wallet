namespace MyWallet.Domain.Entities
{
    public class RecommendedWallet : Entity<long>
    {
        public string Description { get; private set; }
        public decimal Weight { get; private set; }
        public virtual IList<Recommendation> Recommendations { get; private set; } = new List<Recommendation>();

        protected RecommendedWallet() { }

        public RecommendedWallet(string description, decimal weight)
        {
            Description = description;
            Weight = weight;
        }

        public void AddRecommendation(long tickerId, decimal weight, decimal limitePrice)
        {
            Recommendations.Add(new Recommendation(tickerId, weight, limitePrice));
        }
    }
}
