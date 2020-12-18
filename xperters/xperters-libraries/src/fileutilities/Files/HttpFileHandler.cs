using Microsoft.AspNetCore.Http;
using xperters.fileutilities.Interfaces;

namespace xperters.fileutilities.Files
{
    public class HttpFileHandler : IHttpFileHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpFileHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IFormFileCollection GetFromFiles()
        {
            return _httpContextAccessor.HttpContext.Request.Form.Files;
        }
    }
}

