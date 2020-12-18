using Autofac;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using xperters.domain;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.UserTab;

namespace Xperters.Admin.UI.Tabs.JobTab
{
    public partial class JobTabView : DevExpress.XtraEditors.XtraUserControl, IJobTabView
    {
        private JobInformationPresenter _presenter;

        public JobTabView()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _presenter = ContainerOperations
                    .Container
                    .Resolve<JobInformationPresenter>(new TypedParameter(typeof(IJobTabView), this));

                ApplicationState.JobInformationPresenter = _presenter;
            }

            InitializeComponent();
            jobGridView.OptionsBehavior.Editable = false;

            UserTabView.changeTabActivationEvent2 += new EventHandler<RowCellClickEventArgs>(changeGridList);
        }

        private async void changeGridList(object sender, RowCellClickEventArgs e)
        {
            var list = JobInformationList;

            var isFound = list.Any(s => s.JobTitle == e.CellValue.ToString());
            if (!isFound)
            {
                await GetFilteredJobInformationList(e.CellValue.ToString(), null);
            }
        }

        public BindingList<JobInformationDto> JobInformationList { get; } = new BindingList<JobInformationDto>();

        public void BindGrids()
        {
            LoadListToGridView(JobInformationList);
            CustomizeDataGridView(false);
        }

        private void CustomizeDataGridView(bool IsFiltered)
        {
            jobGridView.Columns[nameof(JobInformationViewModel.JobId)].Visible = false;

            var c_SortByDate = jobGridView.Columns[nameof(JobInformationViewModel.SortByDate)];
            c_SortByDate.Visible = false;
            c_SortByDate.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_SortByDate.SortOrder = ColumnSortOrder.Descending;

            var c_JobTitle = jobGridView.Columns[nameof(JobInformationViewModel.JobTitle)];
            c_JobTitle.OptionsFilter.AllowFilter = false;
            c_JobTitle.SortOrder = ColumnSortOrder.Ascending;
            if (!IsFiltered)
                c_JobTitle.SortOrder = ColumnSortOrder.Descending;

            var c_JobDescription = jobGridView.Columns[nameof(JobInformationViewModel.JobDescription)];
            c_JobDescription.OptionsFilter.AllowFilter = false;
            c_JobDescription.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_JobDescription.SortOrder = ColumnSortOrder.Descending;

            var c_Created = jobGridView.Columns[nameof(JobInformationViewModel.Created)];
            c_Created.OptionsFilter.AllowFilter = false;
            c_Created.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_Created.SortOrder = ColumnSortOrder.Descending;

            var c_Owner = jobGridView.Columns[nameof(JobInformationViewModel.Owner)];
            c_Owner.OptionsFilter.AllowFilter = false;
            c_Owner.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_Owner.SortOrder = ColumnSortOrder.Descending;

            var c_Status = jobGridView.Columns[nameof(JobInformationViewModel.Status)];
            c_Status.OptionsFilter.AllowFilter = false;
            c_Status.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_Status.SortOrder = ColumnSortOrder.Descending;

            var c_Freelancer = jobGridView.Columns[nameof(JobInformationViewModel.Freelancer)];
            c_Freelancer.OptionsFilter.AllowFilter = false;
            c_SortByDate.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_Freelancer.SortOrder = ColumnSortOrder.Descending;

            var c_ActiveDate = jobGridView.Columns[nameof(JobInformationViewModel.ActiveDate)];
            c_ActiveDate.OptionsFilter.AllowFilter = false;
            c_ActiveDate.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_ActiveDate.SortOrder = ColumnSortOrder.Descending;

            var c_NumberOfMilestones = jobGridView.Columns[nameof(JobInformationViewModel.NumberOfMilestones)];
            c_NumberOfMilestones.OptionsFilter.AllowFilter = false;
            c_NumberOfMilestones.SortOrder = ColumnSortOrder.None;
            if (!IsFiltered)
                c_NumberOfMilestones.SortOrder = ColumnSortOrder.Descending;
        }

        private async void searchSimpleButton_Click(object sender, System.EventArgs e)
        {
            var jobTitle = jobTextBox.Text;
            var createdDate = createdDateEdit.Text;
            //if(string.IsNullOrEmpty(jobTitle) && string.IsNullOrEmpty(createdDate))
            //{
            //    LoadListToGridView(JobInformationList);
            //    return;
            //}

            searchSimpleButton.Text = "Searching...";
            searchSimpleButton.Enabled = false;
            jobGridView.ShowLoadingPanel();

            await GetFilteredJobInformationList(jobTitle, createdDate);

            jobGridView.HideLoadingPanel();
            searchSimpleButton.Enabled = true;
            searchSimpleButton.Text = "Search";
        }

        private async Task GetFilteredJobInformationList(string title, string date)
        {
            var data = await _presenter.SendFilterRequest(title, date);

            if (data == null)
            {
                jobGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<JobInformationDto>(data);
            LoadListToGridView(list);
            CustomizeDataGridView(true);
        }

        private void LoadListToGridView(BindingList<JobInformationDto> list)
        {
            var data = from j in list
                       select new JobInformationViewModel
                       {
                           JobId = j.JobId,
                           SortByDate = j.Created,
                           JobTitle = j.JobTitle,
                           JobDescription = j.JobDescription,
                           Created = j.Created,
                           Owner = j.Owner,
                           Status = j.Status,
                           Freelancer = j.Freelancer,
                           ActiveDate = j.ActiveDate,
                           NumberOfMilestones = j.NumberOfMilestones,
                           MilestoneInformations = new BindingList<MilestoneDetailDto>(j.MilestoneDetails.ToList())
                       };

            jobGridControl.DataSource = data;
        }
    }
}
