namespace xperters.configurations.Settings.Ad
{
    public class AzureAdAppRegSettings
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string MsOnlineTokenUrl { get; set; }
        public string MsGraphScope { get; set; }
        public string MsGraphUrl { get; set; }
        public string MsGraphNetUrl { get; set; }
        public string MsGraphApiVersion { get; set; }
        public string ResourceApiIamAzureAd { get; set; }
        public string TenantIdSql { get; set; }
        public string ClientIdSql { get; set; }
        public string ClientSecretSql { get; set; }
    }

}
