using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.configurations;
using xperters.entities.Extensions;
using xperters.queues;

namespace xperters.unit.tests.Queues
{
    public class QueueServiceShould{
        private readonly AppConfig _config;
        private readonly LoggerFactory _loggerFactory;

        public QueueServiceShould()
        {
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object);
            
            var builder = new AppConfigBuilder(env.Object, services);
            _config = builder.Build();

            _loggerFactory = new LoggerFactory();  
            
            services.AddTransient(provider =>
            {
                return new Func<string,  IQueueService>((queueName) =>
                {
                    return new QueueService(_config, _loggerFactory, queueName);
                });
            });                
        }
    }
}