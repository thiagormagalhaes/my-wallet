using MyWallet.Api.Application.Interfaces;
using MyWallet.Api.Responses;
using MyWallet.Domain.Interfaces.Services;

namespace MyWallet.Api.Application
{
    public class RecommendedWalletApplication : IRecommendedWalletApplication
    {
        private readonly IRecommendedWalletService _recommendedWalletService;

        public RecommendedWalletApplication(IRecommendedWalletService recommendedWalletService)
        {
            _recommendedWalletService = recommendedWalletService;
        }

        public async Task<IList<RecommendationBuyResponse>> AllRecommendationsBuy()
        {
            var response = new List<RecommendationBuyResponse>();

            var recommendations = await _recommendedWalletService.AllRecommendationsBuy();

            foreach (var recommendation in recommendations)
            {
                response.Add(new RecommendationBuyResponse(recommendation));
            }

            return response
                .OrderByDescending(x => x.Discount)
                .ToList();
        }
    }
}
