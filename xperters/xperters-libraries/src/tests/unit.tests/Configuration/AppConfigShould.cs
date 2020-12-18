using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using xperters.configurations;
using xperters.constants;
using xperters.entities.Extensions;
using xperters.tests.common;
using Xunit;
// ReSharper disable InconsistentNaming

namespace xperters.unit.tests.Configuration
{
    public class AppConfigShould
    {
        private readonly EnvironmentHandlerMock _environmentHandlerMock;
        private readonly Dictionary<string, string> _appSettings;
        const string DatabaseConnectionString = "Database__DatabaseConnectionString";
        const string MockUserData = "Settings__MockUserData";
        const string MockConnections = "Settings__MockConnections";

        const string StorageBaseUrl = "Storage__BaseUrl";
        const string StorageConnectionString = "Storage__ConnectionString";
        const string StorageWebKeysContainer = "Storage__WebKeysContainer";
        const string StorageAccountName = "Storage__AccountName";


        const string ServiceBusNameSpace = "ServiceBus__NameSpace";
        const string ServiceBusConcurrentThreads = "ServiceBus__ConcurrentThreads";
        const string ConnectionStringMilestoneRequestPayers = "ServiceBus__Queues__MilestoneRequestPayers__ConnectionString";
        const string ConnectionStringMilestoneSystemRequestPayers = "ServiceBus__Queues__MilestoneSystemRequestPayers__ConnectionString";
        const string ServiceBusQueueNameMilestoneRequestPayers = "ServiceBus__Queues__MilestoneRequestPayers__Name";
        const string ServiceBusQueueNameMilestoneSystemRequestPayers = "ServiceBus__Queues__MilestoneSystemRequestPayers__Name";


        const string HostingLocation = "Settings__HostingLocation";
        const string WebsiteAddress = "Settings__WebsiteAddress";
        const string Environment = "Settings__Environment";
        const string ApplicationInsightsKey = "ApplicationInsights__InstrumentationKey";

        const string ClientIdAppReg = "Authentication__AzureAdAppReg__ClientId";
        const string ClientSecretAppReg = "Authentication__AzureAdAppReg__ClientSecret";
        const string TenantId = "Authentication__AzureAdAppReg__TenantId";
        const string MsonlineTokenUrl = "Authentication__AzureAdAppReg__MsOnlineTokenUrl";
        const string MsGraphScope = "Authentication__AzureAdAppReg__MsGraphScope";
        const string MsGraphUrl = "Authentication__AzureAdAppReg__MsGraphUrl";
        const string MsGraphNetUrl = "Authentication__AzureAdAppReg__MsGraphNetUrl";
        const string MsGraphApiVersion = "Authentication__AzureAdAppReg__MsGraphApiVersion";
        const string ResourceApiIamAzureAd = "Authentication__AzureAdAppReg__ResourceApiIamAzureAd";
        const string ClientIdSql = "Authentication__AzureAdAppReg__ClientIdSql";
        const string ClientSecretSql = "Authentication__AzureAdAppReg__ClientSecretSql";
        const string TenantIdSql = "Authentication__AzureAdAppReg__TenantIdSql";

        const string ClientIdB2C = "Authentication__AzureAdB2C__ClientId";
        const string ClientSecretB2C = "Authentication__AzureAdB2C__ClientSecret";
        const string Tenant = "Authentication__AzureAdB2C__Tenant";
        const string Domain = "Authentication__AzureAdB2C__Domain";
        const string GraphUri = "Authentication__AzureAdB2C__GraphUri";
        const string GraphRelativePath = "Authentication__AzureAdB2C__GraphRelativePath";
        const string SignupsigninPolicyId = "Authentication__AzureAdB2C__SignUpSignInPolicyId";
        const string ResetpasswordPolicyId = "Authentication__AzureAdB2C__ResetPasswordPolicyId";
        const string EditProfilePolicyId = "Authentication__AzureAdB2C__EditProfilePolicyId";
        const string RedirectUri = "Authentication__AzureAdB2C__RedirectUri";

