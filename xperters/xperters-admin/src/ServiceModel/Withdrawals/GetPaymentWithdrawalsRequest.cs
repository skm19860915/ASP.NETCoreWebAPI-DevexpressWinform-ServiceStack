using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Withdrawals
{
    [Tag(Constants.Constants.Withdrawals)]
    [Route("/payments/withdrawals", RequestTypeConstants.Get)]
    public class GetPaymentWithdrawalsRequest : IRequest<GetPaymentWithdrawalsResponse>
    {
        public int Version { get; set; }
    }
}
