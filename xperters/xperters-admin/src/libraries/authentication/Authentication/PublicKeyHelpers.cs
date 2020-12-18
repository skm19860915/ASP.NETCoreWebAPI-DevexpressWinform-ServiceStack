using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServiceStack;

namespace Xperters.Authentication
{
	public static class PublicKeyHelpers
	{
		public static RSAParameters[] GetPublicKeysForTenant(string tenant)
		{
			var publicKeysUrl = $"https://login.microsoftonline.com/{tenant}/discovery/v2.0/keys";
			return FromUrlAsync(publicKeysUrl).Result;
		}

		internal static async Task<RSAParameters[]> FromUrlAsync(string publicKeysUrl)
		{
			var certs = await GetX509CertificatesAsync(publicKeysUrl);
			return Array.ConvertAll(certs, X509ToRSAXML);
		}

		private static async Task<string[]> GetX509CertificatesAsync(string publicKeysUrl)
		{
			var jsonS = await publicKeysUrl.GetJsonFromUrlAsync();
			var jo = JObject.Parse(jsonS);
			return jo["keys"].Select(k => (string) k["x5c"][0]).ToArray();
		}

		// ReSharper disable once InconsistentNaming
		private static RSAParameters X509ToRSAXML(string x509)
		{
			var cert = new X509Certificate2(Convert.FromBase64String(x509));
			var rsa = cert.GetRSAPublicKey();
			return rsa.ExportParameters(false);
		}
	}
}