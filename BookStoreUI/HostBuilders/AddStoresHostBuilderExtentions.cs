using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.Stores.PatternStores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreUI.HostBuilders
{
    public static class AddStoresHostBuilderExtentions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ChangeBookModelControlContextStore>();
                services.AddSingleton<ChangeBookStockControlContextStore>();
                services.AddSingleton<SelectedItemStore>();
                services.AddSingleton<ProductsStore>();
                services.AddSingleton<CurrentUserStore>();
                services.AddSingleton<UserDataPatternsStore>();
            });

            return hostBuilder;
        }
    }
}