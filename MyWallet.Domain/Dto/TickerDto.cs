namespace MyWallet.Domain.Dto;

public record TickerDto(
    string Code,
    long CompanyId,
    decimal? Price
);
