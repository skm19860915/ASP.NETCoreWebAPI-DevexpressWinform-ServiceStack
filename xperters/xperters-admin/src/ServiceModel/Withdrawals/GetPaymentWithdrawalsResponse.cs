using ServiceStack;
using System.Collections.Generic;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Withdrawals
{
    public sealed class GetPaymentWithdrawalsResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<PaymentOutgoingDto> PaymentOutgoingForWithdrawals { get; set; }
    }
}
