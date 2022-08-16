using MyWallet.Scraper.Dto;

namespace MyWallet.Scraper.Extensions
{
    public static class ScraperStrategyResponseExtensions
    {
        public static bool IsValid(this ScraperStrategyResponse x)
        {
            return !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(x.Cnpj);
        }

        public static decimal GetPrice(this ScraperStrategyResponse x)
        {
            return Convert.ToDecimal(x.Price);
        }
    }
}
