using BLL.Services.AccountSettingsServices;
using BLL.Services.BookDiscountServices;
using BLL.Services.BookServices;
using BLL.Services.DelayBookServices;
using BLL.Services.FilterBookServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreUI.HostBuilders
{
    public static class AddServicesHostBuilderExtentions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IBookService, BookService>();
                services.AddTransient<IDelayBookService, DelayBookService>();
                services.AddTransient<IBookDiscountService, BookDiscountService>();
                services.AddTransient<IFilterBookService, FilterBookService>();
                services.AddTransient<IAccountSettingsService, AccountSettingsService>();
            });

            return hostBuilder;
        }
    }
}