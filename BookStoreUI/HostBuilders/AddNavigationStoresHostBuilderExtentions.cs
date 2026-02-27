using BookStoreUI.Navigation.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreUI.HostBuilders
{
    public static class AddNavigationStoresHostBuilderExtentions
    {
        public static IHostBuilder AddNavigationStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MainNavigationStore>();
                services.AddSingleton<DashboardNavigationStore>();
            });

            return hostBuilder;
        }
    }
}