namespace MyWallet.Scraper.Dto;

public record ScraperStrategyResponse(
    string Name,
    string Cnpj,
    string Price,
    AdministratorDto? Administrator = null
);