using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.GridDefinition;
using Xperters.Admin.UI.Common.LayerProgram;
using xperters.domain;
using System.Linq;
using DevExpress.XtraGrid.Columns;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Xperters.Admin.UI.Tabs.MilestoneAdminApprovals
{
    public partial class PaymentAdminApprovalTabView : XtraUserControl, IPaymentAdminApprovalTabView
    {
        private readonly PaymentAdminApprovalPresenter _presenter;

        public PaymentAdminApprovalTabView()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _presenter = ContainerOperations
                    .Container
                    .Resolve<PaymentAdminApprovalPresenter>(new TypedParameter(typeof(IPaymentAdminApprovalTabView), this));

                ApplicationState.PaymentAdminApprovalPresenter = _presenter;

            }

            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {


            }
        }

        public BindingList<PaymentGridViewModel> PaymentsAwaitingApproval { get; } = new BindingList<PaymentGridViewModel>();


        public void BindGrids()
        {
            var list = PaymentsAwaitingApproval.Select(x => x.Payment).ToList();
            foreach (var item in list)
            {
                var record = new MilestonePaymentViewModel()
                {
                    MilestoneId = item.MilestoneId,
                    MilestoneDescription = item.MilestoneDescription,
                    MilestoneDueDate = item.MilestoneDueDate,
                    MilestoneAmount = item.MilestoneAmount.ToString("C", CultureInfo.CurrentCulture),
                    MilestoneCreated = item.MilestoneCreated,
                    MilestoneStatus = item.MilestoneStatus,
                    JobTitle = item.JobTitle,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    PaymentAmount = item.PaymentAmount,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    RequestPayerCreated = item.RequestPayerCreated,
                    Approve = false
                };
                MilestonePaymentViewModels.Add(record);
            }
            paymentsGridControl.DataSource = MilestonePaymentViewModels;

            for (var i = 1; i < paymentsGridView.Columns.Count; i++)
            {
                var milestonePaymentDto = list.FirstOrDefault();
                paymentsGridView.Columns[i].ToolTip = milestonePaymentDto?.MilestoneId.ToString();
            }

            CustomizeDataGridView();
        }

        private void CustomizeDataGridView()
        {
            paymentsGridView.Columns[0].Visible = false;
            for (var i = 1; i < paymentsGridView.Columns.Count - 1; i++)
            {
                paymentsGridView.Columns[i].OptionsColumn.ReadOnly = true;
            }
        }

        public List<MilestonePaymentDto> Payments { get; private set; } = new List<MilestonePaymentDto>();
        public List<PaymentGridViewModel> PaymentViewModels { get; set; } = new List<PaymentGridViewModel>();

        public List<MilestonePaymentViewModel> MilestonePaymentViewModels { get; set; } = new List<MilestonePaymentViewModel>();


        public IGridDefinitionBuilder GridDefinitionBuilder { get; } = new ProgramGridDefinitionBuilder();


        public void RefreshGrid()
        {
            this.SafelyUpdateUi(() =>
            {

            });
        }

        public void RefreshData()
        {
            this.SafelyUpdateUi(() =>
            {
            });
        }



        public void CloseEditor()
        {
        }

        private async void saveButton_Click(object sender, System.EventArgs e)
        {
            saveButton.Enabled = false;
            paymentsGridView.ShowLoadingPanel();
            var status = await UpdateMilestonePayments();
            MessageBox.Show(status);
            paymentsGridView.HideLoadingPanel();
            saveButton.Enabled = true;
        }

        private async Task<string> UpdateMilestonePayments()
        {
            var gridData = paymentsGridControl.DataSource as List<MilestonePaymentViewModel>;
            var postData = gridData.Where(x => x.Approve);
            if (postData == null || !postData.Any())
                return "Please select one or more row !";

            var countOfUpdatedlist = postData.Count();
            var milestoneIdsFromResponse = await _presenter.SendData(postData);

            if (milestoneIdsFromResponse == null)
                return "Error!!";

            foreach (var milestoneId in milestoneIdsFromResponse)
            {
                var updatedRecord = gridData.FirstOrDefault(x => x.MilestoneId == milestoneId);
                gridData.Remove(updatedRecord);
            }

            paymentsGridControl.DataSource = gridData;
            paymentsGridControl.RefreshDataSource();

            var result = $"Updated {milestoneIdsFromResponse.Count} of {countOfUpdatedlist} !";

            return result;
        }


        private async void refreshButton_Click(object sender, EventArgs e)
        {
            refreshButton.Text = "Refreshing...";
            paymentsGridView.ShowLoadingPanel();

            if (paymentsGridView.DataSource is List<MilestonePaymentViewModel> gridData && gridData.Count > 0)
                gridData.Clear();

            await RefreshMilestonePayments();
            paymentsGridView.HideLoadingPanel();
            refreshButton.Text = "Refresh";
        }

        private async Task RefreshMilestonePayments()
        {
            var data = await _presenter.SendRefreshRequest();

            if (data == null)
            {
                paymentsGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<MilestonePaymentDto>(data);
            LoadListToGridView(list);
        }

        private void LoadListToGridView(BindingList<MilestonePaymentDto> list)
        {
            var data = new List<MilestonePaymentViewModel>();
            foreach (var item in list)
            {
                var record = new MilestonePaymentViewModel()
                {
                    MilestoneId = item.MilestoneId,
                    MilestoneDescription = item.MilestoneDescription,
                    MilestoneDueDate = item.MilestoneDueDate,
                    MilestoneAmount = item.MilestoneAmount.ToString("C", CultureInfo.CurrentCulture),
                    MilestoneCreated = item.MilestoneCreated,
                    MilestoneStatus = item.MilestoneStatus,
                    JobTitle = item.JobTitle,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    PaymentAmount = item.PaymentAmount,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    RequestPayerCreated = item.RequestPayerCreated,
                    Approve = false
                };
                data.Add(record);
            }
            paymentsGridControl.DataSource = data;
        }
    }

    #region temp
    //public partial class PaymentAdminApprovalTabView : XtraUserControl
    //{
    //    public PaymentAdminApprovalTabView()
    //    {
    //        InitializeComponent();
    //    }

    //    private void saveButton_Click(object sender, System.EventArgs e)
    //    {
    //    }

    //    private void refreshButton_Click(object sender, EventArgs e)
    //    {
    //    }
    //}
    #endregion
}
