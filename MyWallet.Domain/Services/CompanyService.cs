using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
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

        public async Task Import(IFormFile file, CategoryType categoryType)
        {
            var companiesCSV = ConvertCsvToCompaniesCsv(file);

            var administrators = BuildAdministrators(companiesCSV, categoryType);

            var companies = BuildCompanies(companiesCSV, administrators, categoryType);

            await _companyRepository.AddRange(companies);
        }

        private IList<CompanyCSV> ConvertCsvToCompaniesCsv(IFormFile file)
        {
            var companiesCSV = new List<CompanyCSV>();

            using (var stream = file.OpenReadStream())
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);

                while (reader.Read())
                {
                    companiesCSV.Add(new CompanyCSV(reader));
                }
            }

            return companiesCSV;
        }

        private Dictionary<string, Administrator> BuildAdministrators(IList<CompanyCSV> companiesCSV, CategoryType categoryType)
        {
            var administrators = new Dictionary<string, Administrator>();

            if (categoryType == CategoryType.Stock)
            {
                return administrators;
            }

            foreach (var companyCSV in companiesCSV)
            {
                AddAdministratorInDictionary(administrators, companyCSV);
            }

            return administrators;
        }

        private void AddAdministratorInDictionary(Dictionary<string, Administrator> administrators, CompanyCSV companyCSV)
        {
            if (!administrators.ContainsKey(companyCSV.AdministratorCnpj))
            {
                administrators.Add(companyCSV.AdministratorCnpj, new Administrator(companyCSV.Administrator, companyCSV.AdministratorCnpj));
            }
        }

        private IList<Company> BuildCompanies(IList<CompanyCSV> companiesCSV, Dictionary<string, Administrator> administrators, CategoryType categoryType)
        {
            var companies = new Dictionary<string, Company>();

            foreach (var companyCSV in companiesCSV)
            {
                AddCompanyInDictionary(companies, administrators, companyCSV, categoryType);
            }

            return new List<Company>(companies.Values);
        }

        private void AddCompanyInDictionary(Dictionary<string, Company> companies, Dictionary<string, Administrator> administrators, CompanyCSV companyCSV, CategoryType categoryType)
        {
            if (!companies.ContainsKey(companyCSV.Cnpj))
            {
                companies.Add(companyCSV.Cnpj, new Company(companyCSV, categoryType));
            }

            var ticker = new Ticker(companyCSV.Ticker);

            companies[companyCSV.Cnpj].AddTicker(ticker);

            if (categoryType == CategoryType.RealEstate)
            {
                companies[companyCSV.Cnpj].UpdateAdministrator(administrators[companyCSV.AdministratorCnpj]);
            }
        }
    }
}
