using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Configuration;
using System.Windows.Forms;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.UserTab;

namespace Xperters.Admin.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            SetAppAndEnvironmentHeader();

            UserTabView.changeTabActivationEvent += new EventHandler<RowCellClickEventArgs>(changeTabStatus);
        }

        private void changeTabStatus(object sender, RowCellClickEventArgs e)
        {
            var value = e.CellValue.ToString();
            ApplicationState.JobInformationPresenter.GoToJobTab(tabControl1, value);
        }

        private void SetAppAndEnvironmentHeader()
        {
            var envName = ConfigurationManager.AppSettings["xpertersAdmin:environment-name"] ?? "Missing Environment Name setting";
            var version = ConfigurationManager.AppSettings["xpertersAdmin:environment-version"] ?? "Missing Version setting";
            Text = $@"Xperters.Admin v {version} ({envName} )";
        }
	}
}
