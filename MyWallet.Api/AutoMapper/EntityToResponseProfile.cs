using AutoMapper;
using MyWallet.Api.Responses;
using MyWallet.Domain.Entities;

namespace MyWallet.Api.AutoMapper
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Ticker, TickerResponse>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<Administrator, AdministratorResponse>();
        }
    }
}
