namespace xperters.constants
{
    public class AppSettings
    {
        public const string StorageAccount = "Storage:StorageConnectionString";
        public const string StorageAccountDeveloper = "UseDevelopmentStorage=true";
        public const string StorageBaseUrl = "Storage:BaseUrl";
        public const string StorageBaseUrlDeveloper = "http://127.0.0.1:10000/devstoreaccount1";
        public const string DatabaseConnectionStringInMemory = "DataSource=:memory:";
        public const string DatabaseConnectionString = "DatabaseConnectionString";
        public const string DatabaseConnectionStringFull = "Database:DatabaseConnectionString";
        public const string MockUserData = "Settings:MockUserData";
        public const string MockConnections = "Settings:MockConnections";
        public const string AzureB2CSettings = "Authentication:AzureAdB2C";
        public const string AzureAdAppRegSettings = "Authentication:AzureAdAppReg";
        public const string AzureAdKeyVaultSettings = "Authentication:AzureAdKeyVault";
        public const string HostingLocation = "Settings:HostingLocation";
        public const string CookieName = "XperterCookie";
    }
}