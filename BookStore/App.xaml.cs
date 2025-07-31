using System.Configuration;
using System.Data;
using System.Windows;
using BLL.Configurations;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;

using Microsoft.Extensions.DependencyInjection;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private string _connectionString;

        public App()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            var service = new ServiceCollection();
            ConfigurationService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigurationService(ServiceCollection service)
        {
            service.AddTransient(typeof(IService<ProductInfo>), typeof(BookStoreService));
            service.AddTransient(typeof(MainWindow));
            ConfigurationBLL.ConfigurationServiceCollection(service, _connectionString);
        }

        private void OnStartUp(object sender, StartupEventArgs arg)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
        }
    }
}
