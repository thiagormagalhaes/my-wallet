using Microsoft.AspNetCore.Mvc;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;
using MyWallet.Domain.Services;

namespace MyWallet.Api.Controllers
{
    [Route("negociations")]
    public class NegociationController : MainController
    {
        private readonly INegociationService _negociationService;

        public NegociationController(INotifier notifier, INegociationService negociationService) : base(notifier)
        {
            _negociationService = negociationService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            await _negociationService.Import(file);

            return Response();
        }
    }
}
