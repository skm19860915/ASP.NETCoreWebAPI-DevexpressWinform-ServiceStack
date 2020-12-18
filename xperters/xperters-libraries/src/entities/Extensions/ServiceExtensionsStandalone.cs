using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.fileio;

namespace xperters.entities.Extensions
{
    public static class ServiceExtensionsStandalone
    {
        public static void ConfigureDependenciesStandAlone(this IServiceCollection services, IHostEnvironment hostingEnvironment, IHandleEnvironment handler = null)
        {
            if (handler == null)
            {
                services.AddSingleton<IHandleEnvironment, EnvironmentHandler>();
            }
            else
            {
                services.AddSingleton(handler);
            }

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConsole();
                var instrumentationKey = config["ApplicationInsights:InstrumentationKey"];

                builder.AddApplicationInsights(instrumentationKey);
            });

            services.AddSingleton(loggerFactory);
            services.AddTransient<IHandleFiles>(f => new FilesHandler(hostingEnvironment));
        }
    }
}
