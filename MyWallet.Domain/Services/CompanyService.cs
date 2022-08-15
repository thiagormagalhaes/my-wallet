using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Extensions;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.ValueObjects;

namespace MyWallet.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Import(IFormFile file, CategoryType category)
        {
            var companiesCSV = file.ConvertCSV<CompanyCSV>();

            var administrators = BuildAdministrators(companiesCSV, category);

            var companies = BuildCompanies(companiesCSV, administrators, category);

            await _companyRepository.AddRange(companies);
        }

        private Dictionary<string, Administrator> BuildAdministrators(IList<CompanyCSV> companiesCSV, CategoryType category)
        {
            if (category == CategoryType.Stock)
            {
                return new Dictionary<string, Administrator>();
            }

            var administrators = new Dictionary<string, Administrator>();

            foreach (var companyCSV in companiesCSV)
            {
                if (!administrators.ContainsKey(companyCSV.AdministratorCnpj))
                {
                    administrators.Add(companyCSV.AdministratorCnpj, new Administrator(companyCSV.Administrator, companyCSV.AdministratorCnpj));
                }
            }

            return administrators;
        }

        private IList<Company> BuildCompanies(IList<CompanyCSV> companiesCSV, Dictionary<string, Administrator> administrators, CategoryType category)
        {
            var companies = new Dictionary<string, Company>();

            foreach (var companyCSV in companiesCSV)
            {
                if (!companies.ContainsKey(companyCSV.Cnpj))
                {
                    companies.Add(companyCSV.Cnpj, new Company(companyCSV, category));
                }

                var ticker = new Ticker(companyCSV.Ticker);

                companies[companyCSV.Cnpj].AddTicker(ticker);

                if (category == CategoryType.RealEstate)
                {
                    companies[companyCSV.Cnpj].UpdateAdministrator(administrators[companyCSV.AdministratorCnpj]);
                }
            }

            return new List<Company>(companies.Values);
        }
    }
}