        const string MomoSettingsCollectionSubscriptionKey = "MomoSettings__Collection__SubscriptionKey";
        const string MomoSettingsDisbursementSubscriptionKey = "MomoSettings__Disbursement__SubscriptionKey";
        const string MomoSettingsBaseUri = "MomoSettings__BaseUri";
        const string MomoSettingsEnvironment = "MomoSettings__Environment";
        const string MomoSettingsCollectionUserId = "MomoSettings__Collection__UserId";
        const string MomoSettingsCollectionUserSecretKey = "MomoSettings__Collection__UserSecretKey";
        const string MomoSettingsDisbursementUserId = "MomoSettings__Disbursement__UserId";
        const string MomoSettingsDisbursementUserSecretKey = "MomoSettings__Disbursement__UserSecretKey";

        const string KeyVaultSettingsName = "KeyVault__Name";
        const string KeyVaultSettingsKeyName = "KeyVault__KeyName";
        const string KeyVaultSettingsKeyVersion = "KeyVault__KeyVersion";
        const string KeyVaultSettingsKeyEncryptedValue = "KeyVault__KeyEncryptedValue";
        const string KeyVaultSettingsProtectionUrlValue = "KeyVault__ProtectionKeyUrl";

        const string WorkerSettingsNumberOfWorkers = "WorkerSettings__NumberOfWorkers";
        const string WorkerSettingsThreadSleepMilliseconds = "WorkerSettings__ThreadSleepMilliseconds";
        const string WorkerSettingsTestingMRPItemsCount = "WorkerSettings__TestingMRPItemsCount";
        const string WorkerSettingsTestingMSRPItemsCount = "WorkerSettings__TestingMSRPItemsCount";

        const string AzureAdDomain = "Authentication__AzureAd__Domain";
        const string AzureAdTenantId = "Authentication__AzureAd__TenantId";
        const string AzureAdClientId = "Authentication__AzureAd__ClientId";

        const string AzureAdRolesWriters = "Authentication__AzureAd__Roles__Writers";
        const string AzureAdRolesReaders = "Authentication__AzureAd__Roles__Readers";
        const string AzureAdRolesAdmins = "Authentication__AzureAd__Roles__Admins";

        private const string DbConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=xperters;Data Source=.";
        private const string SbConnectionStringMrp = "Endpoint=sb://xperters-test.servicebus.windows.net/;SharedAccessKeyName=readwriters;SharedAccessKey=8pouSqL1x3yeiKL64rK6vS3cVphKUA/Njzeb8LOhVXA=;EntityPath=xperters.local.milestonerequestpayers";
        private const string SbConnectionStringMsrp = "Endpoint=sb://xperters-test.servicebus.windows.net/;SharedAccessKeyName=readwriters;SharedAccessKey=5LIArK9l/jhgono7F7wpSZ21ZMtlCvhmUZKJL1r7cTA=;EntityPath=xperters.local.milestonesystemrequestpayers";
        private const string SbConnectionStringMrpLocal = "Endpoint=sb://xperters-local.servicebus.windows.net/;SharedAccessKeyName=readwriters;SharedAccessKey=8pouSqL1x3yeiKL64rK6vS3cVphKUA/Njzeb8LOhVXA=;EntityPath=xperters.local.milestonerequestpayers";
        private const string SbConnectionStringMsrpLocal = "Endpoint=sb://xperters-local.servicebus.windows.net/;SharedAccessKeyName=readwriters;SharedAccessKey=5LIArK9l/jhgono7F7wpSZ21ZMtlCvhmUZKJL1r7cTA=;EntityPath=xperters.local.milestonesystemrequestpayers";

