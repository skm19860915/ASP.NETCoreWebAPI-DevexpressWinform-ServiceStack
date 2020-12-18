using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Milestones;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.MilestoneAdminApprovals.ServiceClient
{
	public class MilestonesServiceClient : IMilestonesServiceClient
	{
		private readonly IXpertersAdminServiceClient _serviceClient;

		public MilestonesServiceClient(IXpertersAdminServiceClient serviceClient)
		{
			_serviceClient = serviceClient ?? throw new System.ArgumentNullException(nameof(serviceClient));
		}


		public async Task<GetPaymentsForAdminApprovalResponse> GetAsync(GetPaymentsForAdminApprovalRequest request)
		{
			return await _serviceClient.GetAsync(request);
		}

        public async Task<PostPaymentsForAdminApprovalResponse> PostAsync(PostPaymentsForAdminApprovalRequest request)
        {
			return await _serviceClient.PostAsync(request);
        }
    }
}
