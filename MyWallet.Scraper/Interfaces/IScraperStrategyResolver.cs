namespace MyWallet.Scraper.Interfaces
{
    public interface IScraperStrategyResolver
    {
        public IScraperStrategy FindStrategy(int category);
    }
}