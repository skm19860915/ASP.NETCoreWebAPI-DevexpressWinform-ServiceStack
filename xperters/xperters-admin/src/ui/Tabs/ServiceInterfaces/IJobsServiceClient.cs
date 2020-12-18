using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Jobs;

namespace Xperters.Admin.UI.Tabs.ServiceInterfaces
{
    public interface IJobsServiceClient
    {
        Task<GetJobInformationForAdminResponse> GetAsync(GetJobInformationForAdminRequest request);
        Task<GetJobInformationForAdminResponse> PostAsync(PostParamsForFilteredJobInformationRequest request);
    }
}
