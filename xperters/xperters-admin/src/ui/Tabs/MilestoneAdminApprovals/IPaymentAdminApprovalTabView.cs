using System.Collections.Generic;
using System.ComponentModel;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.GridDefinition;
using Xperters.Admin.UI.Common.LayerProgram;
using xperters.domain;

namespace Xperters.Admin.UI.Tabs.MilestoneAdminApprovals
{
	public interface IPaymentAdminApprovalTabView : IView, IEnrichableGridView
	{

        BindingList<PaymentGridViewModel> PaymentsAwaitingApproval { get; }

		void RefreshGrid();

		void RefreshData();

		void CloseEditor();
        void BindGrids();

        List<MilestonePaymentDto> Payments { get; }
        List<PaymentGridViewModel> PaymentViewModels { get; set; }
	}
}
