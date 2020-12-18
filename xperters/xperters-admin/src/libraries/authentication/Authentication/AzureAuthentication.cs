using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xperters.Authentication.Native;

namespace Xperters.Authentication
{
    public static class AzureAuthentication
    {
		public static string AuthenticateClientCredential(AzureAdOptions configuration,
			AzureAdExternalOptions target) => Authenticate(configuration, target).GetAwaiter().GetResult().AccessToken;

		public static async Task<string> AuthenticateClientCredentialAsync(AzureAdOptions configuration,
			AzureAdExternalOptions target) => (await Authenticate(configuration, target)).AccessToken;

		public static async Task<AuthenticationResult> AuthenticateClientCredentialResultAsync(AzureAdOptions configuration,
			AzureAdExternalOptions target) => await Authenticate(configuration, target);

        // Hide for now
        [EditorBrowsable(EditorBrowsableState.Never)]
		public static async Task<string> AuthenticateOnBehalfOfAsync(AzureAdOptions configuration,
			AzureAdExternalOptions target, string email, string idToken)
		{
			if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
			if (string.IsNullOrEmpty(idToken)) throw new ArgumentNullException(nameof(idToken));

			const string assertionType = "urn:ietf:params:oauth:grant-type:jwt-bearer";
			var userAssertion = new UserAssertion(idToken, assertionType, email);

			var authResult = await Authenticate(configuration, target, userAssertion);
			return authResult.AccessToken;
		}

		private static async Task<AuthenticationResult> Authenticate(AzureAdOptions configuration,
			AzureAdExternalOptions target, UserAssertion userAssertion=null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));
			if (target == null) throw new ArgumentNullException(nameof(target));

			var authContext = new AuthenticationContext(configuration.Authority);
			var clientCredential = new ClientCredential(configuration.ClientId,
				configuration.ClientSecret);
			AuthenticationResult authResult = null;
			var retryCount = 0;
			var retry = false;

			do
			{
				try
				{
					if(userAssertion == null)
					{
						authResult = await authContext
						.AcquireTokenAsync(target.Resource, clientCredential)
						.ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						authResult = await authContext
						.AcquireTokenAsync(target.Resource, clientCredential, userAssertion)
						.ConfigureAwait(continueOnCapturedContext: false);
					}
				}
				catch (AdalException ex)
				{
					if (ex.ErrorCode == "temporarily_unavailable")
					{
						retry = true;
						retryCount++;
						await Task.Delay(500).ConfigureAwait(false);
					}
					else
					{
						throw;
					}
				}
			} while (retry && retryCount < 3);

			if (authResult != null) return authResult;
			throw new Exception("Authentication failed");
		}
	}
}