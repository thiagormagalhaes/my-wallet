using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task Import(IFormFile file, Category category);
        Task CreateOrUpdate(Category category, string tickerCode);
    }
}
