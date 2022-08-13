using MyWallet.Domain.Enums;

namespace MyWallet.Scraper.Dto;

public record ScraperStrategyResponse(
    string Name,
    string Cnpj,
    string Ticker,
    string Price,
    AdministratorDto? Administrator = null
);