using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface IRecommendedWalletService
    {
        Task Create(RecommendedWalletDto recommendedWalletDto);
        Task AddRecommendation(long recommendedWalletId, RecommendationDto recommendationDto);
        Task UpdatePrices(long recommendedWalletId);
        Task<IList<RecommendationBuy>> RecommendationsBuy();
    }
}
