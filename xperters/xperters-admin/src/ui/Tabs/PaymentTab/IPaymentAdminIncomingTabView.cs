using System.ComponentModel;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public interface IPaymentAdminIncomingTabView : IView
    {
        void BindGrids();
        BindingList<PaymentIncomingDto> PaymentIncomingList { get; }
    }
}
