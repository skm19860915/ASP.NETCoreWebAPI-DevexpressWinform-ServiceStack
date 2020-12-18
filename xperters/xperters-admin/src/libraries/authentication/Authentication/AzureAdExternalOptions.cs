namespace Xperters.Authentication
{
	public class AzureAdExternalOptions
	{
		/// <summary>
		/// Resource: Resource URI of the External Application registration, 
		/// also called ResourceId. e.g. https://domain.com/xxxxxxx-0000-0000-0000-xxxxxxxxxx
		/// </summary>
		public string Resource { get; set; }
		/// <summary>
		/// ApiBaseUrl: Base Service URI of the External Application to connect to
		/// </summary>
		public string ApiBaseUrl { get; set; }
	}
}