using Autofac;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.UserTab
{
    public partial class UserTabView : DevExpress.XtraEditors.XtraUserControl, IUserTabView
    {
        private UserInfoPresenter _presenter;
        public UserTabView()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _presenter = ContainerOperations
                    .Container
                    .Resolve<UserInfoPresenter>(new TypedParameter(typeof(IUserTabView), this));

                ApplicationState.userInfoPresenter = _presenter;
            }
            InitializeComponent();
            userGridView.OptionsBehavior.Editable = false;
        }

        public BindingList<UserInfoDto> UserInfoList { get; } = new BindingList<UserInfoDto>();

        public void BindGrids()
        {
            LoadListToGridView(UserInfoList);
        }

        private void LoadListToGridView(BindingList<UserInfoDto> list)
        {
            var data = from u in list
                       select new UserInfoViewModel
                       {
                           Id = u.Id,
                           DisplayName = u.DisplayName,
                           Mobile = u.Mobile,
                           UserRole = u.UserRole,
                           Created = u.Created,
                           IsEnabled = u.IsEnabled,
                           Country = u.Country,
                           JobsCreated = !string.Equals(u.UserRole, "Freelancer") ? u.Jobs.Count() : 0,
                           JobsWorkedOn = string.Equals(u.UserRole, "Freelancer") ? u.Jobs.Count() : 0,
                           Jobs = GetMapViewList(u.Jobs)
                       };
            userGridControl.DataSource = data;

            CustomizeDataGridView();
        }

        private List<JobDetailViewModel> GetMapViewList(List<JobDto> list)
        {
            var data = (from l in list
                       select new JobDetailViewModel
                       {
                           JobTitle = l.JobTitle,
                           CreatedDate = l.CreatedDate
                       }).OrderByDescending(x => x.CreatedDate).ToList();

            return data;
        }

        private async void refreshButton_Click(object sender, System.EventArgs e)
        {
            refreshButton.Text = "Refreshing...";
            userGridView.ShowLoadingPanel();

            if (userGridView.DataSource is List<UserInfoViewModel> gridData && gridData.Count > 0)
                gridData.Clear();

            await RefreshUserInfos();
            userGridView.HideLoadingPanel();
            refreshButton.Text = "Refresh";
        }

        private async Task RefreshUserInfos()
        {
            var data = await _presenter.SendRefreshRequest();

            if (data == null)
            {
                userGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<UserInfoDto>(data);
            LoadListToGridView(list);
        }

        private async void searchButton_Click(object sender, System.EventArgs e)
        {
            var displayName = nameTextBox.Text;
            var createdDate = createdDateEdit.Text;
            if (string.IsNullOrEmpty(displayName) && string.IsNullOrEmpty(createdDate))
            {
                LoadListToGridView(UserInfoList);
                return;
            }

            searchButton.Text = "Searching...";
            searchButton.Enabled = false;
            userGridView.ShowLoadingPanel();

            await GetFilteredUserInfoList(displayName, createdDate);

            userGridView.HideLoadingPanel();
            searchButton.Enabled = true;
            searchButton.Text = "Search";
        }

        private async Task GetFilteredUserInfoList(string name, string date)
        {
            var data = await _presenter.SendFilterRequest(name, date);

            if (data == null)
            {
                userGridControl.DataSource = null;
                return;
            }
            var list = new BindingList<UserInfoDto>(data);
            LoadListToGridView(list);
            CustomizeDataGridView();
        }

        private void CustomizeDataGridView()
        {
            userGridView.Columns[nameof(UserInfoViewModel.Id)].Visible = false;

            var newDetailView = userGridControl.CreateView("GridView") as GridView;
            newDetailView.OptionsView.ShowGroupPanel = false;
            newDetailView.OptionsBehavior.Editable = false;

            newDetailView.Columns.AddVisible(nameof(JobDetailViewModel.JobTitle), nameof(JobDetailViewModel.JobTitle))
                .ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            newDetailView.Columns.AddVisible(nameof(JobDetailViewModel.CreatedDate), nameof(JobDetailViewModel.CreatedDate));

            userGridView.MasterRowGetLevelDefaultView += (s, e) =>
            {
                e.DefaultView = newDetailView;
            };

            newDetailView.RowCellClick += (s, e) =>
            {
                changeTabActivationEvent?.Invoke(this, e);
                changeTabActivationEvent2?.Invoke(this, e);
            };
        }

        public static event EventHandler<RowCellClickEventArgs> changeTabActivationEvent;
        public static event EventHandler<RowCellClickEventArgs> changeTabActivationEvent2;
    }
}
