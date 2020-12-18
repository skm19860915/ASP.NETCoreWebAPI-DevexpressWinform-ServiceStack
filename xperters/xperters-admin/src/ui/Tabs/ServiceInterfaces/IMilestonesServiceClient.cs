using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Milestones;

namespace Xperters.Admin.UI.Tabs.ServiceInterfaces
{
    public interface IMilestonesServiceClient
    {
        Task<GetPaymentsForAdminApprovalResponse> GetAsync(GetPaymentsForAdminApprovalRequest request);
        Task<PostPaymentsForAdminApprovalResponse> PostAsync(PostPaymentsForAdminApprovalRequest request);
    }
}
