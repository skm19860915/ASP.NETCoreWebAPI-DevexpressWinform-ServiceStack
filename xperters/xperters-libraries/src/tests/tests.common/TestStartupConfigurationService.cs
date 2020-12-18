using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using xperters.tests.common.Interfaces;

namespace xperters.tests.common
{
    public class TestStartupConfigurationService<TDbContext>
        : IStartupConfigurationService
        where TDbContext : DbContext
    {
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            SetupStore(app);
        }

        public virtual void ConfigureEnvironment(IHostEnvironment env)
        {
            env.EnvironmentName = "Test";
        }

        public virtual void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            ConfigureStore(services);
        }

        protected virtual void SetupStore(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<TDbContext>();

                dbContext.Database.OpenConnection();
                dbContext.Database.EnsureCreated();
            }
        }

        protected virtual void ConfigureStore(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            services.AddDbContext<TDbContext>(options => options.UseSqlite(connection));
        }
    }
}