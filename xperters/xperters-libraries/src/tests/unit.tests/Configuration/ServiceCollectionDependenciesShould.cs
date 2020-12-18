using Moq;
using Xunit;
using System.IO;
using xperters.entities;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using xperters.extensions;
using xperters.configurations;
using xperters.entities.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using xperters.infrastructure.Extensions;

namespace xperters.unit.tests.Configuration
{
    public class ServiceCollectionDependenciesShould : BaseUnitTests
    {
        
        [Fact]
        public void BeConfigured()
        {

           var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object);
            
            var serviceProvider = services.BuildServiceProvider();
            var builder = new AppConfigBuilder(env.Object, services);
            var config = builder.Build();        

            Assert.NotNull(config);
            Assert.NotNull(config.Storage.ConnectionString);
            Assert.NotNull(config.WorkerSettings);
            Assert.Equal(5, config.WorkerSettings.NumberOfWorkers);
            Assert.Equal(200, config.WorkerSettings.ThreadSleepMilliseconds);

            Assert.True(config.MomoPaymentSettings.BaseUri.IsNotBlank());
            Assert.True(config.MomoPaymentSettings.Environment.IsNotBlank());
        }

        [Fact]
        public void ConfigureDatabase()
        {

           var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object);
            
            var serviceProvider = services.BuildServiceProvider();
            var builder = new AppConfigBuilder(env.Object, services);
            var config = builder.Build();        

            services.ConfigureDatabase(config);
        }   

        [Fact]
        public async Task UsingSqliteInMemoryProvider_Success()
        {
            await using (var connection = new SqliteConnection("Data Source=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<XpertersContext>()
                    .UseSqlite(connection) // Set the connection explicitly, so it won't be closed automatically by EF
                    .Options;

                // Create the database schema
                // You can use MigrateAsync if you use Migrations
                await using (var context = new XpertersContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                } // The connection is not closed, so the database still exists
            }
        }             
    }
}