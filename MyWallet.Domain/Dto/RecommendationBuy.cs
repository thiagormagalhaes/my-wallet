using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record RecommendationBuy(
    string Category,
    string TickerCode,
    decimal? Discount,
    int Quantity,
    decimal? Price,
    decimal? Amount
);
