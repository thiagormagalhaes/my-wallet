using MyWallet.Scraper.Dto;

namespace MyWallet.Scraper.Interfaces
{
    public interface IScraperStrategy
    {
        bool ApplyTo(int category);

        Task<ScraperStrategyResponse> Execute(string ticker);
    }
}
