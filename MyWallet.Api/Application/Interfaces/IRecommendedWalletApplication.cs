using MyWallet.Api.Responses;

namespace MyWallet.Api.Application.Interfaces
{
    public interface IRecommendedWalletApplication
    {
        Task<IList<RecommendationBuyResponse>> AllRecommendationsBuy();
    }
}
