using AutoMapper;

namespace MyWallet.Api.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToResponseProfile());
                cfg.AddProfile(new EntityToResponseProfile());
            });
        }
    }
}
