using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Funq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using ServiceStack.Text;
using ServiceStack.Validation;
using ServiceStack.Web;
using Xperters.Admin.Api.Mapping;
using Xperters.Admin.ServiceInterface;
using Xperters.Admin.ServiceInterface.Services;
using Xperters.Authentication;
using Xperters.Authentication.Native;
using xperters.business;
using xperters.business.Interfaces;
using xperters.correlationid;
using xperters.entities;
using xperters.entities.Entities;
using xperters.repositories;
using NetCoreIdentityAuthProvider = Xperters.Authentication.NetCoreIdentityAuthProvider;
using xperters.configurations;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.email;
using xperters.email.Interface;
using xperters.domain;
using xperters.fileutilities.Blob;
using xperters.http.Interfaces;
using xperters.http;
using xperters.azuread.Interfaces;
using xperters.azuread.Handlers;

namespace Xperters.Admin.Api
{
	public static class AppHostConfigurator
	{
        private static AppConfig _config;		
		/// <summary>
		///     List of Application level exceptions to ignore in the default exception handler logging
		///     i.e. any exceptions you already handle in the application layer
		/// </summary>
		private static readonly List<Type> ApplicationLevelExceptions = new List<Type>
		{
			typeof(ValidationError)

			//Add your application level exceptions here
		};

		public static void Configure(ServiceStackHost host, Container container, AppConfig config)
		{
            _config = config;			
			LogManager.LogFactory = new SerilogFactory();
			
			var log = LogManager.LogFactory.GetLogger(typeof(AppHostConfigurator));
			
			log.Info($"Starting up {host.ServiceName}");

			var settings = host.AppSettings.Get<ServiceSettings>("ServiceSettings");
			var azureSettings = host.AppSettings.Get<AzureAdOptions>("AzureAd");
			container.Register(s => settings);
            container.AddSingleton(config);

			// Read http://docs.servicestack.net/caching
			container.Register<ICacheClient>(new MemoryCacheClient());

			// Add a login link on the metadata page
			host.GetPlugin<MetadataFeature>().AddPluginLink("../account/signin", "Sign In");
			host.GetPlugin<MetadataFeature>().AddPluginLink("../account/signout", "Sign Out");

			RegisterPlugins(host);

			ConfigureJson();
			ConfigureExceptionHandlers(host);
			ConfigureDbConnections(container);
			ConfigureRepositories(container);
            ConfigureServices(container);
            ConfigureOtherDependencies(container);
			ConfigureValidation(host, container);
			ConfigureScopedUser(host, container);
			ConfigureAuthentication(host);

			// Read http://docs.servicestack.net/configuration-options
			host.SetConfig(new HostConfig
			{
				ApiVersion = "v1",
				HandlerFactoryPath = "api",
				WsdlServiceNamespace = "http://schemas.xperters.com/",
				DebugMode = host.AppSettings.Get(nameof(HostConfig.DebugMode), false)
			});
		}

		private static void RegisterPlugins(ServiceStackHost host)
		{
			host.Plugins.Add(new AutoQueryFeature { MaxLimit = 10000 });
			host.Plugins.Add(new PostmanFeature());
			host.Plugins.Add(new OpenApiFeature());
			host.Plugins.Add(new RequestLogsFeature());			
			host.Plugins.Add(new CorrelationIdDecorator());
			host.Register<IRequestLogger>(new LogRequestLogger());
		}

		private static void ConfigureJson()
		{
			JsConfig.DateHandler = DateHandler.ISO8601;
			JsConfig.AlwaysUseUtc = true;
			JsConfig.AssumeUtc = true;
			JsConfig<Guid>.SerializeFn = guid => guid.ToString();
		}

		/// <summary>
		///     register your databases connection factories
		///     Dapper: Read https://stackoverflow.com/questions/15487000/how-to-use-dapper-in-servicestack/16782435
		/// </summary>
		private static void ConfigureDbConnections(Container container)
		{
			var connectionString = _config.DatabaseConnectionString;

            container.AddScoped(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<XpertersContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new XpertersContext(optionsBuilder.Options);
            });
        }

		private static void ConfigureScopedUser(IAppHost appHost, Container container)
		{
			appHost.GlobalRequestFilters.Add((req, res, responseDto) =>
			{
				var defaultHttpRequest = req.OriginalRequest as DefaultHttpContext;

				if (defaultHttpRequest?.HttpContext?.User == null)
					return;

				var authUserSession = req.SessionAs<AuthUserSession>();

			});
        }

		/// <summary>
		///     Read http://docs.servicestack.net/validation
		/// </summary>
		private static void ConfigureValidation(IAppHost host, Container container)
		{
			host.Plugins.Add(new ValidationFeature
			{
				// Change that to true to scan Host DLL for validators
				ScanAppHostAssemblies = false
			});
        }

