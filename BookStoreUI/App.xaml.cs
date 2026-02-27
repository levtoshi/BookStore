using BLL.HostBuildersBLL;
using BookStoreUI.HostBuilders;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.BaseViewModels;
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
                .AddRepositories()
                .AddStores()
                .AddServices()
                .AddViewModels()
                .AddNavigationStores()
                .AddNavigationServices()
                .ConfigureServices((context, services) =>
                {
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