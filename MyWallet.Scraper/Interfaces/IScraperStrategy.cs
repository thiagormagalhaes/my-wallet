using AngleSharp.Dom;
using MyWallet.Domain.Enums;
using MyWallet.Scraper.Dto;

namespace MyWallet.Scraper.Interfaces
{
    public interface IScraperStrategy
    {
        bool ApplyTo(CategoryType category);

        Task<ScraperStrategyResponse> Execute(string ticker);
    }
}
