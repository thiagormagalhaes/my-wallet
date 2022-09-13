using Microsoft.AspNetCore.Mvc;
using MyWallet.Api.Application.Interfaces;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [Route("patrimonies")]
    public class PatrimonyController : MainController
    {
        private readonly IPatrimonyService _patrimonyService;
        private readonly IPatrimonyApplication _patrimonyApplication;

        public PatrimonyController(IPatrimonyService patrimonyService, IPatrimonyApplication patrimonyApplication, INotifier notifier) : base(notifier)
        {
            _patrimonyService = patrimonyService;
            _patrimonyApplication = patrimonyApplication;
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

        [HttpGet("balancing")]
        public async Task<IActionResult> Balancing()
        {
            var result = await _patrimonyApplication.Balancing();

            return Response(result);
        }
    }
}
