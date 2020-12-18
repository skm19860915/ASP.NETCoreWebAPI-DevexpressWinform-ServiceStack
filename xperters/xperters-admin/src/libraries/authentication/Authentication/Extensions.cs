using ServiceStack.Auth;

namespace Xperters.Authentication
{
	internal static class Extensions
	{
		internal static string SetUserName(this IAuthSession session, string value)
		{
			if (string.IsNullOrEmpty(session.UserName))
			{
				session.UserName = value;
			}
			return session.UserName;
		}
	}
}