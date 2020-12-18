using ServiceStack;

namespace Xperters.Admin.ServiceInterface
{
	[Authenticate]
	public class ServiceBase : Service
	{
		public AuthUserSession User => SessionAs<AuthUserSession>();
		public T ResolveServiceWithEnrichment<T>() where T : ServiceBase
		{
			var service = HostContext.Resolve<T>();

			service.Request = Request;

			return service;
		}
	}
}