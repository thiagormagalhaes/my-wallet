using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Dto
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public CategoryType Category { get; set; }
        public int? AdministratorId { get; set; }
    }
}
