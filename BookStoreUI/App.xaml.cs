using BLL.HostBuildersBLL;
using BLL.Services.AccountSettingsServices;
using BLL.Services.BookDiscountServices;
using BLL.Services.BookModelServices;
using BLL.Services.BookStatisticServices;
using BLL.Services.BookStockServices;
using BLL.Services.BookStoreProviderServices;
using BLL.Services.DelayBookServices;
using BLL.Services.SearchBookServices;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Navigation.Stores;
using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.Stores.PatternStores;
using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Windows;

namespace BookStoreUI
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

            _host = Host.CreateDefaultBuilder()
                .AddDbContexts(connectionString)
                .AddDLLStores()
                .AddRepositories()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ChangeBookModelControlContextStore>();
                    services.AddSingleton<ChangeBookStockControlContextStore>();
                    services.AddSingleton<BookShopStore>();
                    services.AddSingleton<SelectedItemStore>();
                    services.AddSingleton<CurrentUserStore>();
                    services.AddSingleton<UserDataPatternsStore>();

                    services.AddTransient<IBookModelService, BookModelService>();
                    services.AddTransient<IBookStockService, BookStockService>();
                    services.AddTransient<IDelayBookService, DelayBookService>();
                    services.AddTransient<IBookDiscountService, BookDiscountService>();
                    services.AddTransient<ISearchBookService, SearchBookService>();
                    services.AddTransient<IBookStatisticService, BookStatisticService>();
                    services.AddTransient<IBookStoreProviderService, BookStoreProviderService>();
                    services.AddTransient<IAccountSettingsService, AccountSettingsService>();

                    services.AddSingleton<MainViewModel>();

                    services.AddTransient<DashboardViewModel>();
                    services.AddTransient<BookModelViewModel>();
                    services.AddTransient<BookStockViewModel>();
                    services.AddTransient<SearchBookViewModel>();
                    services.AddTransient<BookStatisticViewModel>();
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

                    services.AddSingleton<MainNavigationStore>();
                    services.AddSingleton<DashboardNavigationStore>();

                    services.AddSingleton<Func<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
                    services.AddSingleton<IMainNavigationService<DashboardViewModel>, MainNavigationService<DashboardViewModel>>();

                    services.AddSingleton<Func<BookDiscountsViewModel>>((s) => () => s.GetRequiredService<BookDiscountsViewModel>());
                    services.AddSingleton<IMainNavigationService<BookDiscountsViewModel>, MainNavigationService<BookDiscountsViewModel>>();

                    services.AddSingleton<Func<DelayedBooksViewModel>>((s) => () => s.GetRequiredService<DelayedBooksViewModel>());
                    services.AddSingleton<IMainNavigationService<DelayedBooksViewModel>, MainNavigationService<DelayedBooksViewModel>>();

                    services.AddSingleton<Func<AccountSettingsViewModel>>((s) => () => s.GetRequiredService<AccountSettingsViewModel>());
                    services.AddSingleton<IMainNavigationService<AccountSettingsViewModel>, MainNavigationService<AccountSettingsViewModel>>();

                    services.AddSingleton<Func<LoginViewModel>>((s) => () => s.GetRequiredService<LoginViewModel>());
                    services.AddSingleton<IMainNavigationService<LoginViewModel>, MainNavigationService<LoginViewModel>>();

                    services.AddSingleton<Func<ChangePasswordViewModel>>((s) => () => s.GetRequiredService<ChangePasswordViewModel>());
                    services.AddSingleton<IMainNavigationService<ChangePasswordViewModel>, MainNavigationService<ChangePasswordViewModel>>();

                    services.AddSingleton<Func<ChangePasswordViewModel>>((s) => () => s.GetRequiredService<ChangePasswordViewModel>());
                    services.AddSingleton<IMainNavigationService<ChangePasswordViewModel>, MainNavigationService<ChangePasswordViewModel>>();

                    services.AddSingleton<Func<RegiserAccountViewModel>>((s) => () => s.GetRequiredService<RegiserAccountViewModel>());
                    services.AddSingleton<IMainNavigationService<RegiserAccountViewModel>, MainNavigationService<RegiserAccountViewModel>>();

                    services.AddSingleton<Func<ChangeBookModelViewModel>>((s) => () => s.GetRequiredService<ChangeBookModelViewModel>());
                    services.AddSingleton<IMainNavigationService<ChangeBookModelViewModel>, MainNavigationService<ChangeBookModelViewModel>>();

                    services.AddSingleton<Func<AddDiscountViewModel>>((s) => () => s.GetRequiredService<AddDiscountViewModel>());
                    services.AddSingleton<IMainNavigationService<AddDiscountViewModel>, MainNavigationService<AddDiscountViewModel>>();

                    services.AddSingleton<Func<AddDelayViewModel>>((s) => () => s.GetRequiredService<AddDelayViewModel>());
                    services.AddSingleton<IMainNavigationService<AddDelayViewModel>, MainNavigationService<AddDelayViewModel>>();



                    services.AddSingleton<Func<BookModelViewModel>>((s) => () => s.GetRequiredService<BookModelViewModel>());
                    services.AddSingleton<IDashboardNavigationService<BookModelViewModel>, DashboardNavigationService<BookModelViewModel>>();

                    services.AddSingleton<Func<BookStockViewModel>>((s) => () => s.GetRequiredService<BookStockViewModel>());
                    services.AddSingleton<IDashboardNavigationService<BookStockViewModel>, DashboardNavigationService<BookStockViewModel>>();

                    services.AddSingleton<Func<SearchBookViewModel>>((s) => () => s.GetRequiredService<SearchBookViewModel>());
                    services.AddSingleton<IDashboardNavigationService<SearchBookViewModel>, DashboardNavigationService<SearchBookViewModel>>();

                    services.AddSingleton<Func<BookStatisticViewModel>>((s) => () => s.GetRequiredService<BookStatisticViewModel>());
                    services.AddSingleton<IDashboardNavigationService<BookStatisticViewModel>, DashboardNavigationService<BookStatisticViewModel>>();

                    services.AddSingleton<Func<ChangeBookStockViewModel>>((s) => () => s.GetRequiredService<ChangeBookStockViewModel>());
                    services.AddSingleton<IDashboardNavigationService<ChangeBookStockViewModel>, DashboardNavigationService<ChangeBookStockViewModel>>();

                    services.AddSingleton<Func<SellBookViewModel>>((s) => () => s.GetRequiredService<SellBookViewModel>());
                    services.AddSingleton<IDashboardNavigationService<SellBookViewModel>, DashboardNavigationService<SellBookViewModel>>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            //IMainNavigationService<DashboardViewModel> mainNavigationService = _host.Services.GetRequiredService<IMainNavigationService<DashboardViewModel>>();
            //mainNavigationService.Navigate();

            //IDashboardNavigationService<BookModelViewModel> dashboardNavigationService = _host.Services.GetRequiredService<IDashboardNavigationService<BookModelViewModel>>();
            //await dashboardNavigationService.Navigate();

            IMainNavigationService<LoginViewModel> mainNavigationService = _host.Services.GetRequiredService<IMainNavigationService<LoginViewModel>>();
            mainNavigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}