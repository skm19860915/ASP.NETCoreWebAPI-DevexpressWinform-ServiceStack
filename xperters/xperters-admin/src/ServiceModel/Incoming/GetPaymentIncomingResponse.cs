using ServiceStack;
using System.Collections.Generic;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Incoming
{
    public sealed class GetPaymentIncomingResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<PaymentIncomingDto> PaymentIncoming { get; set; }
    }
}
