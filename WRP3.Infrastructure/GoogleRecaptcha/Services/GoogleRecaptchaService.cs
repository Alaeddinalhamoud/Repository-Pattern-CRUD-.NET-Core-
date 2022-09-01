using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WRP3.Infrastructure.GoogleRecaptcha.IServices;
using WRP3.Infrastructure.GoogleRecaptcha.Models;

namespace WRP3.Infrastructure.GoogleRecaptcha.Services
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        private readonly HttpClient httpClient;
        private readonly IOptions<GoogleRecaptchaSettings> options;
        private readonly ILogger<GoogleRecaptchaService> logger;
        public GoogleRecaptchaService(HttpClient httpClient, IOptions<GoogleRecaptchaSettings> options,
                                      ILogger<GoogleRecaptchaService> logger)
        {
            this.httpClient = httpClient;
            this.options = options;
            this.logger = logger;
        }
        public async Task<GoogleReCaptcha> SiteVerify(GoogleReCaptcha googleReCaptcha)
        {
            try
            {
                var httpResponseMessage = await httpClient.GetStringAsync
              ($"https://www.google.com/recaptcha/api/siteverify?secret={options.Value.SecretKey}&response={googleReCaptcha.GoogleRecaptchaToken}");

                return JsonConvert.DeserializeObject<GoogleReCaptcha>(httpResponseMessage);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error while processing Google Recaptcha Service");
                return new GoogleReCaptcha() { Success = false };
            }
        }
    }
}