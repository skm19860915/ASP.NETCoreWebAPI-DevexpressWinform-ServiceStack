using System.Collections.Generic;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IWithdrawalsManager
    {
        IEnumerable<PaymentOutgoingDto> GetPaymentOutgoingForWithdrawals();
    }
}
