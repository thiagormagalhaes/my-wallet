namespace MyWallet.Domain.Interfaces.Services
{
    public interface IPatrimonyService
    {
        Task Consolidate();
        Task UpdatePrices();
    }
}
