using System.ComponentModel;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public interface IPaymentAdminWithdrawalsTabView : IView
    {
        void BindGrids();
        BindingList<PaymentOutgoingDto> PaymentOutgoingList { get; }
    }
}
