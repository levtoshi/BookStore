using DLL.Repositories.AccountSettingsRepositories;
using DLL.Repositories.BookDiscountRepositories;
using DLL.Repositories.BookModelRepositories;
using DLL.Repositories.BookStatisticRepositories;
using DLL.Repositories.BookStockRepositories;
using DLL.Repositories.BookStoreProviderRepositories;
using DLL.Repositories.DelayBookRepositories;
using DLL.Repositories.SearchBookRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BLL.HostBuildersBLL
{
    public static class AddRepositoriesHostBuilderExtentions
    {
        public static IHostBuilder AddRepositories(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IBookModelRepository, BookModelRepository>();
                services.AddTransient<IBookStockRepository, BookStockRepository>();
                services.AddTransient<IDelayBookRepository, DelayBookRepository>();
                services.AddTransient<IBookDiscountRepository, BookDiscountRepository>();
                services.AddTransient<ISearchBookRepository, SearchBookRepository>();
                services.AddTransient<IBookStatisticRepository, BookStatisticRepository>();
                services.AddTransient<IBookStoreProviderRepository, BookStoreProviderRepository>();
                services.AddTransient<IAccountSettingsRepository, AccountSettingsRepository>();
            });

            return hostBuilder;
        }
    }
}