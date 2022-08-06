using Microsoft.AspNetCore.Mvc;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [Route("companies")]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService, INotifier notifier) : base(notifier)
        {
            _companyService = companyService;
        }

        [HttpPost("import-stocks")]
        public async Task<IActionResult> Load(IFormFile file)
        {
            await _companyService.Import(file, CategoryType.Stock);

            return Response();
        }
    }
}
