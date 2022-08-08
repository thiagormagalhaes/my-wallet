namespace MyWallet.Scraper.Interfaces
{
    public interface IScraperRealEstate : IScraperCommonFields
    {
        string GetAdministrator();
        string GetAdministratorCnpj();
    }
}
