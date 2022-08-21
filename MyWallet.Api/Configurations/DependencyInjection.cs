using MyWallet.Api.Application;
using MyWallet.Api.Application.Interfaces;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;
using MyWallet.Domain.Services;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Repositories;
using MyWallet.Scraper.Extensions;

namespace MyWallet.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScrapperStrategy();

            RegisterContexts(services);
            RegisterApplication(services);
            RegisterServices(services);
            RegisterRepositories(services);
        }

        public static void RegisterContexts(this IServiceCollection services)
        {
            services.AddScoped<MyWalletContext>();
        }

        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<ICompanyApplication, CompanyApplication>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<INegociationService, NegociationService>();
            services.AddScoped<IPatrimonyService, PatrimonyService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITickerRepository, TickerRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<INegociationRepository, NegociationRepository>();
            services.AddScoped<IPatrimonyRepository, PatrimonyRepository>();
        }
    }
}
