using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task Import(IFormFile file, Category category);
        Task Create(Category category, string tickerCode);
        Task Update(Category category, string tickerCode);
        Task<IList<Company>> GetByCategory(Category? category);
        Task<Company?> GetByTickerCode(string tickerCode);
    }
}
