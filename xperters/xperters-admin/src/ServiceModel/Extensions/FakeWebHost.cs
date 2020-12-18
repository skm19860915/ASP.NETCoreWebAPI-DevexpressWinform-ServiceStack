using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Xperters.Admin.ServiceModel.Extensions
{
    public class FakeWebHost : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "DEV";
        public string ApplicationName { get; set; } = "Xperters";
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }

        public FakeWebHost()
        {
            ContentRootPath = Directory.GetCurrentDirectory();
        }
    }
}
