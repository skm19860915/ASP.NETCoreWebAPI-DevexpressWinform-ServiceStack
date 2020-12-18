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
    public partial class PaymentAdminWithdrawalsTabView : XtraUserControl, IPaymentAdminWithdrawalsTabView
    {
        private PaymentWithdrawalsPresenter _presenter;

        public PaymentAdminWithdrawalsTabView()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _presenter = ContainerOperations
                    .Container
                    .Resolve<PaymentWithdrawalsPresenter>(new TypedParameter(typeof(IPaymentAdminWithdrawalsTabView), this));

                ApplicationState.PaymentWithdrawalsPresenter = _presenter;
            }
            InitializeComponent();
            paymentWithdrawalsGridView.OptionsBehavior.Editable = false;
        }

        public BindingList<PaymentOutgoingDto> PaymentOutgoingList { get; } = new BindingList<PaymentOutgoingDto>();

        public void BindGrids()
        {
            LoadListToGridView(PaymentOutgoingList);
        }

        private void LoadListToGridView(BindingList<PaymentOutgoingDto> list)
        {
            var data = new List<PaymentOutgoingViewModel>();
            foreach (var item in list)
            {
                var record = new PaymentOutgoingViewModel()
                {
                    UserName = item.UserName,
                    Amount = item.Amount,
                    BalanceOld = item.BalanceOld,
                    BalanceNew = item.BalanceNew,
                    Currency = item.Currency,
                    PayerStatus = item.PayerStatus,
                    ResponseMessage = item.ResponseMessage,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    CompletedDate = item.CompletedDate,
                    PaymentTransactionType = item.PaymentTransactionType,
                    CreateDate = item.Created,
                    Id = item.Id
                };
                data.Add(record);
            }
            paymentWithdrawalsGridControl.DataSource = data;
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            refreshButton.Text = "Refreshing...";
            paymentWithdrawalsGridView.ShowLoadingPanel();
            if (paymentWithdrawalsGridView.DataSource is List<PaymentOutgoingViewModel> gridData && gridData.Count > 0)
                gridData.Clear();

            await UpdatePaymentWithdrawalsList();
            paymentWithdrawalsGridView.HideLoadingPanel();
            refreshButton.Text = "Refresh";
        }

        private async Task UpdatePaymentWithdrawalsList()
        {
            var data = await _presenter.SendRefreshRequest();

            if (data == null)
            {
                paymentWithdrawalsGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<PaymentOutgoingDto>(data);
            LoadListToGridView(list);
        }
    }

    #region temp
    //public partial class PaymentAdminWithdrawalsTabView : XtraUserControl
    //{
    //    public PaymentAdminWithdrawalsTabView()
    //    {
    //        InitializeComponent();
    //    }

    //    private void refreshButton_Click(object sender, System.EventArgs e)
    //    {
    //    }
    //}
    #endregion
}
