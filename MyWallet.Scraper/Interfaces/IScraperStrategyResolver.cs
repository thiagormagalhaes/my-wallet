using MyWallet.Domain.Enums;
using MyWallet.Scraper.Interfaces;

namespace MyWallet.Scraper.Interfaces
{
    public interface IScraperStrategyResolver
    {
        public IScraperStrategy FindStrategy(CategoryType category);
    }
}

