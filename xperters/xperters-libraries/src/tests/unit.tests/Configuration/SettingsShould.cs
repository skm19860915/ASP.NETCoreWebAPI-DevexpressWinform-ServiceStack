using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.configurations;
using xperters.configurations.Settings.Ad;
using xperters.configurations.Settings.Payments;
using xperters.entities.Extensions;
using xperters.extensions;
using xperters.fileio;
using Xunit;

namespace xperters.unit.tests.Configuration
{
    public class SettingsShould
    {
        private readonly AppConfig _config;
        private readonly string _path;
        private readonly JengaSettings _jengaPaymentSettings;
        private readonly MomoPaymentSettings _momoPaymentSettings;
        private readonly KeyVaultSettings _keyVaultSettings;
        private readonly WorkerSettings _workerSettings;
        private readonly AzureAdSettings _azureAdSettings;
        private const string HostingLocation = "local";
        private const string WebAddress = "localhost";
        private const string StorageWebKeysContainer = "webkeys";
        private const string StorageWebKeysFile = "keys.xml";
        private const string StorageAccountName = "devstoreaccount1";

        public SettingsShould()
        {
            _path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(_path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(env.Object);
            var builder = new AppConfigBuilder(env.Object, serviceCollection);
            _config = builder.Build();
            _jengaPaymentSettings = _config.PaymentSettings.Jenga;
            _momoPaymentSettings = _config.MomoPaymentSettings;
            _keyVaultSettings = _config.KeyVaultSettings;
            _workerSettings = _config.WorkerSettings;
            _azureAdSettings = _config.AzureAdSettings;
        }

        [Fact]
        public void JengaWalletSettingsAreRead()
        {
            Assert.NotNull(_jengaPaymentSettings);
        }

        [Fact]
        public void ContainLocalForHostingLocation()
        {
            Assert.Equal(HostingLocation, _config.HostingLocation);
        }

        [Fact]
        public void ContainsStorageSettings()
        {
            Assert.Equal(StorageWebKeysFile, _config.Storage.WebKeysFile);
            Assert.Equal(StorageWebKeysContainer, _config.Storage.WebKeysContainer);
            Assert.Equal(StorageAccountName, _config.Storage.AccountName);
        }
        
        [Fact]
        public void ContainLocalhostForWebAddress()
        {
            Assert.Equal(WebAddress, _config.WebsiteAddress);
        }

        [Fact]
        public void ContainValueForPageSizeInSetting()
        {
            Assert.Equal(3, _config.PageSize);
        }

        [Fact]
        public void ContainValueForPageSizeMilestoneInSetting()
        {
            Assert.Equal(10, _config.PageSizeMilestone);
        }

        [Fact]
        public void ContainValueForMsGraphUrlInSetting()
        {
            Assert.Contains("https://", _config.AzureAdAppRegSettings.MsGraphUrl);
        }

        [Fact]
        public void ContainValueFoMsGraphScopeInSetting()
        {
            Assert.Contains("graph", _config.AzureAdAppRegSettings.MsGraphScope);
        }

        [Fact]
        public void ContainValueForWalletSettingsUsername()
        {
            Assert.Equal("1748725034", _jengaPaymentSettings.AccountNumber);
        }

        [Fact]
        public void ContainMomoCollectionSettings()
        {
            Assert.True(_momoPaymentSettings.Collection.SubscriptionKey.IsNotBlank());
            Assert.True(_momoPaymentSettings.BaseUri.IsNotBlank());
            Assert.True(_momoPaymentSettings.Environment.IsNotBlank());
            Assert.True(_momoPaymentSettings.Collection.UserId.IsNotBlank());
            Assert.True(_momoPaymentSettings.Collection.UserSecretKey.IsNotBlank());
        }

        [Fact]
        public void ContainMomoDisbursementSettings()
        {
            Assert.True(_momoPaymentSettings.Disbursement.SubscriptionKey.IsNotBlank());
            Assert.True(_momoPaymentSettings.Disbursement.UserId.IsNotBlank());
            Assert.True(_momoPaymentSettings.Disbursement.UserSecretKey.IsNotBlank());
        }

        [Fact]
        public void ContainKeyVaultSettings()
        {
            Assert.True(_keyVaultSettings.Name.IsNotBlank());
            Assert.True(_keyVaultSettings.KeyName.IsNotBlank());
            Assert.True(_keyVaultSettings.KeyVersion.IsNotBlank());
            Assert.True(_keyVaultSettings.KeyEncryptedValue.IsNotBlank());
            Assert.True(_keyVaultSettings.ProtectionKeyUrl.IsNotBlank());
        }

        [Fact]
        public void ContainWorkerSettings()
        {
            Assert.Equal(5, _workerSettings.NumberOfWorkers);
            Assert.Equal(200, _workerSettings.ThreadSleepMilliseconds);
            Assert.Equal(10, _workerSettings.TestingMRPItemsCount);
            Assert.Equal(20, _workerSettings.TestingMSRPItemsCount);
        }

        [Fact]
        public void ContainAzureAdSettings()
        {
            Assert.Equal("https://login.microsoftonline.com/", _azureAdSettings.Instance);
            Assert.Equal("iforemanrarockwell.onmicrosoft.com", _azureAdSettings.Domain);
            Assert.Equal("f818c762-5d03-4aa8-9be2-8b2a990cfcd2", _azureAdSettings.TenantId);
            Assert.Equal("d8302c4f-7ef1-49ac-92a0-a9c46b875313", _azureAdSettings.ClientId);
            Assert.Equal("/signin-oidc", _azureAdSettings.CallbackPath);

            Assert.Equal("73019068-a239-4fb3-ab6a-9a23acf5f7f4", _azureAdSettings.Groups.Readers);
            Assert.Equal("f565df69-26ea-4efe-84bf-b093e5277da7", _azureAdSettings.Groups.Writers);
            Assert.Equal("10bc8e6b-b0f0-4beb-a7de-7359d764b534", _azureAdSettings.Groups.Admins);
        }

        [Fact]
        public void LogThatFoundConfigFileIfItExists()
        {
            var services = new ServiceCollection();
            var loggerFactory = new LoggerFactory();
            var fileHandler = new Mock<IHandleFiles>();

            fileHandler.Setup(x => x.CheckFilePathExists(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            fileHandler.Setup(x => x.GetContentRootPath()).Returns(_path);
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(_path);

            services.ConfigureDependenciesStandAlone(env.Object);
            var appConfigBuilder = new AppConfigBuilder(env.Object, services);


            // var builder = new AppConfigBuilder(services, fileHandler.Object, loggerFactory);
            var config = appConfigBuilder.Build();

            Assert.True(config.DatabaseConnectionString.IsNotBlank());
        }

        [Fact]
        public void LogThatFileNotFoundIfConfigFileDoesNotExist()
        {
            var services = new ServiceCollection();
            var loggerFactory = new LoggerFactory();
            var fileHandler = new Mock<IHandleFiles>();

            fileHandler.Setup(x => x.CheckFilePathExists(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            fileHandler.Setup(x => x.GetContentRootPath()).Returns(_path);
            var ex = Assert.Throws<ArgumentNullException>(() => new AppConfigBuilder(services, fileHandler.Object, loggerFactory));

            Assert.Contains("appsettings file not found. Configuration values will not have been correctly set", ex.Message);
        }

        [Fact]
        public void LogThatFileNotFoundIfFilePathIsEmpty()
        {
            var services = new ServiceCollection();
            var loggerFactory = new LoggerFactory();
            var fileHandler = new Mock<IHandleFiles>();

            fileHandler.Setup(x => x.CheckFilePathExists(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            fileHandler.Setup(x => x.GetContentRootPath()).Returns(string.Empty);
            var ex = Assert.Throws<ArgumentNullException>(() => new AppConfigBuilder(services, fileHandler.Object, loggerFactory));

            Assert.Contains("appsettings file not found. Configuration values will not have been correctly set", ex.Message);
        }
    }
}
