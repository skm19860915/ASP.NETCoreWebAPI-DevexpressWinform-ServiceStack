using System;

namespace xperters.configurations.Settings.Ad
{
    public class AzureAdSettings
    {
        public string Instance { get;  }
        public string TenantId { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string CallbackPath { get;}
        public GroupSettings Groups { get; set; }

        public AzureAdSettings()
        {
            Instance = "https://login.microsoftonline.com/";
            CallbackPath = "/signin-oidc";
            Groups = new GroupSettings();

            ClientId = string.Empty;
            Domain= string.Empty;
            TenantId = string.Empty;

        }
    }
}
