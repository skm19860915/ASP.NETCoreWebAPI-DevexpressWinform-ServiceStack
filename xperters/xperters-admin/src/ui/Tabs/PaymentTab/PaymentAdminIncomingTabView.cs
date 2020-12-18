using Autofac;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public partial class PaymentAdminIncomingTabView : XtraUserControl, IPaymentAdminIncomingTabView
    {
        private readonly PaymentIncomingPresenter _presenter;

        public PaymentAdminIncomingTabView()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _presenter = ContainerOperations
                    .Container
                    .Resolve<PaymentIncomingPresenter>(new TypedParameter(typeof(IPaymentAdminIncomingTabView), this));

                ApplicationState.PaymentIncomingPresenter = _presenter;
            }
            InitializeComponent();
            paymentIncomingGridView.OptionsBehavior.Editable = false;
        }

        public BindingList<PaymentIncomingDto> PaymentIncomingList { get; } = new BindingList<PaymentIncomingDto>();

        public void BindGrids()
        {
            LoadListToGridView(PaymentIncomingList);
        }

        private void LoadListToGridView(BindingList<PaymentIncomingDto> list)
        {
            var data = new List<PaymentIncomingViewModel>();
            foreach (var item in list)
            {
                var record = new PaymentIncomingViewModel()
                {
                    MilestoneDescription = item.MilestoneDescription,
                    UserName = item.UserName,
                    Currency = item.Currency,
                    PayerStatus = item.PayerStatus,
                    ResponseMessage = item.ResponseMessage,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    CompletedDate = item.CompletedDate,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    Amount = item.Amount,
                    FeeFlat = item.FeeFlat,
                    FeePercent = item.FeePercent,
                    FeeTotal = item.FeeTotal,
                    TotalAmount = item.TotalAmount,
                    CreateDate = item.Created,
                    Id = item.Id
                };
                data.Add(record);
            }
            paymentIncomingGridControl.DataSource = data;
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            refreshButton.Text = "Refreshing...";
            paymentIncomingGridView.ShowLoadingPanel();
            if (paymentIncomingGridView.DataSource is List<PaymentIncomingViewModel> gridData && gridData.Count > 0)
                gridData.Clear();

            await UpdatePaymentIncomingList();
            paymentIncomingGridView.HideLoadingPanel();
            refreshButton.Text = "Refresh";
        }

        private async Task UpdatePaymentIncomingList()
        {
            var data = await _presenter.SendRefreshRequest();

            if (data == null)
            {
                paymentIncomingGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<PaymentIncomingDto>(data);
            LoadListToGridView(list);
        }
    }
}
