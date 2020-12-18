using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Xperters.Admin.UI.Common
{
	public class UserSessionClientService
	{
		public AuthenticationInfo AuthenticationInfo { get; }

		public UserSessionClientService(AuthenticationInfo authenticationInfo)
		{
			AuthenticationInfo = authenticationInfo ?? throw new ArgumentNullException(nameof(authenticationInfo));
		}

		public UserSession GetUserSession()
		{
			return new UserSession(
				GetEmailAddress(),
				GetDisplayName(),
				GetEmailAddress(),
				UserHasRole(ServiceModel.Constants.UserRoles.AdminRole),
				GetEmailAddress(),
				GetRoles(),
				GetFirstName(),
				GetLastName()
			);
		}

		private bool UserHasRole(string role)
		{
			return GetRoles().Any(o => o.Equals(role, StringComparison.InvariantCultureIgnoreCase));
		}

		private string GetFirstName()
		{
			var token = GetToken();
			return token.Claims
				.FirstOrDefault(o => o.Type.Equals("given_name", StringComparison.InvariantCultureIgnoreCase))
				?.Value;
		}

		private string GetLastName()
		{
			var token = GetToken();
			return token.Claims
				.FirstOrDefault(o => o.Type.Equals("family_name", StringComparison.InvariantCultureIgnoreCase))
				?.Value;
		}

		private string GetDisplayName()
		{
			var token = GetToken();
			return token.Claims
				.FirstOrDefault(o => o.Type.Equals("name", StringComparison.InvariantCultureIgnoreCase))
				?.Value;
		}

		private string GetEmailAddress()
		{
			var token = GetToken();
			return token.Claims
				.FirstOrDefault(o => o.Type.Equals("unique_name", StringComparison.InvariantCultureIgnoreCase))
				?.Value;
		}

		private List<string> GetRoles()
		{
			var token = GetToken();
			return token.Claims
				.Where(o => o.Type.Equals("roles", StringComparison.InvariantCultureIgnoreCase))
				.Select(o => o.Value)
				.ToList();
		}

		private JwtSecurityToken GetToken()
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadToken(AuthenticationInfo.AuthenticationResult.AccessToken) as JwtSecurityToken;
			return token;
		}
	}
}
