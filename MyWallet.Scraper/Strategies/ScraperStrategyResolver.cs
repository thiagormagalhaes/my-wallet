using MyWallet.Domain.Enums;
using MyWallet.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWallet.Scraper.Strategies
{
    public class ScraperStrategyResolver : IScraperStrategyResolver
    {
        private readonly IScraperStrategy[] _strategies;

        public ScraperStrategyResolver(IScraperStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public IScraperStrategy FindStrategy(CategoryType category)
        {
            var strategy = _strategies.FirstOrDefault(c => c.ApplyTo(category));

            if (strategy == null)
                throw new ArgumentException($"No has strategy for category {category}.");


            return strategy;
        }
    }
}