        public AppConfigShould()
        {
            string value1 = nameof(value1);
            string value2 = "true";
            string value3 = "true";
            string storageBaseUrl = AppSettings.StorageBaseUrlDeveloper;
            string storageConnectionString = AppSettings.StorageAccountDeveloper;
            string location = "uksouth";
            string websiteAddress = "localhost";
            string environment = "LOCAL";

            _appSettings = new Dictionary<string, string>
            {
                {XpertersEnvVariables.DotnetRunningInContainer, "true"},
                {DatabaseConnectionString, value1},
                {MockUserData, value2},
                {MockConnections, value3},

                {StorageBaseUrl, storageBaseUrl},
                {StorageConnectionString, storageConnectionString},
                {StorageWebKeysContainer, "webkeys"},
                {StorageAccountName, "devstoreaccount1"},

                {ServiceBusNameSpace, "xperters-local"},
                {ServiceBusConcurrentThreads, "5"},
                {ConnectionStringMilestoneRequestPayers, SbConnectionStringMrp},
                {ConnectionStringMilestoneSystemRequestPayers, SbConnectionStringMsrp},
                {ServiceBusQueueNameMilestoneRequestPayers, "xperters.local.milestonerequestpayers"},
                {ServiceBusQueueNameMilestoneSystemRequestPayers, "xperters.local.milestonesystemrequestpayers"},

                {HostingLocation, location},
                {WebsiteAddress, websiteAddress},
                {Environment, environment},

                {ClientIdAppReg, "3eba3c73-f3f6-46e1-a19f-0a2c4a82267d"},
                {ClientSecretAppReg, "odDuvag/eF8SrBXe1ZGFFWZraBO5MljhcE62o40Ed0I="},
                {TenantId, "b8bccc9d-2910-469a-96bf-78ae7e1e7b33"},
                {MsonlineTokenUrl, "https://login.microsoftonline.com/"},
                {MsGraphScope, "https://graph.microsoft.com/.default"},
                {MsGraphUrl, "https://graph.microsoft.com/v1.0/users"},
                {MsGraphNetUrl, "https://graph.windows.net/"},
                {MsGraphApiVersion, "api-version=1.6"},
                {ResourceApiIamAzureAd, "74658136-14ec-4630-ad9b-26e160ff0fc6"},

                {ClientIdB2C, "33393268-e208-416d-aad8-e1f982a39010"},
                {ClientSecretB2C, "shy15XB/.f:KlZ0Iy2_rlChv"},
                {Domain, "xpertersdevlocal.b2clogin.com"},
                {Tenant, "xpertersdevlocal.onmicrosoft.com"},
                {GraphUri, "https://graph.microsoft.com/"},
                {GraphRelativePath, "v1.0/users"},
                {SignupsigninPolicyId, "b2c_1_susi"},
                {ResetpasswordPolicyId, "b2c_1_reset"},
                {EditProfilePolicyId, "b2c_1_edit_profile"},
                {RedirectUri, "https://localhost:44387/signin-oidc"},
                {ClientIdSql, "c20aef5a-6e75-444b-a0cd-e337ead68333"},
                {ClientSecretSql, "(wJDwbyhow?DG@lO"},     
                {TenantIdSql, "f818c762-5d03-4aa8-9be2-8b2a990cfcd2"},
                {ApplicationInsightsKey, "a49c50b9-f5c1-48ed-9d1e-583088fc8304"},

                {MomoSettingsBaseUri, "https://sandbox.momodeveloper.mtn.com"},
                {MomoSettingsEnvironment, "sandbox"},
                {MomoSettingsCollectionSubscriptionKey, "0ec2dea844394041b05fe2b758c25d31"},
                {MomoSettingsCollectionUserId, "f11ff76c-a940-4c8e-9eb5-e20edfc11ec3"},
                {MomoSettingsCollectionUserSecretKey, "0c3b9982a96a4966b47241e7163b1a56"},     
                {MomoSettingsDisbursementSubscriptionKey, "821871c33a414bd0818f1225e56a611b"},
                {MomoSettingsDisbursementUserId, "4d05c4ea-aee8-4d0e-97ee-81a316a14891"},
                {MomoSettingsDisbursementUserSecretKey, "62ddc39071b246c7ab8a2b8f5b9ccc71"},

                {KeyVaultSettingsName, "xperters-local"},
                {KeyVaultSettingsKeyName, "SQLCMK"},
                {KeyVaultSettingsKeyVersion, "9988a8017da140d8a2416218cff333c3"},
                {KeyVaultSettingsKeyEncryptedValue, "0x01A6000001680074007400700073003A002F002F00780070006500720074006500720073002D006C006F00630061006C002E007600610075006C0074002E0061007A007500720065002E006E00650074002F006B006500790073002F00730071006C0063006D006B002F00320035006300350065003300650038003000380065003500340061006200660062006200380061006200380030003400360064003500660063006200320064003EA9800449FC5A2CED412D75CCEE7B4F1028443B87591E8809B3EEC637185F9DFC7C09F09F26E3DBD5F66CDC45A0DC5221DD978B8661F46239F186A013FD9C0FB36B077B8A75CED6B954D86B93E22021D8CF353208A4C2C2494E289969386F2E687D9608C5BECF408FEAC3B8D17CEB08A587881BD7534CC54CFEF909026BC73F126C05D70154EF450E6CC208329918A3A88E8F5E6718C50412B5D07D43BAE6FAB4A56915ACD9B53936FBB40CADABFBA68CEF3F43747A402E161104BAE9BCDA58B4203CD66BE8E05E8487CE9383C15F898C6430DA366684ECC100B671DCBB2B2A0159F45F16E7A6A708765DF384F4C1071B345B97D5A3659505E4469C02CE32A146A52AB1A15492B1C2D341FBB4E2636679E9E1C5B17ED854D6DED4BBF18CBE56E6E11A91B27EF82D199A99BC332DD6BECD66FCD2D1711B6697DEAD8B619478E78F2C3198DE804734C175D7ADF59552CA15A98894FFE7B36C60CB04E026321835C80B336ACF51153794D4542F64E2B365F12947E949E27B51DF652D1F07B4BDEDBBCC9181FF5582C0877C5ABF6836DC6269F190C25F6E6F4E5CFC0E56F57265B6E8031FC3E0CE31381D0F7517C3B50311EC89138E47C5B31A5CCA94635AFA8CD450F5FEB114482CB9A369F4D064075B40AF58513C6BF8AED0E1DFD5996A44ED94BFDECDBEA5066A1A5B9D843D068766D88212963DEDD6FA10BC0C66F320034EFB"},
                {KeyVaultSettingsProtectionUrlValue, "https://xperters-local.vault.azure.net/keys/dataprotection/21efe78922e244fd8595045351847e19"},

                { WorkerSettingsNumberOfWorkers, "5"},
                { WorkerSettingsThreadSleepMilliseconds, "200"},
                { WorkerSettingsTestingMRPItemsCount, "10"},
                { WorkerSettingsTestingMSRPItemsCount, "20"},

                { AzureAdDomain, "iforemanrarockwell.onmicrosoft.com"},
                { AzureAdTenantId, "f818c762-5d03-4aa8-9be2-8b2a990cfcd2"},
                { AzureAdClientId, "d8302c4f-7ef1-49ac-92a0-a9c46b875313"},
                { AzureAdRolesWriters, "f565df69-26ea-4efe-84bf-b093e5277da7"},
                { AzureAdRolesReaders, "73019068-a239-4fb3-ab6a-9a23acf5f7f4"},
                { AzureAdRolesAdmins, "10bc8e6b-b0f0-4beb-a7de-7359d764b534"}
            };

            _environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);
        }

