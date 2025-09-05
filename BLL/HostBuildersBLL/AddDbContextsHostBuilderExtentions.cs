using DLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BLL.HostBuildersBLL
{
    public static class AddDbContextsHostBuilderExtentions
    {
        public static IHostBuilder AddDbContexts(this IHostBuilder hostBuilder, string connectionString)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddDbContext<BookStoreContext>(opt =>
                {
                    opt.UseSqlServer(connectionString);
                    //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
            });

            return hostBuilder;
        }
    }
}