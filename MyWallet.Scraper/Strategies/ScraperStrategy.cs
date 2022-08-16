using AngleSharp;
using AngleSharp.Dom;
using MyWallet.Scraper.Dto;
using MyWallet.Scraper.Interfaces;

public abstract class ScraperStrategy : IScraperStrategy
{
    protected IDocument _document;

    public abstract bool ApplyTo(int category);

    public async Task<ScraperStrategyResponse> Execute(string ticker)
    {
        await SetupStrategy(ticker);

        return GetScraperStrategyResponse();
    }

    public async Task SetupStrategy(string ticker)
    {
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        _document = await context.OpenAsync(GetUrl(ticker));
    }

    public abstract ScraperStrategyResponse GetScraperStrategyResponse();

    public abstract string GetUrl(string ticker);
}