using System.Collections.Generic;

namespace Xperters.Admin.UI.Common
{
	public class UserSession
	{
		public string UserId { get; }

		public string DisplayName { get; }

		public string UserName { get; }

		public bool IsAdmin { get; }

		public string EmailAddress { get; }
		public List<string> UserRoles { get; }
		public string FirstName { get; }

		public UserSession(string userId, string displayName, string userName, bool isAdmin, string emailAddress, List<string> userRoles, string firstName, string lastName)
		{
			UserId = userId;
			DisplayName = displayName;
			UserName = userName;
			IsAdmin = isAdmin;
			EmailAddress = emailAddress;
			UserRoles = userRoles;
			FirstName = firstName;
			LastName = lastName;
		}

		public string LastName { get; set; }

	}
}
