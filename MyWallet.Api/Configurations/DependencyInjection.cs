using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;
using MyWallet.Domain.Services;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Repositories;

namespace MyWallet.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            RegisterContexts(services);
            RegisterServices(services);
            RegisterRepositories(services);
        }

        public static void RegisterContexts(this IServiceCollection services)
        {
            services.AddScoped<MyWalletContext>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}
