using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace xperters.tests.common.Interfaces
{
    public interface IStartupConfigurationService
    {
        void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory);

        void ConfigureEnvironment(IHostEnvironment env);

        void ConfigureService(IServiceCollection services, IConfigurationRoot configuration);
    }
}