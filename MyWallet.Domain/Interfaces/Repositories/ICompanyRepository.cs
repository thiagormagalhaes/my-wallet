using MyWallet.Domain.Entities;

namespace MyWallet.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company?> GetByCnpjAsync(string cnpj);
    }
}
