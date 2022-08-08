using AngleSharp.Dom;
using MyWallet.Domain.Dto;
using MyWallet.Scraper.Interfaces;

namespace MyWallet.Scraper
{
    public class ScraperRealEstate : IScraperRealEstate, IScraperStrategy
    {
        private readonly IDocument _realEstate;

        public object Execute()
        {
            return new StockDto(
                GetName(),
                GetCnpj(),
                "",
                GetPrice()
            );
        }

        public ScraperRealEstate(IDocument realEstate)
        {
            _realEstate = realEstate;
        }

        public string GetAdministrator()
        {
            throw new NotImplementedException();
        }

        public string GetAdministratorCnpj()
        {
            throw new NotImplementedException();
        }

        public string GetCnpj()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetPrice()
        {
            throw new NotImplementedException();
        }

        public static string GetUrl(string ticker)
        {
            return $"https://statusinvest.com.br/fundos-imobiliarios/{ticker}";
        }
    }
}
