using Microsoft.Extensions.Logging;
using System.Text.Json;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.Infrastructure.APIServices.Services
{
    public class APIService<T> : IAPIService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<APIService<T>> _logger;
        public APIService(IHttpClientFactory httpClientFactory, ILogger<APIService<T>> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public Task<bool> Delete(int? id, string urlen)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(string id, string url)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>?> GetAll(string? url)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");

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
                _logger.LogCritical(ex, $"Error will processing {nameof(T)}");
                return null;
            }
        }

        public Task<bool> Post(T t, string url)
        {
            throw new NotImplementedException();
        }
    }
}
