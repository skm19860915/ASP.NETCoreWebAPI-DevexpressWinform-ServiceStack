using System.Collections.Generic;

namespace Xperters.Admin.ServiceModel
{
	public static class SecurityConstants
	{
		public static readonly int XpertersApiServiceModelVersion = 1;

		public static class UserRoles
		{
			public const string ReadRole = "Xperters.Admin.Read";
			public const string WriteRole = "Xperters.Admin.Write";
			public const string AdminRole = "Xperters.Admin.Admin";

			public static IReadOnlyCollection<string> AllRoles => new[] { ReadRole, WriteRole, AdminRole };
			public static IReadOnlyCollection<string> RolesWithWritePermissions => new[] { WriteRole, AdminRole };
		}

		public static class AzureAd
		{
			public static readonly string AzureAdConfigurationKey = "AzureAd";
		}
	}
}
