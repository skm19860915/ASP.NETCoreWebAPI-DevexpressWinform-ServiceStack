using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using xperters.configurations.Settings.Ad;
using xperters.constants;
using Xunit;

namespace xperters.unit.tests.Configuration
{
    public class AzureAdAppRegSettingsShould
    {
        private readonly IConfigurationRoot _configuration;
        private const string ClientId = "c07c6208-5ba4-455c-ae77-28e0192d3c8c";
        private const string ClientSecret = "M6s+FlPnzoAXX7CRj4VMaW6qKn21uFqwujWC/a2J5Kc=";
        private const string Tenant = "xpertersdev2.onmicrosoft.com";
        private const string TenantId = "6579523f-b7ca-41ea-a3fa-c67e1d389baf";
        private const string MsGraphUrl = "https://graph.microsoft.com/v1.0/users";
        private const string MsGraphScope= "https://graph.microsoft.com/.default";
        private const string ClientIdSql = "c07c6208-5ba4-455c-ae77-28e0192d3c8d";
        private const string ClientSecretSql = "N7s+FlPnzoAXX7CRj4VMaW6qKn21uFqwujWC/a2J5Kc=";
        private const string TenantIdSql = "6579523f-b7ca-41ea-a3fa-c67e1d389bab";

        public AzureAdAppRegSettingsShould()
        {

            var input = new Dictionary<string, string>
            {
                {"Authentication:AzureAdAppReg:ClientId", ClientId},
                {"Authentication:AzureAdAppReg:ClientSecret", ClientSecret},
                {"Authentication:AzureAdAppReg:TenantId", TenantId},
                {"Authentication:AzureAdAppReg:ClientIdSql", ClientIdSql},
                {"Authentication:AzureAdAppReg:ClientSecretSql", ClientSecretSql},
                {"Authentication:AzureAdAppReg:TenantIdSql", TenantIdSql},
                {"Authentication:AzureAdAppReg:Tenant", Tenant},
                {"Authentication:AzureAdAppReg:MsGraphUrl", MsGraphUrl},
                {"Authentication:AzureAdAppReg:MsGraphScope", MsGraphScope}
            };

            // copy existing config into memory
            var existingConfig = new MemoryConfigurationSource
            {
                InitialData = input
            };

            // create new configuration from existing config
            // and override whatever needed
            var testConfigBuilder = new ConfigurationBuilder()
                .Add(existingConfig);

            _configuration = testConfigBuilder.Build();
        }

        [Fact]
        public void ReturnAzureAdAppRegOptions()
        {
            var options = new AzureAdAppRegSettings();
            _configuration.Bind(AppSettings.AzureAdAppRegSettings, options);

            Assert.Equal(ClientId, options.ClientId);
            Assert.Equal(ClientSecret, options.ClientSecret);
            Assert.Equal(TenantId, options.TenantId);
            Assert.Equal(MsGraphUrl, options.MsGraphUrl);
            Assert.Equal(MsGraphScope, options.MsGraphScope);
        }

        [Fact]
        public void ReturnAzureAdAppRegOptionsSql()
        {
            var options = new AzureAdAppRegSettings();
            _configuration.Bind(AppSettings.AzureAdAppRegSettings, options);

            Assert.Equal(ClientIdSql, options.ClientIdSql);
            Assert.Equal(ClientSecretSql, options.ClientSecretSql);
            Assert.Equal(TenantIdSql, options.TenantIdSql);
        }
    }
}
