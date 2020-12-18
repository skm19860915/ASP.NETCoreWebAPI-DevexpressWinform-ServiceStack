using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Jobs;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.JobTab.ServiceClient
{
    public class JobsServiceClient : IJobsServiceClient
    {
        private readonly IXpertersAdminServiceClient _serviceClient;
        public JobsServiceClient(IXpertersAdminServiceClient serviceClient)
        {
            _serviceClient = serviceClient ?? throw new System.ArgumentNullException(nameof(serviceClient));
        }

        public async Task<GetJobInformationForAdminResponse> GetAsync(GetJobInformationForAdminRequest request)
        {
            return await _serviceClient.GetAsync(request);
        }

        public async Task<GetJobInformationForAdminResponse> PostAsync(PostParamsForFilteredJobInformationRequest request)
        {
            return await _serviceClient.PostAsync(request);
        }
    }
}