		/// <summary>
		///     Read https://github.com/ServiceStack/ServiceStack/wiki/Authentication-and-authorization
		/// </summary>
		private static void ConfigureAuthentication(IAppHost host)
		{
			var authProviders = new List<IAuthProvider> {new NetCoreIdentityAuthProvider(host.AppSettings)};

			if (host.AppSettings.GetAllKeys().Contains("AzureAd"))
			{
				var debugMode = host.AppSettings.Get(nameof(HostConfig.DebugMode), false);
				var azureSettings = host.AppSettings.Get<AzureAdOptions>("AzureAd");
				var jwt = azureSettings.GetJWTProviderReader(debugMode);

				jwt.PopulateSessionFilter = (session, payload, request) =>
				{
					if (session.Email == null && payload.ContainsKey("upn") && payload["upn"].Contains("@"))
						session.Email = payload["upn"];
					if (session.UserName == null && payload.ContainsKey("unique_name"))
						session.UserName = payload["unique_name"];
				};

				authProviders.Add(jwt);
			}

			var auth = new AuthFeature(() => new AuthUserSession(), authProviders.ToArray())
			{
				HtmlRedirect = "/account/signin",
				HtmlLogoutRedirect = "/account/signout",
				IncludeAssignRoleServices = false,
				IncludeRegistrationService = false
			};

			// remove default service authentication services
			auth.ServiceRoutes.Remove(typeof(AuthenticateService));

			host.Plugins.Add(auth);
		}

		private static void ConfigureExceptionHandlers(IAppHost host)
		{
			//Handle Exceptions occurring in Services:
			host.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
			{
				if (!ApplicationLevelExceptions.Contains(exception.GetType()))
				{
					var log = LogManager.GetLogger(typeof(AppHostConfigurator));
					log.Error(exception, "Exception handling request {httpReq} with request dto {request}",
						new {httpReq, request});
				}

				//continue with default Error Handling
				return null;
			});

			//Handle Unhandled Exceptions occurring outside of Services
			host.UncaughtExceptionHandlers.Add((req, res, operationName, ex) =>
			{
				if (ApplicationLevelExceptions.Contains(ex.GetType())) return;

				var log = LogManager.GetLogger(typeof(AppHostConfigurator));
				log.Error(ex, "Exception handling request {req} with response {res} for operation {operationName}.",
					new {req, res, operationName});
			});
		}

		private static void ConfigureRepositories(Container container)
		{
			var repoTypes = Assembly.GetAssembly(typeof(IRepository<Milestone>))
						.ExportedTypes
						.Where(t => t.IsClass)
						.Where(t => !t.IsAbstract)
						.Where(t => t.Namespace == typeof(IRepository<Milestone>).Namespace);
			
			foreach (var repoType in repoTypes)
			{
				var firstInterface = repoType.GetInterfaces().FirstOrDefault();
				if (firstInterface != null)
					container.AddTransient(firstInterface, repoType);
			}
		}
        
        private static void ConfigureOtherDependencies(Container container)
		{
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            container.AddSingleton(conf);
            var mapper = conf.CreateMapper();
            container.AddSingleton(mapper);

            container.AddTransient<IRepository<Milestone>, MilestoneRepository>();
            container.AddTransient<IRepository<MilestoneRequestPayer>, MilestoneRequestPayerRepository>();
            container.AddTransient<IRepository<MilestoneSystemRequestPayer>, MilestoneSystemRequestPayerRepository>();
            container.AddTransient<IRepository<MilestoneMessage>, MilestoneMessageRepository>();
            container.AddTransient<IRepository<User>, AccountsRepository>();
            container.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            container.AddTransient<IMilestoneManager, MilestoneManager>();
            container.AddTransient<IManageEmails, EmailManager>();
            container.AddTransient<IHttpFileHandler, HttpFileHandler>();
            container.AddTransient<IAttachmentHandler<JobDto>, AttachmentHandler>();
            container.AddTransient<IAttachmentHandler<JobBidDto>, JobBidAttachmentHandler>();
            container.AddTransient<IAttachmentHandler<MilestoneDto>, MilestoneAttachmentHandler>();
			container.AddTransient<IBlobService, BlobService>();
			container.AddTransient<IJobManager, JobManager>();


			container.AddTransient<IHttpHandler, HttpHandler>();
			container.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
			container.AddTransient<IAccountManager, AccountManager>();
			container.AddTransient<IHandleAdAuth, AdAuthHandler>();
			container.AddTransient<IJobAdminManager, JobAdminManager>();
			container.AddTransient<IWithdrawalsManager, WithdrawalsManager>();
			container.AddTransient<IPaymentsIncomingManager, PaymentsIncomingManager>();
			container.AddTransient<IUserManager, UserManager>();
		}

        private static void ConfigureServices(Container container)
		{
			var services = Assembly.GetAssembly(typeof(MilestonesService))
						.ExportedTypes
						.Where(t => t.IsClass)
						.Where(t => !t.IsAbstract)
						.Where(t => t.Namespace.Contains("Xperters.Admin.ServiceInterface.Services"));
			
			foreach (var service in services)
			{
				container.AddTransient(service);
			}
		}
	}
}