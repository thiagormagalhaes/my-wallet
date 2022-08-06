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
            var companies = ConvertCsvToCompanies(file, categoryType);

            await _companyRepository.AddRange(companies);
        }

        private IList<Company> ConvertCsvToCompanies(IFormFile file, CategoryType categoryType)
        {
            var companies = new Dictionary<string, Company>();

            var companiesCSV = ConvertCsvToCompaniesCsv(file);

            foreach (var companyCSV in companiesCSV)
            {
                BuildCompanyInDictionary(companies, companyCSV, categoryType);

                var ticker = new Ticker(companyCSV.Ticker);

                companies[companyCSV.Cnpj].AddTicker(ticker);
            }

            return new List<Company>(companies.Values);
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

        private void BuildCompanyInDictionary(Dictionary<string, Company> dictionary, CompanyCSV companyCSV, CategoryType categoryType)
        {
            if (!dictionary.ContainsKey(companyCSV.Cnpj))
            {
                dictionary.Add(companyCSV.Cnpj, new Company(companyCSV, categoryType));
            }
        }

    }
}
