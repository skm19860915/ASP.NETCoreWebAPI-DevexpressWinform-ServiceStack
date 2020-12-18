using System.Threading.Tasks;
using ServiceStack;
using Xperters.Authentication;
using Xperters.Authentication.Native;

namespace Xperters.Admin.ServiceInterface
{
	public abstract class ServiceHandler
	{
		private readonly AzureAdOptions _config;
		private readonly AzureAdExternalOptions _target;

		protected ServiceHandler(AzureAdOptions config, AzureAdExternalOptions target)
		{
			_config = config;
			_target = target;
		}

		protected async Task<ServiceClientBase> ClientAsync()
		{
			return await _config.GetClientForAsync(_target);
		}
	}
}