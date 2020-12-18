using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using xperters.fileio;
using xperters.configurations.Extensions;
using xperters.configurations.Interfaces;
using xperters.configurations.Settings.Ad;
using xperters.configurations.Settings.Payments;
using xperters.extensions;
using xperters.configurations.Settings.MobilePhoneService;
using xperters.configurations.Settings;
using xperters.constants;

namespace xperters.configurations
{
    public class AppConfigBuilder
    {
        private readonly IHandleFiles _filesHandler;
        private readonly ILogger _logger;
        private readonly AppConfig _appConfig;
        private readonly IHandleEnvironment _environmentHandler;


        public AppConfigBuilder(IHostEnvironment hostingEnvironment, IServiceCollection serviceCollection) : this(serviceCollection, new FilesHandler(hostingEnvironment), new LoggerFactory())
        {

        }


        public AppConfigBuilder(IServiceCollection services, IHandleFiles filesHandler, ILoggerFactory loggerFactory)
        {
            _filesHandler = filesHandler;
            _logger = loggerFactory.CreateLogger<AppConfigBuilder>();

            var configuration = GetFileConfigSettings();

            _appConfig = services.SetupConfigObject(configuration);
            var provider = services.BuildServiceProvider();
            _environmentHandler = provider.GetService<IHandleEnvironment>();
            _logger.LogDebug("Configured logging");
        }

        private IConfigurationRoot GetFileConfigSettings()
        {
            // create new configuration from existing config
            // and override whatever needed
            var currentDirectory = _filesHandler.GetContentRootPath();
            _logger.LogInformation($"Looking in {currentDirectory} for the appsettings file");

            // Check for an existing appsettings file
            if (currentDirectory.IsBlank() || 
                !_filesHandler.CheckFilePathExists(currentDirectory, "appsettings.json"))
            {
                var message = "appsettings file not found. Configuration values will not have been correctly set";
                _logger.LogCritical(message);
                throw new ArgumentNullException(message);
            }

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = configBuilder.Build();
            
            _logger.LogDebug("Found appsettings");

            return configuration;
        }

        public AppConfig Build()
        {

            // There seems to be a bug related to appsettings in Azure Web App for Container and the portal/docker
            // Until it's resolved, this code manually checks for running in container and assigns the values
            // https://stackoverflow.com/questions/50543509/azure-webapp-containers-and-appsettings-environment-variables/50545439

            if (_appConfig.GetAppIsRunningInContainer(_environmentHandler))
            {
                ConfigureBaseSettings();
                ConfigureStorageSettings();
                ConfigureServiceBusSettings();
                ConfigureAdAppRegSettings();
                ConfigureAdB2CSettings();
                ConfigureMomoSettings();
                ConfigureKeyVaultSettings();
                ConfigureWorkerSettings();
                ConfigureMobilePhoneSettings();
                ConfigureAzureAdSettings();
            }
            else
            {
                _logger.LogDebug("App settings read from appsettings.json file");
            }

            return _appConfig;
        }

