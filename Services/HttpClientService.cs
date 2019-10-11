using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using nicasource_netcore.Interfaces;

namespace nicasource_netcore.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientFactory = clientFactory;
        }

        public async Task<T> get<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }

            return await response.Content.ReadAsAsync<T>();
        }

        public string getPreviousRequest()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();
        }
    }
}