using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WRP3.Infrastructure.APIServices.IServices;
using WRP3.Infrastructure.APIServices.Services;

namespace WRP3.Infrastructure.APIServices.ServiceCollections
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddCustomAPIServices(this IServiceCollection services,
          IConfiguration config)
        {
            services.AddHttpClient("API", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:44365");
            });
            services.AddScoped(typeof(IAPIService<>), typeof(APIService<>));

            return services;
        }
    }
}
