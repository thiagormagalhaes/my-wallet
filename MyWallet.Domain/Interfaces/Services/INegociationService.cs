using Microsoft.AspNetCore.Http;

namespace MyWallet.Domain.Interfaces.Services
{
    public interface INegociationService
    {
        Task Import(IFormFile file);
    }
}
