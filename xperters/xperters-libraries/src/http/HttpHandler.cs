using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using xperters.http.Interfaces;

namespace xperters.http
{
    public class HttpHandler : IHttpHandler
    {
        protected readonly HttpClient Client;
        private readonly HttpMethod _patch;
        private readonly ILogger<HttpHandler> _logger;

        public HttpHandler(ILoggerFactory loggerFactory)
        {
            Client = new HttpClient();
            _patch = new HttpMethod("PATCH");

            _logger = loggerFactory.CreateLogger<HttpHandler>();
        }

        public HttpRequestHeaders RequestHeaders => Client.DefaultRequestHeaders;

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await Client.GetAsync(url);
        }


        private void AddHeaders(IList<(string name, string value)> headers)
        {
            // clear the headers before attempting a new request
            Client.DefaultRequestHeaders.Clear();

            foreach (var header in headers)
            {
                Client.DefaultRequestHeaders.Add(header.name, header.value);
            }

            _logger.LogDebug("Added headers");
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent content)
        {
            return await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostAsyncWithAuthorization(string authorizationToken, string url, HttpContent content)
        {
            Client.DefaultRequestHeaders.Add("Authorization", authorizationToken);

            return await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostAsyncWithHeaders(IList<(string name, string value)> headers, string url, HttpContent content)
        {
            AddHeaders(headers);

            return await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> GetAsyncWithHeaders(IList<(string name, string value)> headers, string url)
        {
            AddHeaders(headers);

            return await Client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string url, HttpContent content)
        {
            return await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, HttpContent content)
        {
            var request = new HttpRequestMessage(_patch, url) { Content = content };

            return await Client.SendAsync(request);
        }
    }
}
