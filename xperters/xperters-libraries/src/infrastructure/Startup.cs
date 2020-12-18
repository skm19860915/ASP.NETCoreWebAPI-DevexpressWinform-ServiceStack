using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Enrichers.AspNetCore.HttpContext;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.constants;
using xperters.domain;
using xperters.encryption;
using xperters.entities;
using xperters.extensions;
using xperters.fileio;
using xperters.fileutilities.Interfaces;
using xperters.infrastructure.AzureB2C;
using xperters.infrastructure.Exceptions;
using xperters.infrastructure.Extensions;
using xperters.mockdata;
using xperters.infrastructure.Logging;
using xperters.requestheaders;
using xperters.requestheaders.Headers;
using Microsoft.Net.Http.Headers;
using xperters.infrastructure.AppInsights;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace xperters.infrastructure
{
    public class Startup : IStartup
    {
        protected ILoggerFactory LoggerFactory;
        protected IHandleFiles FilesHandler;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; set; }

        public virtual JwtConfig GetSettings(IServiceProvider serviceProvider)
        {
            var jwtOptions = serviceProvider.GetService<IOptions<JwtConfig>>();

            var jwtConfig = jwtOptions.Value;
            return jwtConfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Application services configuration
        /// The order of statements in this method matters.  Be careful before changes the order
        /// After making changes, run all tests to ensure nothing is broken.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// 

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
            });

            services.AddLogging(builder =>
                builder.AddSerilog(dispose: true));
            services.AddHttpClient();
            /// TODO this dependency is required here before appConfig is created.
            /// Do not remove unless it has been refactored out and tested.
            services.AddTransient<IHandleFiles, FilesHandler>();
            services.AddTransient<IHandleEnvironment, EnvironmentHandler>();
            services.AddHttpContextAccessor();

            var provider = services.BuildServiceProvider();
            LoggerFactory = provider.GetService<ILoggerFactory>();
            var webEnv = provider.GetService<IHostEnvironment>();
            FilesHandler = provider.GetService<IHandleFiles>();


            // static instance that can be used from static/extension classes
            ApplicationLogging.LoggerFactory = LoggerFactory;

            ConfigureAuthentication(services);
            services.AddAuthorization();
            var config = services.ConfigureAppConfig(FilesHandler, LoggerFactory);

            //DI Start Here
            services.ConfigureDependencies(config);
            //DI End Here

            services.ConfigureProtection(config);

            services.ConfigureDatabase(config);
            services.AddRazorPages();
            services.AddHealthChecks();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(config.AzureAdB2CSettings.AzureAdB2CInstance);
                    });
            });

            services.AddApplicationInsightsTelemetry();
            services.AddApplicationInsightsTelemetryProcessor<SuppressHealthStaticAndBotsResourcesFilter>();
            services.AddControllersWithViews();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
                options.AllowedHosts = new List<string>(new[] { "www-dev.xperters.com", "localhost", "www-uat.xperters.com", "www.xperters.com", "vpd-ossentoog01" });
                options.KnownNetworks.Clear(); //its loopback by default
                options.KnownProxies.Clear();
            });


            return services.BuildServiceProvider();
        }

        protected virtual AppConfig ConfigureAppConfig(IServiceCollection services)
        {
            return services.ConfigureAppConfig(FilesHandler, LoggerFactory);
        }

        protected virtual void ConfigureAuthentication(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            })
            .AddAzureAdB2C(options => Configuration.Bind(AppSettings.AzureB2CSettings, options))
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;

            });
        }

        /// <summary>
        /// TODO Note: please don't add call to ef migration in code for the website.  Migration will be handled separately
        /// TODO during deployment deployment.  Thus the website process will not have sql permissions required to run ef migrations.
        /// </summary>
        public void Configure(IApplicationBuilder app, AzureServiceTokenProvider azureServiceTokenProvider)
        {

            var env = app.ApplicationServices.GetService<IHostEnvironment>();
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            var mapper = app.ApplicationServices.GetService<IMapper>();
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();

            var config = app.ApplicationServices.GetService<AppConfig>();

            EncryptionBuilder.InitializeAzureKeyVaultProvider(loggerFactory, config.AzureAdAppRegSettings.ClientIdSql, config.AzureAdAppRegSettings.ClientSecretSql);

            try
            {
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch (AggregateException exceptions)
            {
                foreach (var exception in exceptions.InnerExceptions)
                {
                    Debug.WriteLine(exception.Message);
                }

                throw;
            }

            if (config.DatabaseConnectionString.IsBlank() || mapper == null)
            {
                throw new Exception("Config settings or mapping profile not set");
            }

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        var exception = errorFeature.Error;

                        // the IsTrusted() extension method doesn't exist and
                        // you should implement your own as you may want to interpret it differently
                        // i.e. based on the current principal

                        var problemDetails = new ProblemDetails
                        {
                            Instance = $"urn:xperters:error:{Guid.NewGuid()}"
                        };

                        if (exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails.Title = MessageConstants.ErrorMessageInvalidRequest;
                            problemDetails.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode",
                                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(badHttpRequestException);
                            problemDetails.Detail = badHttpRequestException.Message;
                        }
                        else
                        {
                            problemDetails.Title = MessageConstants.ErrorMessageUnexpected;
                            problemDetails.Status = 500;
                            problemDetails.Detail = exception.Demystify().ToString();
                        }

                        // log the exception etc..

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.WriteJson(problemDetails.Title);
                    });
                });

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSecurityHeaders(policies => policies
                .AddXForwardedHost(config.WebsiteAddress)
            );
            app.UseMiddleware<SerilogMiddleware>();
            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context => context.Context.Response.GetTypedHeaders()
                    .CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(30) // approx 1 month
                    }
            });

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseSerilogLogContext(options =>
            {
                options.EnrichersForContextFactory = context => new[]
                {
                    // TraceIdentifier property will be available in all chained middlewares. And yes - it is HttpContext specific
                    new PropertyEnricher("TraceIdentifier", context.TraceIdentifier)
                };
            });

            app.UseSession();

            app.UseForwardedHeaders();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.LogAuthenticationRequests(LoggerFactory);

            ApplyData(scopeFactory, LoggerFactory, mapper);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });

            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
        }

        protected virtual void ApplyData(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory, IMapper mapper)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
                var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
                var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();

                var logger = loggerFactory.CreateLogger("Data");

                bool.TryParse(Configuration[AppSettings.MockUserData], out var mockUsers);
                bool.TryParse(Configuration[AppSettings.MockConnections], out var mockConnections);

                var context = serviceProvider.GetService<XpertersContext>();
                if (mockConnections)
                {
                    logger.LogDebug("Running migrations");

                    context.Database.EnsureCreated();
                    logger.LogDebug("Ran migrations");
                }

                if (mockUsers)
                {
                    logger.LogDebug("Seeding data");
                    var dataBuilder = new DataBuilder(context, mapper, logger);
                    dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
                    logger.LogDebug("Seeded migrations");
                }
            }
        }
    }
}