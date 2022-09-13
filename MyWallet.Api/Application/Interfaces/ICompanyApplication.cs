using MyWallet.Api.Responses;
using MyWallet.Domain.Enums;

namespace MyWallet.Api.Application.Interfaces
{
    public interface ICompanyApplication
    {
        Task<CompanyResponse> GetByTickerCode(string tickerCode);
    }
}
