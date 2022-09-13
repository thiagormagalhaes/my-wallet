using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface IPatrimonyService
    {
        Task Consolidate();
        Task UpdatePrices();
        Task<IList<BalancingDto>> Balancing();
    }
}
