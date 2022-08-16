using MyWallet.Scraper.Interfaces;

namespace MyWallet.Scraper.Strategies
{
    public class ScraperStrategyResolver : IScraperStrategyResolver
    {
        private readonly IEnumerable<IScraperStrategy> _strategies;

        public ScraperStrategyResolver(IEnumerable<IScraperStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IScraperStrategy FindStrategy(int category)
        {
            var strategy = _strategies.FirstOrDefault(c => c.ApplyTo(category));

            if (strategy == null)
            {
                throw new ArgumentException($"No has strategy for category {category}.");
            }

            return strategy;
        }
    }
}
