using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record RealEstateDto(
    string Name,
    string Cnpj,
    string Ticker,
    string Administrator,
    string AdministratorCnpj,
    string Price
);