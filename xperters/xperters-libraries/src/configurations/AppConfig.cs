using System.Collections;
using System.Text;
using xperters.configurations.Interfaces;
using xperters.configurations.Settings;
using xperters.configurations.Settings.Ad;
using xperters.configurations.Settings.Email;
using xperters.configurations.Settings.MobilePhoneService;
using xperters.configurations.Settings.Payments;
using xperters.constants;

namespace xperters.configurations
{
    public class AppConfig
    {
        public AppConfig()
        {
            PaymentSettings = new PaymentSettings();
            EmailSettings = new EmailSettings();
            Storage = new StorageAccountSettings();
            ServiceBus = new ServiceBusSettings();
        }

        public bool GetAppIsRunningInContainer(IHandleEnvironment environmentHandler) {

            bool.TryParse(environmentHandler.GetVariable(XpertersEnvVariables.DotnetRunningInContainer), out var dotnetRunningInContainer);
            return dotnetRunningInContainer;
        }

        public string DatabaseConnectionString { get; set; }
        public string Environment { get; set; }

        public bool MockUserData { get; set; }
        public bool MockConnections { get; set; }
        public string HostingLocation { get; set; }
        public string WebsiteAddress { get; set; }
        public string ApplicationInsightsKey { get; set; }

        public StorageAccountSettings Storage { get; set; }
        public ServiceBusSettings ServiceBus { get; set; }
        public WorkerSettings WorkerSettings { get; set; }
        public AzureAdSettings AzureAdSettings { get; set; }

        public int PageSize { get; set; }
        public int PageSizeMilestone { get; set; }
        public PaymentSettings PaymentSettings { get; set; }
        public MomoPaymentSettings MomoPaymentSettings { get; set; }

        public AzureAdAppRegSettings AzureAdAppRegSettings { get; set; }
        public AzureAdB2CSettings AzureAdB2CSettings { get; set; }
        public KeyVaultSettings KeyVaultSettings { get; set; }
        public string EnvironmentVariables => GetEnvironmentVariables();

        private string GetEnvironmentVariables()
        {
            var variables = System.Environment.GetEnvironmentVariables();

            var builder = new StringBuilder();
            foreach (DictionaryEntry variable in variables)
            {
                builder.Append( $"Key {variable.Key} Value {variable.Value} <br>\n\r");
            }

            return builder.ToString();
        }

        public EmailSettings EmailSettings { get; set; }
        public MobilePhoneServiceSettings MobilePhoneServiceSettings { get; set; }
    }
}
