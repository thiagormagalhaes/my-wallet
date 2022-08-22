using Microsoft.AspNetCore.Mvc;
using MyWallet.Domain.Dto;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [Route("recommended-wallet")]
    public class RecommendedWalletController : MainController
    {
        private readonly IRecommendedWalletService _recommendedWalletService;

        public RecommendedWalletController(IRecommendedWalletService recommendedWalletService, INotifier notifier) : base(notifier)
        {
            _recommendedWalletService = recommendedWalletService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecommendedWalletDto recommendedWalletDto)
        {
            await _recommendedWalletService.Create(recommendedWalletDto);

            return Response();
        }

        [HttpPost("{recommendedWalletId:long}/recommendation")]
        public async Task<IActionResult> AddRecommendation(long recommendedWalletId, [FromBody] RecommendationDto recommendedWalletDto)
        {
            await _recommendedWalletService.AddRecommendation(recommendedWalletId, recommendedWalletDto);

            return Response();
        }

        [HttpPut("{recommendedWalletId:long}/update-prices")]
        public async Task<IActionResult> UpdatePrices(long recommendedWalletId)
        {
            await _recommendedWalletService.UpdatePrices(recommendedWalletId);

            return Response();
        }

        [HttpGet("recommendations-buy")]
        public async Task<IActionResult> RecommendationsBuy()
        {
            var result = await _recommendedWalletService.RecommendationsBuy();

            return Response(result);
        }
    }
}
