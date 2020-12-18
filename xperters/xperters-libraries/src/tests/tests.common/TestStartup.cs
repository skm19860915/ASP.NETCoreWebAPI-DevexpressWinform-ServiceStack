using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using xperters.domain;
using xperters.entities;
using xperters.fileutilities.Interfaces;
using xperters.infrastructure;
using xperters.configurations;
using xperters.mockdata;
namespace xperters.Tests.Common
{
    public class TestStartup : Startup
    {
        private readonly bool _dbError;

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public TestStartup(IConfiguration configuration, bool dbError) : base(configuration)
        {
            _dbError = dbError;
            Configuration = configuration;
        }

        public override JwtConfig GetSettings(IServiceProvider serviceProvider)
        {
            var jwtConfig = new JwtConfig
            {
                SigningKey = "jlksadjflk!!.sadf9u3298rukjsaf@~",
                Audience = "xperters.com",
                Issuer = "xperters.com"
            };

            return jwtConfig;
        }

        protected override void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                });
        }

        /// <summary>
        /// Ensures that tests run in memory
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override AppConfig ConfigureAppConfig(IServiceCollection services)
        {
            var appConfigBuilder1 = new AppConfigBuilder(services, FilesHandler, LoggerFactory);
            var config = appConfigBuilder1.Build();

            config.MockUserData = true;
            config.MockConnections = true;
            return config;
        }
       

        protected override void ApplyData(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory, IMapper mapper)
        {

            using (var serviceScope = scopeFactory.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                var context = serviceProvider.GetService<XpertersContext>();

                // Create the schema in the database
                context.Database.EnsureCreated();

                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.ChangeTracker.AutoDetectChangesEnabled = false;

                // seed database
                var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
                var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
                var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();

                var logger = loggerFactory.CreateLogger("Seeding data");

                var dataBuilder = new DataBuilder(context, mapper, logger);
                dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
            }
        }
    }
}

