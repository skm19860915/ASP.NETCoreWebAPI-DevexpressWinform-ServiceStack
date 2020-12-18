using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.encryption;
using xperters.entities.Extensions;
using xperters.fileio;

namespace xperters.entities
{
    public class MigrationSqlBuilder 
    {
        private readonly string _keyVaultName;
        private readonly string _keyVaultKeyName;
        private readonly string _keyVaultKeyVersion;
        private readonly SqlColumnEncryptionAzureKeyVaultProvider _provider;
        private readonly string _connectionString;
        private readonly string _applicationName;
        public ILoggerFactory LoggerFactory { get; }
        public AppConfig Config { get; }

        private const string CreateColumnEncryptionKeyTemplate = @" 
            CREATE COLUMN ENCRYPTION KEY [{0}] 
            WITH VALUES 
            ( 
                COLUMN_MASTER_KEY = [{1}], 
                ALGORITHM = 'RSA_OAEP', 
                ENCRYPTED_VALUE = {2} 
            );";

        public MigrationSqlBuilder(IHostEnvironment env)
        {
            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env);

            var fileHandler = new FilesHandler(env);
            var provider = services.BuildServiceProvider();
            LoggerFactory = provider.GetService<ILoggerFactory>();
            var appConfigBuilder = new AppConfigBuilder(services, fileHandler, LoggerFactory);

            Config = appConfigBuilder.Build();

            _applicationName = "xperters";

            _keyVaultName = Config.KeyVaultSettings.Name;
            _keyVaultKeyName = Config.KeyVaultSettings.KeyName;
            _keyVaultKeyVersion = Config.KeyVaultSettings.KeyVersion;

            _connectionString = Config.DatabaseConnectionString;
            EncryptionBuilder.InitializeAzureKeyVaultProvider(LoggerFactory, Config.AzureAdAppRegSettings.ClientIdSql, Config.AzureAdAppRegSettings.ClientSecretSql);
            _provider = EncryptionBuilder.AzureKeyVaultProvider;

        }

        public string BankAccountsEncryptionDrop()
        {
            return @"DELETE FROM AccountDetails;
                    ALTER TABLE AccountDetails
	                    DROP 
		                    COLUMN IF EXISTS AccountHolderName, 
		                    COLUMN IF EXISTS BankAccountNumber, 
		                    COLUMN IF EXISTS BankName, 
		                    COLUMN IF EXISTS BranchName, 
		                    COLUMN IF EXISTS IfscCode, 
		                    COLUMN IF EXISTS SwiftNumber, 
		                    COLUMN IF EXISTS BankAddress;

                    ALTER TABLE AccountDetails ADD AccountHolderName varchar(100) NOT NULL
                        ALTER TABLE AccountDetails ADD BankAccountNumber varchar(100) NOT NULL
                        ALTER TABLE AccountDetails ADD BankName varchar(100) NOT NULL
                        ALTER TABLE AccountDetails ADD BranchName varchar(100) NOT NULL
                        ALTER TABLE AccountDetails ADD IfscCode varchar(50) NOT NULL
                        ALTER TABLE AccountDetails ADD SwiftNumber varchar(100) NOT NULL
                        ALTER TABLE AccountDetails ADD BankAddress varchar(100) NOT NULL";
        }

        public string BankAccountsEncryptionAdd()
        {
            return @"DELETE FROM AccountDetails;
                    ALTER TABLE AccountDetails
	                    DROP 
		                    COLUMN IF EXISTS AccountHolderName, 
		                    COLUMN IF EXISTS BankAccountNumber, 
		                    COLUMN IF EXISTS BankName, 
		                    COLUMN IF EXISTS BranchName, 
		                    COLUMN IF EXISTS IfscCode, 
		                    COLUMN IF EXISTS SwiftNumber, 
		                    COLUMN IF EXISTS BankAddress;

                        ALTER TABLE AccountDetails ADD AccountHolderName varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD BankAccountNumber varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD BankName varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD BranchName varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD IfscCode varchar(50) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD SwiftNumber varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                        ALTER TABLE AccountDetails ADD BankAddress varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL";
        }

