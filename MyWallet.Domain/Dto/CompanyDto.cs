using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto;

public record CompanyDto(
    string Name,
    string Cnpj,
    Category Category,
    long? AdministratorId
);
