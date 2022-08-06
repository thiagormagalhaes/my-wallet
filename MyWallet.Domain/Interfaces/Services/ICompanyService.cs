using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Enums;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task Import(IFormFile file, CategoryType categoryType);
    }
}
