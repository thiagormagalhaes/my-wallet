namespace MyWallet.Domain.Dto;

public record RecommendedWalletDto(
    string Description,
    decimal Weight,
    bool Active
);
