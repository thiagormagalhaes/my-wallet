using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record StockDto(
    string Name,
    string Cnpj,
    string Ticker,
    string Price
);