using Microsoft.Extensions.DependencyInjection;
using MyWallet.Scraper.Interfaces;
using MyWallet.Scraper.Strategies;

namespace MyWallet.Scraper.Extensions;

public static class ScraperExtensions
{
    public static void AddScrapperStrategy(this IServiceCollection service)
    {
        service.AddScoped<IScraperStrategyResolver,
            ScraperStrategyResolver>();
        service.AddScoped<IScraperStrategy, GetRealEstateStrategy>();
        service.AddScoped<IScraperStrategy, GetStockStrategy>();
    }
}
