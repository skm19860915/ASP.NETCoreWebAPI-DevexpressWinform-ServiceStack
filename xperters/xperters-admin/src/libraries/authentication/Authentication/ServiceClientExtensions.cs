using System.Threading.Tasks;
using ServiceStack;
using Xperters.Authentication.Native;

namespace Xperters.Authentication
{
	public static class ServiceClientExtensions
	{
		public static ServiceClientBase GetClientFor(this AzureAdOptions options, AzureAdExternalOptions targetOptions)
		{
			return options.GetClientForAsync(targetOptions).GetAwaiter().GetResult();
		}

		public static async Task<ServiceClientBase> GetClientForAsync(this AzureAdOptions options, AzureAdExternalOptions targetOptions)
		{
			var client = new JsonServiceClient(targetOptions.ApiBaseUrl);
			return await client.WithAzureAdBearerTokenAsync(options, targetOptions).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static ServiceClientBase WithAzureAdBearerToken(this ServiceClientBase sc,
				AzureAdOptions options, AzureAdExternalOptions targetOptions)
		{
			return sc.WithAzureAdBearerTokenAsync(options, targetOptions).GetAwaiter().GetResult();
		}

		public static async Task<ServiceClientBase> WithAzureAdBearerTokenAsync(this ServiceClientBase sc,
				AzureAdOptions options, AzureAdExternalOptions targetOptions)
		{
			var bearerToken = await AzureAuthentication.AuthenticateClientCredentialAsync(options, targetOptions)
				.ConfigureAwait(continueOnCapturedContext: false);
			sc.BearerToken = bearerToken;
			return sc;
		}

		/// <summary>
		/// Returns a ServiceClientBase that can make calls on behalf of a user. Will authenticate the user first if
		/// they are unauthenticated
		/// </summary>
		/// <param name="sc">A JsonServiceClient or any ServiceStack client that inherits from ServiceClientBase</param>
		/// <param name="options">The current executing applications Azure AD configuration options</param>
		/// <param name="targetOptions">The Azure AD configuration options for the API to be called</param>
		/// <param name="username">Username or Email address of the user to make the call on behalf of</param>
		/// <param name="idToken">The original id token received on first auth</param>
		/// <returns></returns>
		public static async Task<ServiceClientBase> WithAzureOnBehalfOfTokenAsync(this ServiceClientBase sc,
			AzureAdOptions options, AzureAdExternalOptions targetOptions, string username, string idToken)
		{
			var onbehalf = await AzureAuthentication.AuthenticateOnBehalfOfAsync(options, targetOptions, username, idToken);
			sc.BearerToken = onbehalf;
			return sc;
		}

		/// <summary>
		/// Returns a ServiceClientBase that can make calls on behalf of a user. Will authenticate the user first if
		/// they are unauthenticated
		/// </summary>
		/// <param name="sc">A JsonServiceClient or any ServiceStack client that inherits from ServiceClientBase</param>
		/// <param name="options">The current executing applications Azure AD configuration options</param>
		/// <param name="targetOptions">The Azure AD configuration options for the API to be called</param>
		/// <param name="username">Username or Email address of the user to make the call on behalf of</param>
		/// <param name="idToken">The original id token received on first auth</param>
		/// <returns></returns>
		public static ServiceClientBase WithAzureOnBehalfOfToken(this ServiceClientBase sc,
			AzureAdOptions options, AzureAdExternalOptions targetOptions, string username, string idToken)
		{
			return sc.WithAzureOnBehalfOfTokenAsync(options, targetOptions, username, idToken).GetAwaiter().GetResult();
		}
	}
}