using Microsoft.AspNetCore.Mvc;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [Route("patrimonies")]
    public class PatrimonyController : MainController
    {
        private readonly IPatrimonyService _patrimonyService;

        public PatrimonyController(IPatrimonyService patrimonyService, INotifier notifier) : base(notifier)
        {
            _patrimonyService = patrimonyService;
        }

        [HttpPost("consolidate")]
        public async Task<IActionResult> Consolidate()
        {
            await _patrimonyService.Consolidate();

            return Response();
        }

        [HttpPut("update-prices")]
        public async Task<IActionResult> UpdatePrices()
        {
            await _patrimonyService.UpdatePrices();

            return Response();
        }
    }
}
