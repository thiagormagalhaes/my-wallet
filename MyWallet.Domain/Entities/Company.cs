using MyWallet.Domain.Dto;
using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public CategoryType Category { get; private set; }
        public int? AdministratorId { get; private set; }
        public virtual Administrator Administrator { get; private set; }
        public virtual IList<Ticker> Tickers { get; private set; } = new List<Ticker>();

        protected Company() { }

        public Company(CompanyDto companyDto)
        {
            Name = companyDto.Name;
            Cnpj = companyDto.Cnpj;
            Category = companyDto.Category;
            AdministratorId = companyDto.AdministratorId;
        }

        public void AddTicker(Ticker ticker)
        {
            Tickers.Add(ticker);
        }
    }
}
