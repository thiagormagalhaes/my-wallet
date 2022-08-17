using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Extensions;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.ValueObjects;
using MyWallet.Scraper.Dto;
using MyWallet.Scraper.Enums;
using MyWallet.Scraper.Extensions;
using MyWallet.Scraper.Interfaces;
using Category = MyWallet.Domain.Enums.Category;

namespace MyWallet.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IScraperStrategyResolver _scraperStrategyResolver;

        public CompanyService(ICompanyRepository companyRepository, IScraperStrategyResolver scraperStrategyResolver)
        {
            _companyRepository = companyRepository;
            _scraperStrategyResolver = scraperStrategyResolver;
        }

        public async Task Import(IFormFile file, Category category)
        {
            var companiesCSV = file.ConvertCSV<CompanyCSV>();

            var administrators = BuildAdministrators(companiesCSV, category);

            var companies = BuildCompanies(companiesCSV, administrators, category);

            await _companyRepository.AddRange(companies);
        }

        private Dictionary<string, Administrator> BuildAdministrators(IList<CompanyCSV> companiesCSV, Category category)
        {
            if (category == Category.Stock)
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

        private IList<Company> BuildCompanies(IList<CompanyCSV> companiesCSV, Dictionary<string, Administrator> administrators, Category category)
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

                if (category == Category.RealEstate)
                {
                    companies[companyCSV.Cnpj].UpdateAdministrator(administrators[companyCSV.AdministratorCnpj]);
                }
            }

            return new List<Company>(companies.Values);
        }

        public async Task CreateOrUpdate(Category category, string tickerCode)
        {
            var scraperStrategyResponse = await _scraperStrategyResolver.FindStrategy(category.GetHashCode())
                .Execute(tickerCode);

            if (!scraperStrategyResponse.IsValid())
            {
                return;
            }

            var company = await _companyRepository.GetByCnpjAsync(scraperStrategyResponse.Cnpj);

            if (company is null)
            {
                company = await Create(category, scraperStrategyResponse);
            } else
            {
                company.UpdateName(scraperStrategyResponse.Name);
                await _companyRepository.Update(company);
            }

            if (!company.HasTicker(tickerCode))
            {
                await AddTicker(company, tickerCode, scraperStrategyResponse.GetPrice());
            }

            // TODO: Contemplar as Company que possuem Administrator
        }

        private async Task<Company> Create(Category category, ScraperStrategyResponse scraperStrategyResponse)
        {
            var companyDto = BuildCompanyDto(category, scraperStrategyResponse);

            var company = new Company(companyDto);

            await _companyRepository.Add(company);

            return company;
        }

        private async Task AddTicker(Company company, string tickerCode, decimal price)
        {
            var tickerDto = new TickerDto(tickerCode, company.Id, price);

            var ticker = new Ticker(tickerDto);

            company.AddTicker(ticker);

            await _companyRepository.Update(company);
        }

        // TODO: Contemplar as Company que possuem Administrator
        private CompanyDto BuildCompanyDto(Category category, ScraperStrategyResponse scraperStrategyResponse)
        {
            return new CompanyDto(
                scraperStrategyResponse.Name,
                scraperStrategyResponse.Cnpj,
                category,
                null
            );
        }
    }
}
