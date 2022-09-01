using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WRP3.Infrastructure.GoogleRecaptcha.IServices;
using WRP3.Infrastructure.GoogleRecaptcha.Models;
using WRP3.Infrastructure.GoogleRecaptcha.Services;

namespace WRP3.Infrastructure.GoogleRecaptcha.ServiceCollections
{
    public static partial class ServiceCollections
    {
        public static IServiceCollection AddCustomGoogleReCaptchaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<GoogleRecaptchaSettings>(config.GetSection(GoogleRecaptchaSettings.GoogleRecaptchaSetting));

            services.AddHttpClient<IGoogleRecaptchaService, GoogleRecaptchaService>();

            return services;
        }
    }
}
