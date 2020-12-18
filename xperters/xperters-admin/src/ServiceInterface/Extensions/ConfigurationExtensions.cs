using Microsoft.Extensions.Configuration;

namespace Xperters.Admin.ServiceInterface.Extensions
{
	public static class ConfigurationExtensions
	{
		public static T BindConfig<T>(this IConfiguration configuration, string key) where T : new()
		{
			T binding = new T();
			configuration.Bind(key, binding);
			return binding;
		}
	}
}
