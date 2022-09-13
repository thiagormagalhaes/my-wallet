using AutoMapper;
using MyWallet.Api.Responses;
using MyWallet.Domain.Dto;

namespace MyWallet.Api.AutoMapper
{
    public class DtoToResponseProfile : Profile
    {
        public DtoToResponseProfile()
        {
            CreateMap<BalancingDto, BalancingResponse>();
        }
    }
}
