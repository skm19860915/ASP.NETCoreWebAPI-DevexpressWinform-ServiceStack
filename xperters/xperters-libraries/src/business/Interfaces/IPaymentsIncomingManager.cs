using System.Collections.Generic;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IPaymentsIncomingManager
    {
        IEnumerable<PaymentIncomingDto> GetPaymentIncoming();
    }
}