        public string CardsEncryptionAdd()
        {
            return @" DELETE FROM Cards;

                    ALTER TABLE Cards
	                DROP 
		                COLUMN IF EXISTS Country, 
		                COLUMN IF EXISTS CardScheme, 
		                COLUMN IF EXISTS CardType, 
		                COLUMN IF EXISTS ExpMonth, 
		                COLUMN IF EXISTS ExpYear, 
		                COLUMN IF EXISTS Number, 
		                COLUMN IF EXISTS AddressCity, 
		                COLUMN IF EXISTS AddressCountry, 
		                COLUMN IF EXISTS AddressLine1, 
		                COLUMN IF EXISTS AddressLine2, 
		                COLUMN IF EXISTS AddressState, 
		                COLUMN IF EXISTS AddressZip, 
		                COLUMN IF EXISTS Currency, 
		                COLUMN IF EXISTS Name; 

                    ALTER TABLE Cards ADD Country varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD CardScheme int ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD CardType int ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD ExpMonth int ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD ExpYear int ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD Number varchar(30) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressCity varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressCountry varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressLine1 varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressLine2 varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressState varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD AddressZip varchar(20) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD Currency varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Cards ADD Name varchar(100) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL";
        }
        public string CardsEncryptionDrop()
        {
            return @"DELETE FROM Cards;                    
                    ALTER TABLE Cards
	                DROP 
		                COLUMN IF EXISTS Country, 
		                COLUMN IF EXISTS CardScheme, 
		                COLUMN IF EXISTS CardType, 
		                COLUMN IF EXISTS ExpMonth, 
		                COLUMN IF EXISTS ExpYear, 
		                COLUMN IF EXISTS Number, 
		                COLUMN IF EXISTS AddressCity, 
		                COLUMN IF EXISTS AddressCountry, 
		                COLUMN IF EXISTS AddressLine1, 
		                COLUMN IF EXISTS AddressLine2, 
		                COLUMN IF EXISTS AddressState, 
		                COLUMN IF EXISTS AddressZip, 
		                COLUMN IF EXISTS Currency, 
		                COLUMN IF EXISTS Name; 

                    ALTER TABLE Cards ADD Country varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD CardScheme int NOT NULL;
                    ALTER TABLE Cards ADD CardType int NOT NULL;
                    ALTER TABLE Cards ADD ExpMonth int NOT NULL;
                    ALTER TABLE Cards ADD ExpYear int NOT NULL;
                    ALTER TABLE Cards ADD Number varchar(30) NOT NULL;
                    ALTER TABLE Cards ADD AddressCity varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD AddressCountry varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD AddressLine1 varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD AddressLine2 varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD AddressState varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD AddressZip varchar(20) NOT NULL;
                    ALTER TABLE Cards ADD Currency varchar(100) NOT NULL;
                    ALTER TABLE Cards ADD Name varchar(100) NOT NULL;"; 
        }

        public string UsersEncryptionDrop()
        {
            return @"DELETE FROM Users;
                    ALTER TABLE Users
                        DROP
                            COLUMN IF EXISTS FirstName, 
                            COLUMN IF EXISTS LastName,
                            COLUMN IF EXISTS Email; 

                    ALTER TABLE Users ADD Email varchar(255) NOT NULL
                    ALTER TABLE Users ADD FirstName varchar(255) NOT NULL
                    ALTER TABLE Users ADD LastName varchar(255) NOT NULL";
        }

        public string UsersEncryptionAdd()
        {
            return @"DELETE FROM Users;
                    ALTER TABLE Users
                        DROP
                            COLUMN IF EXISTS FirstName, 
                            COLUMN IF EXISTS LastName,
                            COLUMN IF EXISTS Email; 

                    ALTER TABLE Users ADD Email varchar(255) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Users ADD FirstName varchar(255) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL
                    ALTER TABLE Users ADD LastName varchar(255) ENCRYPTED WITH (ENCRYPTION_TYPE = RANDOMIZED, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = xperters) NOT NULL";
        }

        public void CreateMasterKey()
        {
            var sql =  $@"
                CREATE COLUMN MASTER KEY xperters
                WITH
                (
	                KEY_STORE_PROVIDER_NAME = N'AZURE_KEY_VAULT',
	                KEY_PATH = N'https://{_keyVaultName}.vault.azure.net/keys/{_keyVaultKeyName}/{_keyVaultKeyVersion}'
                )
            ";

            DropMasterKey();
            ExecuteSql(sql);            
        }

        public void DropMasterKey()
        {
            var sql = @"
                if exists (select name from  sys.column_master_keys where name = 'xperters')
                begin
	                drop column master key xperters;
	                print 'CMK xperters dropped';
                end
                else
	                print 'CMK xperters does not exist';
            ";

            ExecuteSql(sql);
        }

        public void CreateEncryptionKey()
        {
            var keyId = $"https://{_keyVaultName}.vault.azure.net/keys/{_keyVaultKeyName}";
            CreateColumnEncryptionKey(keyId);
        }

        public void DropEncryptionKey()
        {
            var sql =  @"
                if exists (select name from sys.column_encryption_keys where name = 'xperters')
                begin
	                drop column encryption key xperters;
	                print 'CEK xperters dropped';
                end
                else
	                print 'CEK xperters does not exist';
            ";

            ExecuteSql(sql);
        }

        [SuppressMessage("Microsoft.Security", "CA2100", 
            Justification = "The SqlCommand text is issuing a DDL statement that requires to use only literals (no parameterization is possible). The user input is being escaped.", Scope = "method")]
        private void CreateColumnEncryptionKey(string keyId)
        {
            // Generate the raw bytes that will be used as a key by using a CSPRNG 
            var cekRawValue = new byte[32];
            var provider = new RNGCryptoServiceProvider();
            provider.GetBytes(cekRawValue);

            var cekEncryptedValue = _provider.EncryptColumnEncryptionKey(keyId, @"RSA_OAEP", cekRawValue);

            // Prevent SQL injections by escaping the user-defined tokens 
            var sql =  string.Format(CreateColumnEncryptionKeyTemplate, _applicationName, _applicationName, BytesToHex(cekEncryptedValue));

            ExecuteSql(sql);
        }


        private void ExecuteSql(string sql)
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = sql;

            command.ExecuteNonQuery();
        }

        private static string BytesToHex(byte[] a)
        {
            var temp = BitConverter.ToString(a);
            var len = a.Length;

            // We need to remove the dashes that come from the BitConverter
            var sb = new StringBuilder((len - 2) / 2); // This should be the final size

            foreach (var t in temp)
                if (t != '-')
                    sb.Append(t);

            return "0x" + sb;
        }
    }
}
