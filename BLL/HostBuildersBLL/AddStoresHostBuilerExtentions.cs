using DLL.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BLL.HostBuildersBLL
{
    public static class AddDLLStoresHostBuilderExtentions
    {
        public static IHostBuilder AddDLLStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ProductsStore>();
            });

            return hostBuilder;
        }
    }
}