        [Fact]
        public void WhenRunningInIIS_DBAndStorageFound()
        {
            System.Environment.SetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER", "false");
            string connectionString = DbConnectionString;

            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object);
            var appConfigBuilder = new AppConfigBuilder(env.Object, serviceCollection);

            var config = appConfigBuilder.Build();

            Assert.Equal(connectionString, config.DatabaseConnectionString);
            Assert.Equal(AppSettings.StorageAccountDeveloper, config.Storage.ConnectionString);
            Assert.Equal(AppSettings.StorageBaseUrlDeveloper, config.Storage.BaseUrl);

            var item = config.ServiceBus.Queues["MilestoneRequestPayers"];
            Assert.Equal(SbConnectionStringMrpLocal, config.ServiceBus.Queues["MilestoneRequestPayers"].ConnectionString);
            Assert.Equal(SbConnectionStringMsrpLocal, config.ServiceBus.Queues["MilestoneSystemRequestPayers"].ConnectionString);

            Assert.True(config.MockConnections);
            Assert.True(config.MockUserData);

            Assert.Equal("userattachments", config.Storage.UserContainer);
            Assert.Equal("contractattachments", config.Storage.ContractContainer);
            Assert.Equal("jobattachments", config.Storage.JobContainer);
        }

        [Fact]
        public void WhenRunningInContainer_SettingsSectionFound()
        {
            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object, _environmentHandlerMock);
            var appConfigBuilder = new AppConfigBuilder(env.Object, services);
            var config = appConfigBuilder.Build();

            Assert.True(config.GetAppIsRunningInContainer(_environmentHandlerMock));
            Assert.Equal(_environmentHandlerMock.GetVariable(MockUserData), config.MockConnections.ToString().ToLower());
            Assert.Equal(_environmentHandlerMock.GetVariable(MockConnections), config.MockUserData.ToString().ToLower());
            Assert.Equal(_environmentHandlerMock.GetVariable(HostingLocation), config.HostingLocation);
            Assert.Equal(_environmentHandlerMock.GetVariable(WebsiteAddress), config.WebsiteAddress);
            Assert.Equal(_environmentHandlerMock.GetVariable(ApplicationInsightsKey), config.ApplicationInsightsKey);
            Assert.Equal(_environmentHandlerMock.GetVariable(Environment), config.Environment);
        }

        [Fact]
        public void WhenRunningInContainer_ApRegSectionFound()
        {
            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);
            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);

            var appConfigBuilder = new AppConfigBuilder(env.Object, services);
            var config = appConfigBuilder.Build();

            Assert.Equal(environmentHandlerMock.GetVariable(ClientIdAppReg), config.AzureAdAppRegSettings.ClientId);
            Assert.Equal(environmentHandlerMock.GetVariable(ClientSecretAppReg), config.AzureAdAppRegSettings.ClientSecret);
            Assert.Equal(environmentHandlerMock.GetVariable(TenantId), config.AzureAdAppRegSettings.TenantId);
            Assert.Equal(environmentHandlerMock.GetVariable(MsonlineTokenUrl), config.AzureAdAppRegSettings.MsOnlineTokenUrl);
            Assert.Equal(environmentHandlerMock.GetVariable(MsGraphScope), config.AzureAdAppRegSettings.MsGraphScope);
            Assert.Equal(environmentHandlerMock.GetVariable(MsGraphUrl), config.AzureAdAppRegSettings.MsGraphUrl);
            Assert.Equal(environmentHandlerMock.GetVariable(MsGraphNetUrl), config.AzureAdAppRegSettings.MsGraphNetUrl);
            Assert.Equal(environmentHandlerMock.GetVariable(MsGraphApiVersion), config.AzureAdAppRegSettings.MsGraphApiVersion);
            Assert.Equal(environmentHandlerMock.GetVariable(ResourceApiIamAzureAd), config.AzureAdAppRegSettings.ResourceApiIamAzureAd);
            Assert.Equal(environmentHandlerMock.GetVariable(ClientIdSql), config.AzureAdAppRegSettings.ClientIdSql);
            Assert.Equal(environmentHandlerMock.GetVariable(ClientSecretSql), config.AzureAdAppRegSettings.ClientSecretSql);
            Assert.Equal(environmentHandlerMock.GetVariable(TenantIdSql), config.AzureAdAppRegSettings.TenantIdSql);
        }

        [Fact]
        public void WhenRunningInContainer_AksSettingsFound()
        {
            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);
            var services = new ServiceCollection();
            services.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);

            var appConfigBuilder = new AppConfigBuilder(env.Object, services);
            var config = appConfigBuilder.Build();

            Assert.Equal(environmentHandlerMock.GetVariable(DatabaseConnectionString), config.DatabaseConnectionString);

            Assert.Equal(environmentHandlerMock.GetVariable(StorageBaseUrl), config.Storage.BaseUrl);
            Assert.Equal(environmentHandlerMock.GetVariable(StorageConnectionString), config.Storage.ConnectionString);
            Assert.Equal(environmentHandlerMock.GetVariable(StorageWebKeysContainer), config.Storage.WebKeysContainer);            
            Assert.Equal(environmentHandlerMock.GetVariable(StorageAccountName), config.Storage.AccountName);            

            Assert.Equal(environmentHandlerMock.GetVariable(ServiceBusNameSpace), config.ServiceBus.NameSpace);
            Assert.Equal(environmentHandlerMock.GetVariable(ServiceBusConcurrentThreads), config.ServiceBus.ConcurrentThreads.ToString());


            var mrp = config.ServiceBus.Queues["MilestoneRequestPayers"];
            Assert.Equal(environmentHandlerMock.GetVariable(ConnectionStringMilestoneRequestPayers), mrp.ConnectionString);
            Assert.Equal(environmentHandlerMock.GetVariable(ServiceBusQueueNameMilestoneRequestPayers), mrp.Name);

            var msrp = config.ServiceBus.Queues["MilestoneSystemRequestPayers"];
            Assert.Equal(environmentHandlerMock.GetVariable(ConnectionStringMilestoneSystemRequestPayers), msrp.ConnectionString);
            Assert.Equal(environmentHandlerMock.GetVariable(ServiceBusQueueNameMilestoneSystemRequestPayers), msrp.Name);

            Assert.Equal(environmentHandlerMock.GetVariable(MomoSettingsBaseUri), config.MomoPaymentSettings.BaseUri);
            Assert.Equal(environmentHandlerMock.GetVariable(MomoSettingsEnvironment), config.MomoPaymentSettings.Environment);
            Assert.Equal(environmentHandlerMock.GetVariable(MomoSettingsCollectionSubscriptionKey), config.MomoPaymentSettings.Collection.SubscriptionKey);
            Assert.Equal(environmentHandlerMock.GetVariable(MomoSettingsCollectionUserId), config.MomoPaymentSettings.Collection.UserId);
            Assert.Equal(environmentHandlerMock.GetVariable(MomoSettingsCollectionUserSecretKey), config.MomoPaymentSettings.Collection.UserSecretKey);
        }        
        
        [Fact]
        public void WhenRunningInContainer_NotFoundIfMissingFromAppsettings()
        {
            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);
            var services = new ServiceCollection();
            environmentHandlerMock.SetVariable(DatabaseConnectionString, null);

            services.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);

            var appConfigBuilder = new AppConfigBuilder(env.Object, services);
            var config = appConfigBuilder.Build();

            Assert.Equal(DbConnectionString, config.DatabaseConnectionString);
        }

        [Fact]
        public void WhenRunningInContainer_AdB2CSettingsSectionFound()
        {

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);

            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);
            var appConfigBuilder = new AppConfigBuilder(env.Object, serviceCollection);

            var config = appConfigBuilder.Build();

            Assert.Equal(environmentHandlerMock.GetVariable(ClientIdB2C), config.AzureAdB2CSettings.ClientId);
            Assert.Equal(environmentHandlerMock.GetVariable(ClientSecretB2C), config.AzureAdB2CSettings.ClientSecret);
            Assert.Equal(environmentHandlerMock.GetVariable(Domain), config.AzureAdB2CSettings.Domain);
            Assert.Equal(environmentHandlerMock.GetVariable(Tenant), config.AzureAdB2CSettings.Tenant);
            Assert.Equal(environmentHandlerMock.GetVariable(GraphUri), config.AzureAdB2CSettings.GraphUri);
            Assert.Equal(environmentHandlerMock.GetVariable(GraphRelativePath), config.AzureAdB2CSettings.GraphRelativePath);
            Assert.Equal(environmentHandlerMock.GetVariable(SignupsigninPolicyId), config.AzureAdB2CSettings.SignUpSignInPolicyId);
            Assert.Equal(environmentHandlerMock.GetVariable(ResetpasswordPolicyId), config.AzureAdB2CSettings.ResetPasswordPolicyId);
            Assert.Equal(environmentHandlerMock.GetVariable(EditProfilePolicyId), config.AzureAdB2CSettings.EditProfilePolicyId);
            Assert.Equal(environmentHandlerMock.GetVariable(RedirectUri), config.AzureAdB2CSettings.RedirectUri);
        }     
        
        [Fact]
        public void WhenRunningInContainer_KeyVaultSettingsSectionFound()
        {

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);

            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);
            var appConfigBuilder = new AppConfigBuilder(env.Object, serviceCollection);

            var config = appConfigBuilder.Build();

            Assert.Equal(environmentHandlerMock.GetVariable(KeyVaultSettingsName), config.KeyVaultSettings.Name);
            Assert.Equal(environmentHandlerMock.GetVariable(KeyVaultSettingsKeyName), config.KeyVaultSettings.KeyName);
            Assert.Equal(environmentHandlerMock.GetVariable(KeyVaultSettingsKeyVersion), config.KeyVaultSettings.KeyVersion);
            Assert.Equal(environmentHandlerMock.GetVariable(KeyVaultSettingsKeyEncryptedValue), config.KeyVaultSettings.KeyEncryptedValue);
            Assert.Equal(environmentHandlerMock.GetVariable(KeyVaultSettingsProtectionUrlValue), config.KeyVaultSettings.ProtectionKeyUrl);
        }

        [Fact]
        public void WhenRunningInContainer_WorkerSettingsSectionFound()
        {

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);

            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);
            var appConfigBuilder = new AppConfigBuilder(env.Object, serviceCollection);

            var config = appConfigBuilder.Build();

            Assert.Equal(Convert.ToByte(environmentHandlerMock.GetVariable(WorkerSettingsNumberOfWorkers)), config.WorkerSettings.NumberOfWorkers);
            Assert.Equal(short.Parse(environmentHandlerMock.GetVariable(WorkerSettingsThreadSleepMilliseconds)), config.WorkerSettings.ThreadSleepMilliseconds);
            Assert.Equal(Convert.ToInt32(environmentHandlerMock.GetVariable(WorkerSettingsTestingMRPItemsCount)), config.WorkerSettings.TestingMRPItemsCount);
            Assert.Equal(Convert.ToInt32(environmentHandlerMock.GetVariable(WorkerSettingsTestingMSRPItemsCount)), config.WorkerSettings.TestingMSRPItemsCount);
        }        
        
        [Fact]
        public void WhenRunningInContainer_AzureAdSettingsSectionFound()
        {

            var environmentHandlerMock = new EnvironmentHandlerMock(_appSettings);

            // create new configuration from existing config
            // and override whatever needed
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object, environmentHandlerMock);
            var appConfigBuilder = new AppConfigBuilder(env.Object, serviceCollection);

            var config = appConfigBuilder.Build();

            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdDomain), config.AzureAdSettings.Domain);
            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdTenantId), config.AzureAdSettings.TenantId);
            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdClientId), config.AzureAdSettings.ClientId);

            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdRolesWriters), config.AzureAdSettings.Groups.Writers);
            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdRolesReaders), config.AzureAdSettings.Groups.Readers);
            Assert.Equal(environmentHandlerMock.GetVariable(AzureAdRolesAdmins), config.AzureAdSettings.Groups.Admins);
        }
    }
}
