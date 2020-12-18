using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Auth;
using Xperters.Authentication.Native;

namespace Xperters.Authentication
{
	public static class JWTProviderExtension
	{
		public static JwtAuthProviderReader GetJWTProviderReader(this AzureAdOptions settings, bool debugMode = false, string hashAlgorithm = "RS256")
		{
			return settings.GetJWTProviderReaderAsync(debugMode, hashAlgorithm).Result;
		}
		public static async Task<JwtAuthProviderReader> GetJWTProviderReaderAsync(this AzureAdOptions settings, bool debugMode = false, string hashAlgorithm = "RS256")
		{
			// This will only work for Microsoft Azure tokens
			var publicKeysUrl = settings.Authority + "/discovery/v2.0/keys";
			var publicKeys = await PublicKeyHelpers.FromUrlAsync(publicKeysUrl).ConfigureAwait(continueOnCapturedContext: false);

			return new JwtAuthProviderReader()
			{
				RequireSecureConnection = !debugMode,
				HashAlgorithm = hashAlgorithm,
				PublicKeyXml = publicKeys[0].ToPublicKeyXml(),
				FallbackPublicKeys = publicKeys.ToList(),
				Audiences = new List<string> { settings.Resource, settings.ClientId },
				Issuer = settings.Instance,
				PopulateSessionFilter = (session, payload, req) =>
				{
					if (string.IsNullOrEmpty(session.UserName))
					{
						if (payload.ContainsKey("appid"))
						{
							session.SetUserName(payload["appid"]);
						}
						else if (payload.ContainsKey("upn") && payload["upn"].Contains("@"))
						{
							session.SetUserName(payload["upn"]);
						}
					}
					if (session.Email == null && payload.ContainsKey("upn") && payload["upn"].Contains("@"))
						session.Email = payload["upn"];

				}
			};
		}
	}
}
