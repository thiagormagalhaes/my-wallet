using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Extensions;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.ValueObjects;
using MyWallet.Scraper.Dto;
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
                BuildCompanyInDictonary(companies, companyCSV, category);

                companies[companyCSV.Cnpj].AddTicker(companyCSV.Ticker);

                if (!companyCSV.HasAdministrator())
                {
                    continue;
                }

                companies[companyCSV.Cnpj].Update(administrators[companyCSV.AdministratorCnpj]);
            }

            return new List<Company>(companies.Values);
        }

        private void BuildCompanyInDictonary(Dictionary<string, Company> companies, CompanyCSV companyCSV, Category category)
        {
            if (!companies.ContainsKey(companyCSV.Cnpj))
            {
                companies.Add(companyCSV.Cnpj, new Company(companyCSV, category));
            }
        }

        public async Task Create(Category category, string tickerCode)
        {
            var scraperStrategyResponse = await _scraperStrategyResolver.FindStrategy(category.GetHashCode())
                .Execute(tickerCode);

            if (!scraperStrategyResponse.IsValid())
            {
                return;
            }

            var company = await CreateCompany(category, scraperStrategyResponse);

            await CreateTicker(company, scraperStrategyResponse, tickerCode);
        }

        private async Task<Company> CreateCompany(Category category, ScraperStrategyResponse scraperStrategyResponse)
        {
            var company = await _companyRepository.GetByCnpjAsync(scraperStrategyResponse.Cnpj);

            if (company is not null)
            {
                return company;
            }

            company = BuildCompany(category, scraperStrategyResponse);

            await _companyRepository.Add(company);

            return company;
        }

        private Company BuildCompany(Category category, ScraperStrategyResponse scraperStrategyResponse)
        {
            var companyDto = BuildCompanyDto(category, scraperStrategyResponse);

            return new Company(companyDto);
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

        private async Task CreateTicker(Company company, ScraperStrategyResponse scraperStrategyResponse, string tickerCode)
        {
            if (company.HasTicker(tickerCode))
            {
                return;
            }

            var ticker = BuildTicker(company, tickerCode, scraperStrategyResponse.GetPrice());

            company.AddTicker(ticker);

            await _companyRepository.Update(company);
        }

        private Ticker BuildTicker(Company company, string tickerCode, decimal? price)
        {
            var tickerDto = new TickerDto(tickerCode, company.Id, price);

            return new Ticker(tickerDto);
        }

        public async Task Update(Category category, string tickerCode)
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
                return;
            }

            company.Update(scraperStrategyResponse.Name);

            if (company.HasTicker(tickerCode))
            {
                company.GetTicker(tickerCode).Update(scraperStrategyResponse.GetPrice());
            }

            await _companyRepository.Update(company);
        }
    }
}
