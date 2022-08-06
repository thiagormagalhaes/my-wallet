using MyWallet.Domain.Notifications;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            RegisterContexts(services);
        }

        public static void RegisterContexts(this IServiceCollection services)
        {
            services.AddScoped<MyWalletContext>();
        }
    }
}
