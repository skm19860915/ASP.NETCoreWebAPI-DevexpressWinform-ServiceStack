using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Incoming
{
    [Tag(Constants.Constants.Incoming)]
    [Route("/payments/incoming", RequestTypeConstants.Get)]
    public class GetPaymentIncomingRequest : IRequest<GetPaymentIncomingResponse>
    {
        public int Version { get; set; }
    }
}
