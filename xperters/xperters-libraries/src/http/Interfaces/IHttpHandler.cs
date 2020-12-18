using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace xperters.http.Interfaces
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> GetAsyncWithHeaders(IList<(string name, string value)> headers, string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content);
        Task<HttpResponseMessage> PostAsyncWithAuthorization(string authorizationToken, string url, HttpContent content);
        Task<HttpResponseMessage> PostAsyncWithHeaders(IList<(string name, string value)> headers, string url, HttpContent content);
        HttpRequestHeaders RequestHeaders { get; }

    }
}
