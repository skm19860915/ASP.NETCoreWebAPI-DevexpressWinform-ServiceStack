using ServiceStack.Configuration;

namespace Xperters.Admin.ServiceInterface
{
	public class ServiceSettings : AppSettings
	{
		public string SqlServerConnectionString { get; set; }
		
		// Add the rest of your settings here
	}
}