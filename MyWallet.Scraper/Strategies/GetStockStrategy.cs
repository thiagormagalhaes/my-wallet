using MyWallet.Scraper.Dto;
using MyWallet.Scraper.Enums;
using MyWallet.Scraper.Extensions;

namespace MyWallet.Scraper.Strategies
{
    public class GetStockStrategy : ScraperStrategy
    {
        public override bool ApplyTo(int category)
        {
            return category == Category.Stock.GetHashCode();
        }
        public override ScraperStrategyResponse GetScraperStrategyResponse()
        {
            return new ScraperStrategyResponse(GetName(), GetCnpj(), GetPrice());
        }

        public override string GetUrl(string ticker)
        {
            return $"https://statusinvest.com.br/acoes/{ticker}";
        }

        public string GetCnpj()
        {
            return _document.GetTextBySelector("#company-section > div:nth-child(1) > div > div.d-block.d-md-flex.mb-5.img-lazy-group > div.company-description.w-100.w-md-70.ml-md-5 > h4 > small");
        }

        public string GetName()
        {
            return _document.GetTextBySelector("#company-section > div:nth-child(1) > div > div.d-block.d-md-flex.mb-5.img-lazy-group > div.company-description.w-100.w-md-70.ml-md-5 > h4 > span");
        }

        public string GetPrice()
        {
            return _document.GetTextBySelector("#main-2 > div:nth-child(4) > div > div.pb-3.pb-md-5 > div > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong");
        }
    }
}

