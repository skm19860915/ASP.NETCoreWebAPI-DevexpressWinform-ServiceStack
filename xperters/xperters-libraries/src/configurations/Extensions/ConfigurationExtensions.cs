using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using xperters.configurations.Settings;
using xperters.configurations.Settings.Ad;
using xperters.configurations.Settings.Email;
using xperters.configurations.Settings.MobilePhoneService;
using xperters.configurations.Settings.Payments;

namespace xperters.configurations.Extensions
{
    public static class ConfigurationExtensions
    {
        public static AppConfig SetupConfigObject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<AppConfig>(configuration.GetSection("Database"))
                    .Configure<AppConfig>(configuration.GetSection("Settings"));

            var appConfig = services.BuildServiceProvider().GetService<IOptions<AppConfig>>().Value;

            var storageSettings = new StorageAccountSettings();
            configuration.GetSection("Storage").Bind(storageSettings);
            appConfig.Storage = storageSettings;

            var serviceBusSettings = new ServiceBusSettings();
            configuration.GetSection("ServiceBus").Bind(serviceBusSettings);
            appConfig.ServiceBus = serviceBusSettings;

            var jengaSettings = new JengaSettings();
            configuration.GetSection("PaymentSettings:JengaSettings").Bind(jengaSettings);
            appConfig.PaymentSettings.Jenga = jengaSettings;

            var stripeSettings = new StripeSettings();
            configuration.GetSection("PaymentSettings:StripeSettings").Bind(stripeSettings);
            appConfig.PaymentSettings.Stripe = stripeSettings;

            var settingsAppReg = new AzureAdAppRegSettings();
            configuration.GetSection("Authentication:AzureAdAppReg").Bind(settingsAppReg);
            appConfig.AzureAdAppRegSettings = settingsAppReg;

            var settingsB2C = new AzureAdB2CSettings();
            configuration.GetSection("Authentication:AzureAdB2C").Bind(settingsB2C);
            appConfig.AzureAdB2CSettings = settingsB2C;

            var settingsAd = new AzureAdSettings();
            var settingsAdGroup = new GroupSettings();
            configuration.GetSection("Authentication:AzureAd").Bind(settingsAd);
            configuration.GetSection("Authentication:AzureAd:Groups").Bind(settingsAdGroup);
            settingsAd.Groups = settingsAdGroup;
            appConfig.AzureAdSettings = settingsAd;

            var emailSettings = new EmailSettings();
            configuration.GetSection("EmailSettings:SendGrid").Bind(emailSettings);
            appConfig.EmailSettings= emailSettings;

            var settingsPaymentMomo = new MomoPaymentSettings();
            configuration.GetSection("PaymentSettings:MomoSettings").Bind(settingsPaymentMomo);
            appConfig.MomoPaymentSettings = settingsPaymentMomo;

            var settingsKeyVault = new KeyVaultSettings();
            configuration.GetSection("KeyVault").Bind(settingsKeyVault);
            appConfig.KeyVaultSettings = settingsKeyVault;
            
            var settingsWorker = new WorkerSettings();
            configuration.GetSection("WorkerSettings").Bind(settingsWorker);
            appConfig.WorkerSettings = settingsWorker;

            var mobilePhoneServiceSettings = new MobilePhoneServiceSettings();
            configuration.GetSection("MobilePhoneServiceSettings").Bind(mobilePhoneServiceSettings);
            appConfig.MobilePhoneServiceSettings = mobilePhoneServiceSettings;

            appConfig.ApplicationInsightsKey = configuration["ApplicationInsights:InstrumentationKey"];
            return appConfig;
        }
    }
}
