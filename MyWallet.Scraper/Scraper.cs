using AngleSharp;
using AngleSharp.Dom;
using MyWallet.Domain.Enums;
using MyWallet.Scraper.Interfaces;

namespace MyWallet.Scraper
{
    public class Scraper : IScraper
    {
        private readonly IBrowsingContext context;

        public Scraper()
        {
            var config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
        }

        public async Task<object> Execute(CategoryType categoryType, string ticker)
        {
            var url = ResolveUrl(categoryType, ticker);

            var document = await context.OpenAsync(url);

            return ResolveStrategy(categoryType, document).Execute();
        }

        private IScraperStrategy ResolveStrategy(CategoryType categoryType, IDocument document)
        {
            switch (categoryType)
            {
                case CategoryType.RealEstate:
                    return new ScraperRealEstate(document);
                default:
                    return new ScraperStock(document);
            }
        }

        private string ResolveUrl(CategoryType categoryType, string ticker)
        {
            switch (categoryType)
            {
                case CategoryType.RealEstate:
                    return ScraperRealEstate.GetUrl(ticker);
                default:
                    return ScraperStock.GetUrl(ticker);
            }
        }
    }
}
