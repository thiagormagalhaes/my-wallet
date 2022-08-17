using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record NegociationDto(
    DateTime DateOperation,
    long TickerId,
    OperationType Operation,
    int Quantity,
    decimal UnitPrice
);
