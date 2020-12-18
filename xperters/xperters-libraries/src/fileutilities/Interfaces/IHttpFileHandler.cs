using Microsoft.AspNetCore.Http;


namespace xperters.fileutilities.Interfaces
{
    public interface IHttpFileHandler
    {
        IFormFileCollection GetFromFiles();
    }
}