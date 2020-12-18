using System;

namespace Xperters.Authentication.Native
{
	public class AzureAdNativeOptions
	{
		public string Authority { get; }
		public string ClientId { get; }
		public string Resource { get; }
		public string RedirectUri { get; }
		public string Env { get; }

		public AzureAdNativeOptions(string authority, string clientId, string resource, string redirectUri, string env = "")
		{
			if (string.IsNullOrWhiteSpace(authority))
				throw new ArgumentException("Value cannot be null or empty.", nameof(authority));
			if (string.IsNullOrWhiteSpace(clientId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(clientId));
			if (string.IsNullOrWhiteSpace(resource))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(resource));
			if (string.IsNullOrWhiteSpace(redirectUri))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(redirectUri));
			Authority = authority;
			ClientId = clientId;
			Resource = resource;
			RedirectUri = redirectUri;
			Env = env;
		}
	}
}