using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Xperters.Authentication.Native
{
	public static class AzureAuthentication
	{
		public static async Task<string> AuthenticateNativeClient(AzureAdNativeOptions configuration)
		{
			var bearerToken = await AuthenticateNativeClientAuthenticationResult(configuration);
			return bearerToken.AccessToken;
		}

		public static async Task<AuthenticationResult> AuthenticateNativeClientAuthenticationResult(AzureAdNativeOptions configuration)
	    {
		    if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		    var authContext = new AuthenticationContext(configuration.Authority, new FileTokenCache(configuration.Env));

		    AuthenticationResult authResult = null;

		    var retryCount = 0;
		    bool retry;

		    do
		    {
			    retry = false;
			    try
			    {
				    authResult = await authContext
					    .AcquireTokenSilentAsync(configuration.Resource, configuration.ClientId)
					    .ConfigureAwait(true);
			    }
			    catch (AdalException ex)
			    {
				    if (ex.ErrorCode == "temporarily_unavailable")
				    {
					    retry = true;
					    retryCount++;
					    await Task.Delay(500).ConfigureAwait(false);
				    }
				    else if (ex.ErrorCode == AdalError.UserInteractionRequired || ex.ErrorCode == AdalError.FailedToAcquireTokenSilently)
				    {
					    authResult = await authContext.AcquireTokenAsync(configuration.Resource,
							    configuration.ClientId, new Uri(configuration.RedirectUri),
							    new PlatformParameters(PromptBehavior.Always))
						    .ConfigureAwait(true);
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

		public static async Task<string> AuthenticateClientCredentialAsync(AzureAdOptions configuration)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var authContext = new AuthenticationContext(configuration.Authority);
			var clientCredential = new ClientCredential(configuration.ClientId, configuration.ClientSecret);

			AuthenticationResult authResult = null;

			var retryCount = 0;
			bool retry;

			do
			{
				retry = false;
				try
				{
					authResult = await authContext
						.AcquireTokenAsync(configuration.Resource, clientCredential)
						.ConfigureAwait(continueOnCapturedContext: false);
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

			if (authResult != null) return authResult.AccessToken;
			throw new Exception("Authentication failed");
		}
	}
}