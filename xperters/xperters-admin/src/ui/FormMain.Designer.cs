using System.Windows.Forms;
using Xperters.Admin.UI.Tabs.ErrorWarningTab;
using Xperters.Admin.UI.Tabs.MilestoneAdminApprovals;

namespace Xperters.Admin.UI
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.paymentAdminApprovalTab = new System.Windows.Forms.TabPage();
            this._paymentAdminApprovalTabView = new Xperters.Admin.UI.Tabs.MilestoneAdminApprovals.PaymentAdminApprovalTabView();
            this.errorWarningTab = new System.Windows.Forms.TabPage();
            this.errorWarningView = new Xperters.Admin.UI.Tabs.ErrorWarningTab.ErrorWarningView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.jobTab = new System.Windows.Forms.TabPage();
            this.jobTabView1 = new Xperters.Admin.UI.Tabs.JobTab.JobTabView();
            this.paymentsWithdrawalsTab = new System.Windows.Forms.TabPage();
            this.paymentAdminWithdrawalsTabView1 = new Xperters.Admin.UI.Tabs.PaymentTab.PaymentAdminWithdrawalsTabView();
            this.paymentsIncomingTab = new System.Windows.Forms.TabPage();
            this.paymentAdminIncomingTabView1 = new Xperters.Admin.UI.Tabs.PaymentTab.PaymentAdminIncomingTabView();
            this.userTab = new System.Windows.Forms.TabPage();
            this.userTabView1 = new Xperters.Admin.UI.Tabs.UserTab.UserTabView();
            this.paymentAdminApprovalTab.SuspendLayout();
            this.errorWarningTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.jobTab.SuspendLayout();
            this.paymentsWithdrawalsTab.SuspendLayout();
            this.paymentsIncomingTab.SuspendLayout();
            this.userTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentAdminApprovalTab
            // 
            this.paymentAdminApprovalTab.Controls.Add(this._paymentAdminApprovalTabView);
            this.paymentAdminApprovalTab.Location = new System.Drawing.Point(4, 22);
            this.paymentAdminApprovalTab.Margin = new System.Windows.Forms.Padding(2);
            this.paymentAdminApprovalTab.Name = "paymentAdminApprovalTab";
            this.paymentAdminApprovalTab.Padding = new System.Windows.Forms.Padding(2);
            this.paymentAdminApprovalTab.Size = new System.Drawing.Size(1269, 658);
            this.paymentAdminApprovalTab.TabIndex = 1;
            this.paymentAdminApprovalTab.Text = "Payments For Approval";
            this.paymentAdminApprovalTab.UseVisualStyleBackColor = true;
            // 
            // _paymentAdminApprovalTabView
            // 
            this._paymentAdminApprovalTabView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentAdminApprovalTabView.Location = new System.Drawing.Point(2, 2);
            this._paymentAdminApprovalTabView.Name = "_paymentAdminApprovalTabView";
            this._paymentAdminApprovalTabView.Size = new System.Drawing.Size(1265, 654);
            this._paymentAdminApprovalTabView.TabIndex = 0;
            // 
            // errorWarningTab
            // 
            this.errorWarningTab.Controls.Add(this.errorWarningView);
            this.errorWarningTab.Location = new System.Drawing.Point(4, 22);
            this.errorWarningTab.Margin = new System.Windows.Forms.Padding(2);
            this.errorWarningTab.Name = "errorWarningTab";
            this.errorWarningTab.Padding = new System.Windows.Forms.Padding(2);
            this.errorWarningTab.Size = new System.Drawing.Size(1269, 658);
            this.errorWarningTab.TabIndex = 1;
            this.errorWarningTab.Text = "Errors / Warnings";
            this.errorWarningTab.UseVisualStyleBackColor = true;
            // 
            // errorWarningView
            // 
            this.errorWarningView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorWarningView.Location = new System.Drawing.Point(2, 2);
            this.errorWarningView.Margin = new System.Windows.Forms.Padding(2);
            this.errorWarningView.Name = "errorWarningView";
            this.errorWarningView.Size = new System.Drawing.Size(1265, 654);
            this.errorWarningView.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.paymentAdminApprovalTab);
            this.tabControl1.Controls.Add(this.jobTab);
            this.tabControl1.Controls.Add(this.paymentsWithdrawalsTab);
            this.tabControl1.Controls.Add(this.paymentsIncomingTab);
            this.tabControl1.Controls.Add(this.userTab);
            this.tabControl1.Controls.Add(this.errorWarningTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1277, 684);
            this.tabControl1.TabIndex = 0;
            // 
            // jobTab
            // 
            this.jobTab.Controls.Add(this.jobTabView1);
            this.jobTab.Location = new System.Drawing.Point(4, 22);
            this.jobTab.Name = "jobTab";
            this.jobTab.Size = new System.Drawing.Size(1269, 658);
            this.jobTab.TabIndex = 2;
            this.jobTab.Text = "Jobs";
            this.jobTab.UseVisualStyleBackColor = true;
            // 
            // jobTabView1
            // 
            this.jobTabView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobTabView1.Location = new System.Drawing.Point(0, 0);
            this.jobTabView1.Name = "jobTabView1";
            this.jobTabView1.Size = new System.Drawing.Size(1269, 658);
            this.jobTabView1.TabIndex = 0;
            // 
            // paymentsWithdrawalsTab
            // 
            this.paymentsWithdrawalsTab.Controls.Add(this.paymentAdminWithdrawalsTabView1);
            this.paymentsWithdrawalsTab.Location = new System.Drawing.Point(4, 22);
            this.paymentsWithdrawalsTab.Name = "paymentsWithdrawalsTab";
            this.paymentsWithdrawalsTab.Size = new System.Drawing.Size(1269, 658);
            this.paymentsWithdrawalsTab.TabIndex = 3;
            this.paymentsWithdrawalsTab.Text = "Payments (Withdrawals)";
            this.paymentsWithdrawalsTab.UseVisualStyleBackColor = true;
            // 
            // paymentAdminWithdrawalsTabView1
            // 
            this.paymentAdminWithdrawalsTabView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentAdminWithdrawalsTabView1.Location = new System.Drawing.Point(0, 0);
            this.paymentAdminWithdrawalsTabView1.Name = "paymentAdminWithdrawalsTabView1";
            this.paymentAdminWithdrawalsTabView1.Size = new System.Drawing.Size(1269, 658);
            this.paymentAdminWithdrawalsTabView1.TabIndex = 0;
            // 
            // paymentsIncomingTab
            // 
            this.paymentsIncomingTab.Controls.Add(this.paymentAdminIncomingTabView1);
            this.paymentsIncomingTab.Location = new System.Drawing.Point(4, 22);
            this.paymentsIncomingTab.Name = "paymentsIncomingTab";
            this.paymentsIncomingTab.Size = new System.Drawing.Size(1269, 658);
            this.paymentsIncomingTab.TabIndex = 4;
            this.paymentsIncomingTab.Text = "Payments (Incoming)";
            this.paymentsIncomingTab.UseVisualStyleBackColor = true;
            // 
            // paymentAdminIncomingTabView1
            // 
            this.paymentAdminIncomingTabView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentAdminIncomingTabView1.Location = new System.Drawing.Point(0, 0);
            this.paymentAdminIncomingTabView1.Name = "paymentAdminIncomingTabView1";
            this.paymentAdminIncomingTabView1.Size = new System.Drawing.Size(1269, 658);
            this.paymentAdminIncomingTabView1.TabIndex = 0;
            // 
            // userTab
            // 
            this.userTab.Controls.Add(this.userTabView1);
            this.userTab.Location = new System.Drawing.Point(4, 22);
            this.userTab.Name = "userTab";
            this.userTab.Size = new System.Drawing.Size(1269, 658);
            this.userTab.TabIndex = 5;
            this.userTab.Text = "Users";
            this.userTab.UseVisualStyleBackColor = true;
            // 
            // userTabView1
            // 
            this.userTabView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userTabView1.Location = new System.Drawing.Point(0, 0);
            this.userTabView1.Name = "userTabView1";
            this.userTabView1.Size = new System.Drawing.Size(1269, 658);
            this.userTabView1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 684);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "Xperters Admin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.paymentAdminApprovalTab.ResumeLayout(false);
            this.errorWarningTab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.jobTab.ResumeLayout(false);
            this.paymentsWithdrawalsTab.ResumeLayout(false);
            this.paymentsIncomingTab.ResumeLayout(false);
            this.userTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabPage paymentAdminApprovalTab;
		private System.Windows.Forms.TabPage errorWarningTab;

		private System.Windows.Forms.TabControl tabControl1;

		private ErrorWarningView errorWarningView;
		private PaymentAdminApprovalTabView _paymentAdminApprovalTabView;
        private TabPage jobTab;
        private Tabs.JobTab.JobTabView jobTabView1;
        private TabPage paymentsWithdrawalsTab;
        private Tabs.PaymentTab.PaymentAdminWithdrawalsTabView paymentAdminWithdrawalsTabView1;
        private TabPage paymentsIncomingTab;
        private Tabs.PaymentTab.PaymentAdminIncomingTabView paymentAdminIncomingTabView1;
        private TabPage userTab;
        private Tabs.UserTab.UserTabView userTabView1;
    }
}