using System;
using AutoMapper;
using Funq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceStack;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.fileio;
using xperters.queues;
using Xperters.Admin.Api.Mapping;
using Xperters.Admin.ServiceInterface.Services;
using Xperters.Admin.ServiceModel.Extensions;

namespace Xperters.Admin.Api
{
	public class AppHost : AppHostBase
	{

		private ILoggerFactory _loggerFactory;
        private AppConfig _config;

        /// <summary>
		///     Base constructor requires a Name and Assembly where web service implementation is located
		/// </summary>
		public AppHost()
			: base("Xperters.Admin", typeof(MilestonesService).Assembly)
		{

        }

		/// <summary>
		///     Application specific configuration
		///     This method should initialize any IoC resources utilized by your web service classes.
		/// </summary>
		public override void Configure(Container container)
		{
			container.AddTransient<IHandleFiles, FilesHandler>();
            container.AddTransient<IHandleEnvironment, EnvironmentHandler>();

            _loggerFactory = container.GetService<ILoggerFactory>();

            var services = new ServiceCollection();
            services.AddTransient<IHandleFiles, FilesHandler>();
            services.AddTransient<IHandleEnvironment, EnvironmentHandler>();
            services.AddTransient<IHostEnvironment, FakeWebHost>();
            var provider = services.BuildServiceProvider();
            var filesHandler = provider.GetService<IHandleFiles>();

            var builder = new AppConfigBuilder(services, filesHandler, _loggerFactory);
            _config = builder.Build();

            var conf = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });

            container.AddSingleton(conf);
            var mapper = conf.CreateMapper();
            container.AddSingleton(mapper);

            container.AddTransient(queue =>
            {
                return new Func<string, IQueueService>(queueName => new QueueService(_config, _loggerFactory, queueName));
            });

			AppHostConfigurator.Configure(this, container, _config);			
		}
	}
}