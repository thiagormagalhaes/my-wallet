using MyWallet.Domain.Entities;

namespace MyWallet.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company, long>
    {
        Task<Company?> GetByCnpjAsync(string cnpj);
    }
}
