using System;
using xperters.domain;

namespace Xperters.Admin.UI.Common.LayerProgram
{
    public static class PaymentMappingExtensions
    {
        public static PaymentGridViewModel ToViewModel(this MilestonePaymentDto payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            var vm = new PaymentGridViewModel(payment);
            return vm;
        }
    }
}
