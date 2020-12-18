using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace xperters.unit.tests.Configuration
{
    public class AzureAdKeyVaultSettingsShould
    {
        private readonly IConfigurationRoot _configuration;
        private const string ClientId = "298834bc-9038-4aad-959b-a25dcc113285";
        private const string ClientSecret = "dXDTFadt5mDd7ivpV277eLg/sGJCOGDJRPWiBWItbLQ=";
        private const string KeyVaultName = "xperters-dev";
        private const string KeyVaultSecretName = "AzureAdB2C-RefreshToken";

        public AzureAdKeyVaultSettingsShould()
        {

            var input = new Dictionary<string, string>
            {
                {"Authentication:AzureAdKeyVault:ClientId", ClientId},
                {"Authentication:AzureAdKeyVault:ClientSecret", ClientSecret},
                {"Authentication:AzureAdKeyVault:KeyVaultName", KeyVaultName},
                {"Authentication:AzureAdKeyVault:KeyVaultSecretName", KeyVaultSecretName}
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

    }
}
