using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ServiceStack;
using Xperters.Authentication;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.fileio;
using xperters.infrastructure.Extensions;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Xperters.Admin.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddAzureAd(options => Configuration.Bind("AzureAd", options))
                .AddCookie();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
                options.AllowedHosts = new List<string>(new[] { "admin-dev.xperters.com", "localhost", "admin-uat.xperters.com", "admin.xperters.com" });
                options.ForwardLimit = 2;
                options.KnownNetworks.Clear(); //its loopback by default
                options.KnownProxies.Clear();
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
            var loggerFactory = provider.GetService<ILoggerFactory>();

            var filesHandler = provider.GetService<IHandleFiles>();                

            AppConfig config = services.ConfigureAppConfig(filesHandler, loggerFactory);
            services.ConfigureProtection(config);

            services.AddHealthChecks()
                .AddApplicationInsightsPublisher()
                .AddSqlServer(name: "DB",
                    connectionString: config.DatabaseConnectionString,
                    tags: new[] { "DB", "SQL", "SQLSERVER" });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseForwardedHeaders();

            app.UseAuthentication();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseServiceStack(new AppHost { AppSettings = new NetCoreAppSettings(Configuration) });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health",
                    new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
            });

            app.Run(context =>
            {
                context.Response.Redirect("/api/metadata");
                return Task.CompletedTask;
            });


            app.Use(async (context, next) =>
            {
                if (context.Request.IsHttps || context.Request.Headers["X-Forwarded-Proto"] == Uri.UriSchemeHttps)
                {
                    await next();
                }
                else
                {
                    string queryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty;
                    var https = "https://" + context.Request.Host + context.Request.Path + queryString;
                    context.Response.Redirect(https);
                }
            });
        }
    }
}