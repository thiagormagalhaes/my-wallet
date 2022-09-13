using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface IRecommendedWalletService
    {
        Task Create(RecommendedWalletDto recommendedWalletDto);
        Task AddRecommendation(long recommendedWalletId, RecommendationDto recommendationDto);
        Task UpdatePrices(long recommendedWalletId);
        Task<IList<Recommendation>> AllRecommendationsBuy();
    }
}
