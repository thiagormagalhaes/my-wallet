using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record NegociationDto(
    DateTime DateOperation,
    int CompanyId,
    OperationType Operation,
    int Quantity,
    decimal UnitPrice
);
