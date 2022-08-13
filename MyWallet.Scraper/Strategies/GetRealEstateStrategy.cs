using MyWallet.Domain.Enums;
using MyWallet.Scraper.Dto;
using MyWallet.Scraper.Extensions;

namespace MyWallet.Scraper.Strategies
{
    public class GetRealEstateStrategy : ScraperStrategy
    {
        public override bool ApplyTo(CategoryType category)
        {
            return category == CategoryType.RealEstate;
        }

        public override ScraperStrategyResponse GetScraperStrategyResponse()
        {
            return new ScraperStrategyResponse(
                GetName(),
                GetCnpj(),
                "",
                GetCurrentPrice(),
                new AdministratorDto(GetAdministrator(), GetAdministratorCnpj())
            );
        }

        public override string GetUrl(string ticker)
        {
            return $"https://statusinvest.com.br/fundos-imobiliarios/{ticker}";
        }

        private string GetAdministratorCnpj()
        {
            return _document.GetTextBySelector("#fund-section > div > div > div:nth-child(3) > div > div.card-body.mt-2 > div.text-center.mb-3 > div > span");
        }

        private string GetAdministrator()
        {
            return _document.GetTextBySelector("#fund-section > div > div > div:nth-child(3) > div > div.card-body.mt-2 > div.text-center.mb-3 > div > strong");
        }

        private string GetCnpj()
        {
            return _document.GetTextBySelector("#fund-section > div > div > div:nth-child(2) > div > div:nth-child(1) > div > div > strong");
        }

        private string GetCurrentPrice()
        {
            return _document.GetTextBySelector("#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong");
        }

        private string GetName()
        {
            return _document.GetTextBySelector("#fund-section > div > div > div:nth-child(2) > div > div:nth-child(2) > div > div > div > strong");
        }
    }
}