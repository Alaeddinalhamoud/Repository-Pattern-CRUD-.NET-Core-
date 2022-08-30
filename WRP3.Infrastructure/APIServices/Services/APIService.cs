using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.Infrastructure.APIServices.Services
{
    public class APIService<T> : IAPIService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly ILogger<APIService<T>> _logger;
        private readonly string _APIScope = string.Empty;

        public APIService(IHttpClientFactory httpClientFactory,
            ILogger<APIService<T>> logger,
            ITokenAcquisition tokenAcquisition,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _APIScope = configuration["APIScopes:UserAccess"];

        }
        public async Task<T?> Delete(int? id, string? url)
        {
            try
            {
                HttpClient httpClient = await GetToken();

                var httpResponseMessage = await httpClient.DeleteAsync($"{url}/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync
                          <T>(contentStream, new JsonSerializerOptions()
                          { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error whilw Deleting {nameof(T)}");
                return null;
            }
        }

        public async Task<T?> Get(int? id, string? url)
        {
            try
            {
                HttpClient httpClient = await GetToken();

                var httpResponseMessage = await httpClient.GetAsync($"{url}/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync
                          <T>(contentStream, new JsonSerializerOptions()
                          { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error while Getting {nameof(T)}");
                return null;
            }
        }

        public async Task<List<T>?> GetAll(string? url)
        {
            try
            {
                HttpClient httpClient = await GetToken();

                var httpResponseMessage = await httpClient.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync
                          <List<T>>(contentStream, new JsonSerializerOptions()
                          { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error while Getting All {nameof(T)}");
                return null;
            }
        }



        public async Task<T?> Post(T? t, string? url)
        {
            try
            {
                HttpClient httpClient = await GetToken();

                var content = JsonSerializer.Serialize(t);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var httpResponseMessage = await httpClient.PostAsync(url, httpContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync
                          <T>(contentStream, new JsonSerializerOptions()
                          { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error while Posting {nameof(T)}");
                return null;
            }
        }

        public async Task<T?> Update(T? t, string? url)
        {
            try
            {
                HttpClient httpClient = await GetToken();

                var content = JsonSerializer.Serialize(t);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var httpResponseMessage = await httpClient.PutAsync(url, httpContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync
                          <T>(contentStream, new JsonSerializerOptions()
                          { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error while Updating All {nameof(T)}");
                return null;
            }
        }
        private async Task<HttpClient> GetToken()
        {
            var httpClient = _httpClientFactory.CreateClient("API");

            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _APIScope });
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
