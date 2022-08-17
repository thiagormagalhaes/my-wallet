using MyWallet.Domain.Dto;
using MyWallet.Domain.Enums;
using MyWallet.Domain.ValueObjects;

namespace MyWallet.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public Category Category { get; private set; }
        public long? AdministratorId { get; private set; }
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

        public Company(CompanyCSV companyCSV, Category category)
        {
            Name = companyCSV.Company;
            Cnpj = companyCSV.Cnpj;
            Category = category;
        }

        public void AddTicker(Ticker ticker)
        {
            Tickers.Add(ticker);
        }

        public void UpdateAdministrator(Administrator administrator)
        {
            Administrator = administrator;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public bool HasTicker(string tickerCode)
        {
            return Tickers.Count(x => x.Code == tickerCode.ToUpperInvariant()) > 0;
        }
    }
}
