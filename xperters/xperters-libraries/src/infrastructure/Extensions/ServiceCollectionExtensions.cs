using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using xperters.azuread.Handlers;
using xperters.azuread.Interfaces;
using xperters.business;
using xperters.business.Interfaces;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.constants;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;
using xperters.fileutilities.Blob;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.http;
using xperters.http.Interfaces;
using xperters.infrastructure.Converters;
using xperters.infrastructure.Profiles;
using xperters.repositories;
using xperters.email;
using xperters.email.Interface;
using xperters.queues;
using System;
using xperters.fileio;
using Microsoft.AspNetCore.DataProtection;
using MappingProfile = xperters.infrastructure.Profiles.MappingProfile;

namespace xperters.infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencies(this IServiceCollection services, AppConfig config)
        {
            //Mapper Start Here
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profiles.MappingProfile(config, loggerFactory));
            });

            services.AddSingleton(conf);
            var mapper = conf.CreateMapper();
            services.AddSingleton(mapper);

            // value resolvers, member value resolvers, type converters to the container.
            services.AddAutoMapper(typeof(Startup), typeof(Profiles.MappingProfile));
            //Mapper End Here
            
            services.AddTransient(provider =>
            {
                return new Func<string, IQueueService>((queueName) => new QueueService(config, loggerFactory, queueName));
            });


            services.AddTransient<AzureServiceTokenProvider>();
            // All connection strings must be retrieved via the options object
            services.AddTransient<XpertersContext>();
            services.AddSingleton(config);
            services.AddTransient<IHttpHandler, HttpHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IHandleAdAuth, AdAuthHandler>();
            services.AddTransient<IJobManager, JobManager>();
            services.AddTransient<IBillingManager, BillingManager>();
            services.AddTransient<IManageEmails, EmailManager>();
            services.AddTransient<IRepository<Job>, JobsRepository>();
            services.AddTransient<IRepository<Skill>, SkillsRepository>();
            services.AddTransient<IRepository<Country>, CountriesRepository>();
            services.AddTransient<IRepository<Category>, CategoriesRepository>();
            services.AddTransient<IRepository<JobAttachment>, JobAttachmentsRepository>();
            services.AddTransient<IRepository<User>, AccountsRepository>();
            services.AddTransient<IBlobService, BlobService>();
            services.AddTransient<IAttachmentHandler<JobDto>, AttachmentHandler>();
            services.AddTransient<IHttpFileHandler, HttpFileHandler>();
            services.AddTransient<IHandleEnvironment, EnvironmentHandler>();
            services.AddTransient<ITypeConverter<JobAttachment, JobAttachmentDto>, JobAttachmentReader>();
            services.AddTransient<IRepository<JobBid>, JobBidsRepository>();
            services.AddTransient<IAttachmentHandler<JobBidDto>, JobBidAttachmentHandler>();
            services.AddTransient<IRepository<JobBidAttachment>, JobBidAttachmentRepository>();
            services.AddTransient<IRepository<JobBidChatSessionUser>, JobBidChatSessionUsersRepository>();
            services.AddTransient<IRepository<JobBidChatSession>, JobBidChatSessionsRepository>();
            services.AddTransient<IRepository<JobBidChatMessage>, JobBidChatMessagesRepository>();
            services.AddTransient<IRepository<ContractChatMessage>,ContractChatMessagesRepository>();
            services.AddTransient<IRepository<ContractChatSession>, ContractChatSessionsRepository>();
            services.AddTransient<IRepository<ContractChatSessionUser>, ContractChatSessionUsersRepository>();
            services.AddTransient<IRepository<JobContract>, JobContractRepository>();
            services.AddTransient<IRepository<Milestone>, MilestoneRepository>();
            services.AddTransient<IRepository<ContractMilestoneFund>, ContractMilestoneFundRepository>();
            services.AddTransient<IAttachmentHandler<MilestoneDto>, MilestoneAttachmentHandler>();
            services.AddTransient<IRepository<MilestoneAttachment>, MilestoneAttachmentRepository>();
            services.AddTransient<IRepository<Card>,CardRepository>();
            services.AddTransient<IRepository<EmailAudit>, EmailAuditRepository>();
            services.AddTransient<IRepository<EmailAttachments>, EmailAttachmentRepository>();
            services.AddTransient<IRepository<AccountDetail>, AccountDetailRepository>();
            services.AddTransient<IMilestoneManager, MilestoneManager>();
            services.AddTransient<IRepository<MilestoneMessage>, MilestoneMessageRepository>();
			services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<IRepository<MilestoneRequestPayer>, MilestoneRequestPayerRepository>();
            services.AddTransient<IRepository<UserBalance>, UserBalanceRepository>();
            services.AddTransient<IRepository<UserPayment>, UserPaymentRepository>();
            services.AddTransient<IRepository<MilestoneSystemRequestPayer>, MilestoneSystemRequestPayerRepository>();
            services.AddTransient<IRepository<UserWithdrawal>, UserWithdrawalsRepository>();
            // services.AddSingleton(Configuration);
        }

        public static void ConfigureDatabase(this IServiceCollection services, AppConfig config)
        {
            if (config.MockConnections)
            {
                var connection = new SqliteConnection(AppSettings.DatabaseConnectionStringInMemory);
                connection.Open();
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<XpertersContext>(options =>
                        options.UseSqlite(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
                         ServiceLifetime.Transient);
            }
            else
            {
                services
                    .AddEntityFrameworkSqlServer()
                    .AddDbContext<XpertersContext>(options =>
                        options.UseSqlServer(config.DatabaseConnectionString));
            }
        }

        public static AppConfig ConfigureAppConfig(this IServiceCollection services, IHandleFiles filesHandler, ILoggerFactory loggerFactory)
        {
            var builder = new AppConfigBuilder(services, filesHandler, loggerFactory);

            var config = builder.Build();
            return config;
        }


        public static void ConfigureProtection(this IServiceCollection services, AppConfig config)
        {
            services
                .AddDataProtection()
                .PersistKeysToAzureBlobStorage(config.Storage.ConnectionString, config.Storage.WebKeysContainer, config.Storage.WebKeysFile);
        }    
    }
}
