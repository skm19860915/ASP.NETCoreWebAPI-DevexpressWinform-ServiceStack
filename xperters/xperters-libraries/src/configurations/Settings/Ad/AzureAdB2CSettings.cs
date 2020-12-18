
namespace xperters.configurations.Settings.Ad
{
    public class AzureAdB2CSettings
    {
        private string _domain;
        private string _graphEndpoint;
        private string _graphRelativePath;
        public const string PolicyAuthenticationProperty = "Policy";


        public string ClientId { get; set; }

        public string AzureAdB2CInstance { get; private set; }

        public string Tenant { get; set; }
        public string GraphUri { get; set; }

        public string GraphRelativePath
        {
            get => _graphRelativePath;
            set
            {
                _graphRelativePath = value;
                _graphEndpoint = $"{GraphUri}{_graphRelativePath}";
            }
        }

        public string GraphEndpoint => _graphEndpoint;

        public string Domain
        {
            get => _domain;
            set
            {
                _domain = value;

                AzureAdB2CInstance = $"https://{_domain}";
            }
        }

        public string SignUpSignInPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string SignUpPolicyId { get; set; }
        public string ResetPasswordPolicyId { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string RedirectUri { get; set; }

        public string DefaultPolicy => SignUpSignInPolicyId;
        public string Authority => $"{AzureAdB2CInstance}/{Tenant}/{DefaultPolicy}/v2.0";

        public string ClientSecret { get; set; }
        public string ApiUrl { get; set; }
        public string ApiScopes { get; set; }
    }
}
