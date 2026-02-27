using DLL.Repositories.AccountSettingsRepositories;
using DLL.Repositories.BookDiscountRepositories;
using DLL.Repositories.BookRepositories;
using DLL.Repositories.DelayBookRepositories;
using DLL.Repositories.FilterBookRepositories;
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
                services.AddTransient<IBookRepository, BookRepository>();
                services.AddTransient<IFilterBookRepository, FilterBookRepository>();
                services.AddTransient<IDelayBookRepository, DelayBookRepository>();
                services.AddTransient<IBookDiscountRepository, BookDiscountRepository>();
                services.AddTransient<IAccountSettingsRepository, AccountSettingsRepository>();
            });

            return hostBuilder;
        }
    }
}