        private void ConfigureBaseSettings()
        {
            // Azure
            var parentType = _appConfig.GetType();
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.DatabaseConnectionString?.GetType(), "DatabaseConnectionString", "Database__DatabaseConnectionString");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.MockUserData.GetType(), "MockUserData", "Settings__MockUserData");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.MockConnections.GetType(), "MockConnections", "Settings__MockConnections");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.HostingLocation?.GetType(), "HostingLocation", "Settings__HostingLocation");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.WebsiteAddress?.GetType(), "WebsiteAddress", "Settings__WebsiteAddress");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.Environment?.GetType(), "Environment", "Settings__Environment");
            OverrideSettingWithEnvVariable(_appConfig, parentType, _appConfig.ApplicationInsightsKey?.GetType(), "ApplicationInsightsKey", "ApplicationInsights__InstrumentationKey");
        }
        private void ConfigureStorageSettings()
        {
            var settings = _appConfig.Storage ?? new StorageAccountSettings();
            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.BaseUrl?.GetType(), "BaseUrl", "Storage__BaseUrl");
            OverrideSettingWithEnvVariable(settings, parentType, settings.WebKeysContainer?.GetType(), "WebKeysContainer", "Storage__WebKeysContainer");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ConnectionString?.GetType(), "ConnectionString", "Storage__ConnectionString");
            OverrideSettingWithEnvVariable(settings, parentType, settings.AccountName?.GetType(), "AccountName", "Storage__AccountName");
        }
        private void ConfigureServiceBusSettings()
        {
            var settings = _appConfig.ServiceBus ?? new ServiceBusSettings();
            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.NameSpace?.GetType(), "NameSpace", "ServiceBus__NameSpace", true);
            OverrideSettingWithEnvVariable(settings, parentType, settings.ConcurrentThreads.GetType(), "ConcurrentThreads", "ServiceBus__ConcurrentThreads", true);

            var queue1 = _appConfig.ServiceBus?.Queues[QueueNameConstants.MilestoneRequestPayers];

            var queueType1 = queue1?.GetType();
            OverrideSettingWithEnvVariable(queue1, queueType1, queue1?.Name?.GetType(), "Name", "ServiceBus__Queues__MilestoneRequestPayers__Name", true);
            OverrideSettingWithEnvVariable(queue1, queueType1, queue1?.ConnectionString?.GetType(), "ConnectionString", "ServiceBus__Queues__MilestoneRequestPayers__ConnectionString", true);
            settings.Queues.Remove(QueueNameConstants.MilestoneRequestPayers);
            settings.Queues.Add(QueueNameConstants.MilestoneRequestPayers, queue1);

            var queue2 = _appConfig.ServiceBus?.Queues[QueueNameConstants.MilestoneSystemRequestPayers];
            var queueType2 = queue2?.GetType();
            OverrideSettingWithEnvVariable(queue2, queueType2, queue2?.Name?.GetType(), "Name", "ServiceBus__Queues__MilestoneSystemRequestPayers__Name", true);
            OverrideSettingWithEnvVariable(queue2, queueType2, queue2?.ConnectionString.GetType(), "ConnectionString", "ServiceBus__Queues__MilestoneSystemRequestPayers__ConnectionString", true);

            settings.Queues.Remove(QueueNameConstants.MilestoneSystemRequestPayers);
            settings.Queues.Add(QueueNameConstants.MilestoneSystemRequestPayers, queue2);
        }

        private void ConfigureMobilePhoneSettings()
        {
            var settings = _appConfig.MobilePhoneServiceSettings ?? new MobilePhoneServiceSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.TenantId?.GetType(), "TenantId", "MobilePhoneServiceSettings__TenantId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.Email?.GetType(), "Email", "MobilePhoneServiceSettings__Email");
            OverrideSettingWithEnvVariable(settings, parentType, settings.Password?.GetType(), "Password", "MobilePhoneServiceSettings__Password");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ResourceId?.GetType(), "ResourceId", "MobilePhoneServiceSettings__ResourceId");
            _appConfig.MobilePhoneServiceSettings = settings;
        }

        private void ConfigureAdAppRegSettings()
        {

            var settings = _appConfig.AzureAdAppRegSettings ?? new AzureAdAppRegSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientId?.GetType(), "ClientId", "Authentication__AzureAdAppReg__ClientId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientSecret?.GetType(), "ClientSecret", "Authentication__AzureAdAppReg__ClientSecret");
            OverrideSettingWithEnvVariable(settings, parentType, settings.TenantId?.GetType(), "TenantId", "Authentication__AzureAdAppReg__TenantId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientIdSql?.GetType(), "ClientIdSql", "Authentication__AzureAdAppReg__ClientIdSql");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientSecretSql?.GetType(), "ClientSecretSql", "Authentication__AzureAdAppReg__ClientSecretSql");
            OverrideSettingWithEnvVariable(settings, parentType, settings.TenantIdSql?.GetType(), "TenantIdSql", "Authentication__AzureAdAppReg__TenantIdSql");
            OverrideSettingWithEnvVariable(settings, parentType, settings.MsOnlineTokenUrl?.GetType(), "MsOnlineTokenUrl", "Authentication__AzureAdAppReg__MsOnlineTokenUrl");
            OverrideSettingWithEnvVariable(settings, parentType, settings.MsGraphUrl?.GetType(), "MsGraphUrl", "Authentication__AzureAdAppReg__MsGraphUrl");
            OverrideSettingWithEnvVariable(settings, parentType, settings.MsGraphScope?.GetType(), "MsGraphScope", "Authentication__AzureAdAppReg__MsGraphScope");
            OverrideSettingWithEnvVariable(settings, parentType, settings.MsGraphNetUrl?.GetType(), "MsGraphNetUrl", "Authentication__AzureAdAppReg__MsGraphNetUrl");
            OverrideSettingWithEnvVariable(settings, parentType, settings.MsGraphApiVersion?.GetType(), "MsGraphApiVersion", "Authentication__AzureAdAppReg__MsGraphApiVersion");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ResourceApiIamAzureAd?.GetType(), "ResourceApiIamAzureAd", "Authentication__AzureAdAppReg__ResourceApiIamAzureAd");
        }

        private void ConfigureAdB2CSettings()
        {
            var settings = _appConfig.AzureAdB2CSettings ?? new AzureAdB2CSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientId?.GetType(), "ClientId", "Authentication__AzureAdB2C__ClientId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientSecret?.GetType(), "ClientSecret", "Authentication__AzureAdB2C__ClientSecret");
            OverrideSettingWithEnvVariable(settings, parentType, settings.Domain?.GetType(), "Domain", "Authentication__AzureAdB2C__Domain");
            OverrideSettingWithEnvVariable(settings, parentType, settings.Tenant?.GetType(), "Tenant", "Authentication__AzureAdB2C__Tenant");
            OverrideSettingWithEnvVariable(settings, parentType, settings.GraphUri?.GetType(), "GraphUri", "Authentication__AzureAdB2C__GraphUri");
            OverrideSettingWithEnvVariable(settings, parentType, settings.GraphRelativePath?.GetType(), "GraphRelativePath", "Authentication__AzureAdB2C__GraphRelativePath");
            OverrideSettingWithEnvVariable(settings, parentType, settings.SignUpSignInPolicyId?.GetType(), "SignUpSignInPolicyId", "Authentication__AzureAdB2C__SignUpSignInPolicyId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ResetPasswordPolicyId?.GetType(), "ResetPasswordPolicyId", "Authentication__AzureAdB2C__ResetPasswordPolicyId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.EditProfilePolicyId?.GetType(), "EditProfilePolicyId", "Authentication__AzureAdB2C__EditProfilePolicyId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.RedirectUri?.GetType(), "RedirectUri", "Authentication__AzureAdB2C__RedirectUri");
        }

        private void ConfigureMomoSettings()
        {

            var paymentSettings = _appConfig.MomoPaymentSettings ?? new MomoPaymentSettings();
            var collection = paymentSettings.Collection ?? new MomoSettings();
            var disbursement = paymentSettings.Disbursement ?? new MomoSettings();

            var parentType = collection.GetType();
            OverrideSettingWithEnvVariable(collection, parentType, collection.SubscriptionKey?.GetType(), "SubscriptionKey", "MomoSettings__Collection__SubscriptionKey");
            OverrideSettingWithEnvVariable(collection, parentType, collection.UserId?.GetType(), "UserId", "MomoSettings__Collection__UserId");
            OverrideSettingWithEnvVariable(collection, parentType, collection.UserSecretKey?.GetType(), "UserSecretKey", "MomoSettings__Collection__UserSecretKey");

            parentType = disbursement.GetType();
            OverrideSettingWithEnvVariable(disbursement, parentType, disbursement.SubscriptionKey?.GetType(), "SubscriptionKey", "MomoSettings__Disbursement__SubscriptionKey");
            OverrideSettingWithEnvVariable(disbursement, parentType, disbursement.UserId?.GetType(), "UserId", "MomoSettings__Disbursement__UserId");
            OverrideSettingWithEnvVariable(disbursement, parentType, disbursement.UserSecretKey?.GetType(), "UserSecretKey", "MomoSettings__Disbursement__UserSecretKey");

            parentType = paymentSettings.GetType();
            OverrideSettingWithEnvVariable(paymentSettings, parentType, paymentSettings.BaseUri?.GetType(), "BaseUri", "MomoSettings__BaseUri");
            OverrideSettingWithEnvVariable(paymentSettings, parentType, paymentSettings.Environment?.GetType(), "Environment", "MomoSettings__Environment");            
        }

        private void ConfigureKeyVaultSettings()
        {
            var settings = _appConfig.KeyVaultSettings ?? new KeyVaultSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.Name?.GetType(), "Name", "KeyVault__Name");
            OverrideSettingWithEnvVariable(settings, parentType, settings.KeyName?.GetType(), "KeyName", "KeyVault__KeyName");
            OverrideSettingWithEnvVariable(settings, parentType, settings.KeyVersion?.GetType(), "KeyVersion", "KeyVault__KeyVersion");
            OverrideSettingWithEnvVariable(settings, parentType, settings.KeyEncryptedValue?.GetType(), "KeyEncryptedValue", "KeyVault__KeyEncryptedValue");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ProtectionKeyUrl?.GetType(), "ProtectionKeyUrl", "KeyVault__ProtectionKeyUrl");
        }

        private void ConfigureWorkerSettings()
        {
            var settings = _appConfig.WorkerSettings ?? new WorkerSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.NumberOfWorkers.GetType(), "NumberOfWorkers", "WorkerSettings__NumberOfWorkers");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ThreadSleepMilliseconds.GetType(), "ThreadSleepMilliseconds", "WorkerSettings__ThreadSleepMilliseconds");
            OverrideSettingWithEnvVariable(settings, parentType, settings.TestingMRPItemsCount.GetType(), "TestingMRPItemsCount", "WorkerSettings__TestingMRPItemsCount");
            OverrideSettingWithEnvVariable(settings, parentType, settings.TestingMSRPItemsCount.GetType(), "TestingMSRPItemsCount", "WorkerSettings__TestingMSRPItemsCount");
        }

        private void ConfigureAzureAdSettings()
        {
            var settings = _appConfig.AzureAdSettings ?? new AzureAdSettings();

            var parentType = settings.GetType();
            OverrideSettingWithEnvVariable(settings, parentType, settings.Domain?.GetType(), "Domain", "Authentication__AzureAd__Domain");
            OverrideSettingWithEnvVariable(settings, parentType, settings.TenantId?.GetType(), "TenantId", "Authentication__AzureAd__TenantId");
            OverrideSettingWithEnvVariable(settings, parentType, settings.ClientId?.GetType(), "ClientId", "AzureAd__ClientId");

            var settingsGroup = settings.Groups ?? new GroupSettings();

            parentType = settingsGroup.GetType();
            OverrideSettingWithEnvVariable(settingsGroup, parentType, settingsGroup.Readers?.GetType(), "Readers", "Authentication__AzureAd__Groups__Readers");
            OverrideSettingWithEnvVariable(settingsGroup, parentType, settingsGroup.Writers?.GetType(), "Writers", "Authentication__AzureAd__Groups__Writers");
            OverrideSettingWithEnvVariable(settingsGroup, parentType, settingsGroup.Admins?.GetType(), "Admins", "Authentication__AzureAd__Groups__Admins");

        }

        private void OverrideSettingWithEnvVariable(object instance, Type parentType, Type fieldType, string fieldName, string variableName, bool criticalSetting = false)
        {
            if (parentType == null || fieldType == null)
            {
                _logger.LogDebug($"Setting {fieldName} missing value in appsettings.json config file");
                return;
            }

            var variableValue = _environmentHandler.GetVariable(variableName);
            if (variableValue.IsNotBlank())
            {
                var propertyInfo = parentType.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public, null, fieldType, new Type[0], null);

                if (propertyInfo==null)
                {
                    if(criticalSetting){
                        _logger.LogCritical($"Variable {nameof(fieldName)} of type {fieldType} for variablename {variableName} not found");
                    }
                    else{
                        _logger.LogWarning($"Variable {nameof(fieldName)} of type {fieldType} for variablename {variableName} not found");
                    }
                    return;
                }

                // if we are not dealing with a string, convert to target type
                try
                {
                    if (fieldType != typeof(string))
                    {
                        dynamic result = Convert.ChangeType(variableValue, fieldType);
                        propertyInfo.SetValue(instance, result, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, variableValue, null);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Values are: Fieldname: {fieldName}. Variablename: {variableName}.  Value: {variableValue}. Message {ex.Message}");
                    throw;
                }

                _logger.LogDebug($"Configured {fieldName} settings for Azure");
            }
            else
            {
                if(criticalSetting){
                    _logger.LogCritical($"Variable {nameof(fieldName)} of type {fieldType} for variablename {variableName} not found");
                }
                else{
                    _logger.LogWarning($"Variable {nameof(fieldName)} of type {fieldType} for variablename {variableName} not found");
                }
            }
        }
    }
}
