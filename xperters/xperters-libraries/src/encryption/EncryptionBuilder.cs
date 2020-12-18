using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace xperters.encryption
{
    public static class EncryptionBuilder
    {
        private static string _clientId;
        private static string _clientSecret;
        private static bool _sqlProviderIsRegistered;
        private static ILogger _logger;
        public static SqlColumnEncryptionAzureKeyVaultProvider AzureKeyVaultProvider { get; private set; }

        public static void InitializeAzureKeyVaultProvider(ILoggerFactory loggerFactory, string clientId,
            string clientSecret)
        {
            if (_sqlProviderIsRegistered)
            {
                return;
            }

            _logger = loggerFactory.CreateLogger("EncryptionBuilder");
            _clientId = clientId;
            _clientSecret = clientSecret;

            AzureKeyVaultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(GetToken);

            var providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>
            {
                {SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, AzureKeyVaultProvider}
            };

            SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);

            _sqlProviderIsRegistered = true;
        }

        private static async Task<string> GetToken(string authority, string resource, string scope)
        {
            try
            {
                var appCredentials = new ClientCredential(_clientId, _clientSecret);
                var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

                var result = await context.AcquireTokenAsync(resource, appCredentials);

                _logger.LogDebug($"Sql auth encryption token acquired for tenant: {result.TenantId}");
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }
        }

    }
}
