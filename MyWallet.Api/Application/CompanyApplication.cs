using AutoMapper;
using MyWallet.Api.Application.Interfaces;
using MyWallet.Api.Responses;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Services;

namespace MyWallet.Api.Application
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public CompanyApplication(IMapper mapper, ICompanyService companyService)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        public async Task<IList<CompanyResponse>> GetByCategory(Category? category)
        {
            var companies = await _companyService.GetByCategory(category);

            return _mapper.Map<List<CompanyResponse>>(companies);
        }

        public async Task<CompanyResponse> GetByTickerCode(string tickerCode)
        {
            var company = await _companyService.GetByTickerCode(tickerCode);

            return _mapper.Map<CompanyResponse>(company);
        }
    }
}
