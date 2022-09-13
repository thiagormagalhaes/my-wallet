using Microsoft.AspNetCore.Mvc;
using MyWallet.Api.Application.Interfaces;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [Route("companies")]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyApplication _companyApplication;

        public CompanyController(ICompanyService companyService, ICompanyApplication companyApplication, INotifier notifier) 
            : base(notifier)
        {
            _companyService = companyService;
            _companyApplication = companyApplication;
        }

        [HttpPost("import-stocks")]
        public async Task<IActionResult> ImportStocks(IFormFile file)
        {
            await _companyService.Import(file, Category.Stock);

            return Response();
        }

        [HttpPost("import-real-estate")]
        public async Task<IActionResult> ImportRealEstate(IFormFile file)
        {
            await _companyService.Import(file, Category.RealEstate);

            return Response();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category, string tickerCode)
        {
            await _companyService.Create(category, tickerCode);

            return Response();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category, string tickerCode)
        {
            await _companyService.Update(category, tickerCode);

            return Response();
        }

        [HttpGet("{tickerCode}")]
        public async Task<IActionResult> GetByTickerCode(string tickerCode)
        {
            var company = await _companyApplication.GetByTickerCode(tickerCode);

            return Response(company);
        }
    }
}
