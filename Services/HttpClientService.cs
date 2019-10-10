using System.Net.Http;
using System.Threading.Tasks;
using nicasource_netcore.Interfaces;

namespace nicasource_netcore.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> get<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);

            return await response.Content.ReadAsAsync<T>();
        }
    }
}