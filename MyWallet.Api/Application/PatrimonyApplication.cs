using AutoMapper;
using MyWallet.Api.Application.Interfaces;
using MyWallet.Api.Responses;
using MyWallet.Domain.Interfaces.Services;

namespace MyWallet.Api.Application
{
    public class PatrimonyApplication : IPatrimonyApplication
    {
        private readonly IMapper _mapper;
        private readonly IPatrimonyService _patrimonyService;

        public PatrimonyApplication(IMapper mapper, IPatrimonyService patrimonyService)
        {
            _mapper = mapper;
            _patrimonyService = patrimonyService;
        }

        public async Task<IList<BalancingResponse>> Balancing()
        {
            var balancing = await _patrimonyService.Balancing();

            return _mapper.Map<IList<BalancingResponse>>(balancing);
        }
    }
}
