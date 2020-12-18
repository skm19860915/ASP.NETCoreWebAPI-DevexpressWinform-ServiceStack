using System.IO;
using System.Reflection;
using Microsoft.Extensions.Hosting;


namespace xperters.fileio
{
    public class FilesHandler : IHandleFiles
    {
        private readonly IHostEnvironment _env;

        public FilesHandler(IHostEnvironment env)
        {
            _env = env;
        }
        public string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); ;
        }

        public string GetContentRootPath()
        {
            return _env.ContentRootPath;
        }

        public bool CheckFilePathExists(string path, string file)
        {
            var fullPath = Path.Combine(path, file);
            return File.Exists(fullPath);
        }
    }
}
