using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.domain;
using xperters.encryption;
using xperters.entities;
using xperters.entities.Extensions;
using xperters.extensions;
using xperters.fileutilities.Interfaces;
using xperters.infrastructure.Extensions;
using xperters.mockdata;

namespace Xperters.DBMigrator
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    /// </summary>
    public class XperterContextFactory : IDesignTimeDbContextFactory<XpertersContext>
    {
        private readonly AppConfig _config;
        private ServiceProvider _serviceProvider;
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;


        public XperterContextFactory()
        {
            var handler = new EnvironmentHandler();
            var env = handler.GetHostingEnvironment<XperterContextFactory>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env);
            var appConfigBuilder = new AppConfigBuilder(env, serviceCollection);

            _config = appConfigBuilder.Build();

            ConfigureServices(serviceCollection);

            // Apply mock data
            ApplyMockData();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
                // .AddJsonFile($"appsettings.{env}.json", true, true)
                // .AddEnvironmentVariables();

            var config = builder.Build();
            _loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConfiguration(config.GetSection("Logging"))
                        .AddConsole();
            });

            _logger = _loggerFactory.CreateLogger<Program>();

            // Add Identity services to the services container.
            services.AddSingleton(_loggerFactory);
            services.ConfigureDatabase(_config);
            services.ConfigureDependencies(_config);
            _serviceProvider = services.BuildServiceProvider();

            _loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
            _logger = _loggerFactory.CreateLogger("Seeding data");

            _logger.LogDebug($"Connection string is set: {_config.DatabaseConnectionString.IsNotBlank()}");
            _logger.LogDebug($"Mock connections: {_config.MockConnections}");
            _logger.LogDebug($"Mock user data: {_config.MockUserData}");

            var context = _serviceProvider.GetService<XpertersContext>();
            _logger.LogDebug($"CreatedBy database context {context!=null}");
        }

        public XpertersContext CreateDbContext(string[] args)
        {
            return _serviceProvider.GetService<XpertersContext>();
        }

        private void ApplyMockData()
        {
            var mapper = _serviceProvider.GetService<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var context = _serviceProvider.GetService<XpertersContext>();
            var attachmentHandler = _serviceProvider.GetService<IAttachmentHandler<JobDto>>();
            var bidAttachmentHandler = _serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
            var milestoneAttachmentHandler = _serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();

            context.Database.GetDbConnection();
            
            context.Database.Migrate();
            _logger.LogInformation("Succeeded applying database migrations");

            EncryptionBuilder.InitializeAzureKeyVaultProvider(_loggerFactory, _config.AzureAdAppRegSettings.ClientIdSql, _config.AzureAdAppRegSettings.ClientSecretSql);
            _logger.LogDebug("Initialized key vault provider");

            var dataBuilder = new DataBuilder(context, mapper, _logger);

            if (_config.MockUserData)
            {
                dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
                _logger.LogInformation("Applying mock data to database");
            }
            else
            {
                _logger.LogInformation("Not applying mock data to database");
            }
        }
    }
}
