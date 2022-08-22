namespace MyWallet.Domain.Dto;

public record RecommendationDto(
    string TickerCode,
    decimal Weight,
    decimal LimitePrice,
    bool Active
);
