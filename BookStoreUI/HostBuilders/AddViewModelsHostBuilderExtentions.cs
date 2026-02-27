using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreUI.HostBuilders
{
    public static class AddViewModelsHostBuilderExtentions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();

                services.AddTransient<DashboardViewModel>();
                services.AddTransient<BookModelViewModel>();
                services.AddTransient<BookStockViewModel>();
                services.AddTransient<FilterBookViewModel>();
                services.AddTransient<BookStockViewModel>();
                services.AddTransient<AddDelayViewModel>();
                services.AddTransient<AddDiscountViewModel>();
                services.AddTransient<SellBookViewModel>();
                services.AddTransient<ChangeBookStockViewModel>();

                services.AddTransient<BookDiscountsViewModel>();
                services.AddTransient<DelayedBooksViewModel>();
                services.AddTransient<ChangeBookModelViewModel>();

                services.AddTransient<AccountSettingsViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegiserAccountViewModel>();
                services.AddTransient<ChangePasswordViewModel>();
            });

            return hostBuilder;
        }
    }
}