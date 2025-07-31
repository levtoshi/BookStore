using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL;
using DLL.Interfaces;
using DLL.Models;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Configurations
{
    public static class ConfigurationBLL
    {
        public static void ConfigurationServiceCollection(ServiceCollection services, string connectionString)
        {
            services.AddTransient(typeof(IRepository<Product>), typeof(BookStoreRepository));
            services.AddDbContext<BookStoreContext>(opt => {
                opt.UseSqlServer(connectionString);
                //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}