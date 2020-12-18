namespace Xperters.Authentication.Native
{
	public class AzureAdOptions
	{
		/// <summary>
		/// ClientId: Also called Applicaiotn Id. Should refer to the calling Application Registration Id. Usually a guid
		/// </summary>
		public string ClientId { get; set; }
		/// <summary>
		/// ClientSecret: Secret of the Calling Application registration, also called AppKey
		/// Is optional if the Application does not need to connect to an other application 
		/// </summary>
		public string ClientSecret { get; set; }
		/// <summary>
		/// Resource: URI of the Calling Application, also called ResourceId. e.g. https://domain.com/xxxxxxx-0000-0000-0000-xxxxxxxxxx
		/// </summary>
		public string Resource { get; set; }
		/// <summary>
		/// CallbackPath: Relative path to the callback service uri. e.g.: "/signin", "/signin-oidc".
		/// </summary>
		public string CallbackPath { get; set; }
		/// <summary>
		/// Instance: OAuth provider base URL, default: "https://login.microsoftonline.com/"
		/// </summary>
		public string Instance { get; set; } = "https://login.microsoftonline.com/";
		/// <summary>
		/// TenantId: AAD account, usually a guid
		/// </summary>
		public string TenantId { get; set; }
		/// <summary>
		/// Authority: "{Instance}{TenantId}"
		/// </summary>
		public string Authority => $"{Instance}{TenantId}";
	}
}