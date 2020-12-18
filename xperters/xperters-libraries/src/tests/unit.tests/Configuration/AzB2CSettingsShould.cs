using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using xperters.configurations.Settings.Ad;
using xperters.constants;
using Xunit;

namespace xperters.unit.tests.Configuration
{
    public class AzB2CSettingsShould
    {
        private readonly IConfigurationRoot _configuration;
        private const string ClientId = "dfd32512-23a6-4f85-bdb3-59373941dd83";
        private const string Domain = "xpertersdev2.b2clogin.com";
        private const string SignUpSignInPolicyId = "b2c_1_susi";
        private const string ResetPasswordPolicyId = "b2c_1_reset";
        private const string DefaultPolicy = SignUpSignInPolicyId;
        private const string Tenant = "xpertersdev2.onmicrosoft.com";
        private readonly string _azureAdB2CInstance;
        private readonly string _authority;

        public AzB2CSettingsShould()
        {

            _azureAdB2CInstance = $"https://{Domain}";
            _authority = $"{_azureAdB2CInstance}/{Tenant}/{SignUpSignInPolicyId}/v2.0";

            var input = new Dictionary<string, string>
            {
                {"Authentication:AzureAdB2C:Domain", Domain},
                {"Authentication:AzureAdB2C:ClientId", ClientId},
                {"Authentication:AzureAdB2C:Tenant", Tenant},
                {"Authentication:AzureAdB2C:SignUpSignInPolicyId", SignUpSignInPolicyId},
                {"Authentication:AzureAdB2C:ResetPasswordPolicyId", ResetPasswordPolicyId},
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
        public void ReturnFullAzureB2COptions()
        {
            var options = new AzureAdB2CSettings();
            _configuration.Bind(AppSettings.AzureB2CSettings, options);

            Assert.Equal(ClientId, options.ClientId);
            Assert.Equal(Domain, options.Domain);
            Assert.Equal(_azureAdB2CInstance, options.AzureAdB2CInstance);
            Assert.Equal(Tenant, options.Tenant);
            Assert.Equal(_authority, options.Authority);
            Assert.Equal(SignUpSignInPolicyId, options.SignUpSignInPolicyId);
            Assert.Equal(DefaultPolicy, options.DefaultPolicy);
            Assert.Equal(ResetPasswordPolicyId, options.ResetPasswordPolicyId);
        }
    }
}
