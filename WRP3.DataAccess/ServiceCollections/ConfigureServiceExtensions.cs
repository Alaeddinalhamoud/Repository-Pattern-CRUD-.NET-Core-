using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WRP3.DataAccess.EFDBContext;
using WRP3.IServices.Common;

namespace WRP3.DataAccess.ServiceCollections
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddCustomSqlServer(this IServiceCollection services,
          IConfiguration config)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("ConnectionStr")));

            return services;
        }
    }
}
