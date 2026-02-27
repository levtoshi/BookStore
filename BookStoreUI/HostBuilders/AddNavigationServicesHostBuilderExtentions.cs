using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreUI.HostBuilders
{
    public static class AddNavigationServicesHostBuilderExtentions
    {
        public static IHostBuilder AddNavigationServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<Func<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
                services.AddTransient<IMainNavigationService<DashboardViewModel>, MainNavigationService<DashboardViewModel>>();

                services.AddTransient<Func<BookDiscountsViewModel>>((s) => () => s.GetRequiredService<BookDiscountsViewModel>());
                services.AddTransient<IMainNavigationService<BookDiscountsViewModel>, MainNavigationService<BookDiscountsViewModel>>();

                services.AddTransient<Func<DelayedBooksViewModel>>((s) => () => s.GetRequiredService<DelayedBooksViewModel>());
                services.AddTransient<IMainNavigationService<DelayedBooksViewModel>, MainNavigationService<DelayedBooksViewModel>>();

                services.AddTransient<Func<AccountSettingsViewModel>>((s) => () => s.GetRequiredService<AccountSettingsViewModel>());
                services.AddTransient<IMainNavigationService<AccountSettingsViewModel>, MainNavigationService<AccountSettingsViewModel>>();

                services.AddTransient<Func<LoginViewModel>>((s) => () => s.GetRequiredService<LoginViewModel>());
                services.AddTransient<IMainNavigationService<LoginViewModel>, MainNavigationService<LoginViewModel>>();

                services.AddTransient<Func<ChangePasswordViewModel>>((s) => () => s.GetRequiredService<ChangePasswordViewModel>());
                services.AddTransient<IMainNavigationService<ChangePasswordViewModel>, MainNavigationService<ChangePasswordViewModel>>();

                services.AddTransient<Func<ChangePasswordViewModel>>((s) => () => s.GetRequiredService<ChangePasswordViewModel>());
                services.AddTransient<IMainNavigationService<ChangePasswordViewModel>, MainNavigationService<ChangePasswordViewModel>>();

                services.AddTransient<Func<RegiserAccountViewModel>>((s) => () => s.GetRequiredService<RegiserAccountViewModel>());
                services.AddTransient<IMainNavigationService<RegiserAccountViewModel>, MainNavigationService<RegiserAccountViewModel>>();

                services.AddTransient<Func<ChangeBookModelViewModel>>((s) => () => s.GetRequiredService<ChangeBookModelViewModel>());
                services.AddTransient<IMainNavigationService<ChangeBookModelViewModel>, MainNavigationService<ChangeBookModelViewModel>>();

                services.AddTransient<Func<AddDiscountViewModel>>((s) => () => s.GetRequiredService<AddDiscountViewModel>());
                services.AddTransient<IMainNavigationService<AddDiscountViewModel>, MainNavigationService<AddDiscountViewModel>>();

                services.AddTransient<Func<AddDelayViewModel>>((s) => () => s.GetRequiredService<AddDelayViewModel>());
                services.AddTransient<IMainNavigationService<AddDelayViewModel>, MainNavigationService<AddDelayViewModel>>();

                services.AddTransient<Func<BookModelViewModel>>((s) => () => s.GetRequiredService<BookModelViewModel>());
                services.AddTransient<IDashboardNavigationService<BookModelViewModel>, DashboardNavigationService<BookModelViewModel>>();

                services.AddTransient<Func<BookStockViewModel>>((s) => () => s.GetRequiredService<BookStockViewModel>());
                services.AddTransient<IDashboardNavigationService<BookStockViewModel>, DashboardNavigationService<BookStockViewModel>>();

                services.AddTransient<Func<FilterBookViewModel>>((s) => () => s.GetRequiredService<FilterBookViewModel>());
                services.AddTransient<IDashboardNavigationService<FilterBookViewModel>, DashboardNavigationService<FilterBookViewModel>>();

                services.AddTransient<Func<ChangeBookStockViewModel>>((s) => () => s.GetRequiredService<ChangeBookStockViewModel>());
                services.AddTransient<IDashboardNavigationService<ChangeBookStockViewModel>, DashboardNavigationService<ChangeBookStockViewModel>>();

                services.AddTransient<Func<SellBookViewModel>>((s) => () => s.GetRequiredService<SellBookViewModel>());
                services.AddTransient<IDashboardNavigationService<SellBookViewModel>, DashboardNavigationService<SellBookViewModel>>();
            });

            return hostBuilder;
        }
    }
}