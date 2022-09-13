using MyWallet.Api.Responses;

namespace MyWallet.Api.Application.Interfaces
{
    public interface IPatrimonyApplication
    {
        Task<IList<BalancingResponse>> Balancing();
    }
}
