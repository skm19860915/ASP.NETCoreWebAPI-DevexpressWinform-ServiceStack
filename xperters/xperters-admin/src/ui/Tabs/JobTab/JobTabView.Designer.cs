namespace Xperters.Admin.UI.Tabs.JobTab
{
    partial class JobTabView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.jobGridControl = new DevExpress.XtraGrid.GridControl();
            this.jobGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.searchSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.createdDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.jobTextBox = new System.Windows.Forms.TextBox();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dateLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.jobLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.jobGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.createdDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.createdDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // jobGridControl
            // 
            this.jobGridControl.Location = new System.Drawing.Point(12, 38);
            this.jobGridControl.MainView = this.jobGridView;
            this.jobGridControl.Name = "jobGridControl";
            this.jobGridControl.Size = new System.Drawing.Size(1011, 544);
            this.jobGridControl.TabIndex = 0;
            this.jobGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.jobGridView});
            // 
            // jobGridView
            // 
            this.jobGridView.GridControl = this.jobGridControl;
            this.jobGridView.Name = "jobGridView";
            this.jobGridView.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.searchSimpleButton);
            this.layoutControl1.Controls.Add(this.createdDateEdit);
            this.layoutControl1.Controls.Add(this.jobTextBox);
            this.layoutControl1.Controls.Add(this.jobGridControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1035, 594);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // searchSimpleButton
            // 
            this.searchSimpleButton.Location = new System.Drawing.Point(902, 12);
            this.searchSimpleButton.Name = "searchSimpleButton";
            this.searchSimpleButton.Size = new System.Drawing.Size(99, 22);
            this.searchSimpleButton.StyleController = this.layoutControl1;
            this.searchSimpleButton.TabIndex = 6;
            this.searchSimpleButton.Text = "Search";
            this.searchSimpleButton.Click += new System.EventHandler(this.searchSimpleButton_Click);
            // 
            // createdDateEdit
            // 
            this.createdDateEdit.EditValue = null;
            this.createdDateEdit.Location = new System.Drawing.Point(733, 12);
            this.createdDateEdit.Name = "createdDateEdit";
            this.createdDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.createdDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.createdDateEdit.Size = new System.Drawing.Size(157, 20);
            this.createdDateEdit.StyleController = this.layoutControl1;
            this.createdDateEdit.TabIndex = 5;
            // 
            // jobTextBox
            // 
            this.jobTextBox.Location = new System.Drawing.Point(476, 12);
            this.jobTextBox.Name = "jobTextBox";
            this.jobTextBox.Size = new System.Drawing.Size(165, 20);
            this.jobTextBox.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.dateLayoutControlItem,
            this.jobLayoutControlItem,
            this.layoutControlItem2,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1035, 594);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.jobGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1015, 548);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(412, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dateLayoutControlItem
            // 
            this.dateLayoutControlItem.Control = this.createdDateEdit;
            this.dateLayoutControlItem.Location = new System.Drawing.Point(641, 0);
            this.dateLayoutControlItem.Name = "dateLayoutControlItem";
            this.dateLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 10, 2, 2);
            this.dateLayoutControlItem.Size = new System.Drawing.Size(249, 26);
            this.dateLayoutControlItem.Text = "Created Date : ";
            this.dateLayoutControlItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.dateLayoutControlItem.TextSize = new System.Drawing.Size(75, 13);
            this.dateLayoutControlItem.TextToControlDistance = 5;
            // 
            // jobLayoutControlItem
            // 
            this.jobLayoutControlItem.Control = this.jobTextBox;
            this.jobLayoutControlItem.Location = new System.Drawing.Point(412, 0);
            this.jobLayoutControlItem.Name = "jobLayoutControlItem";
            this.jobLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 10, 2, 2);
            this.jobLayoutControlItem.Size = new System.Drawing.Size(229, 26);
            this.jobLayoutControlItem.Text = "Job Title : ";
            this.jobLayoutControlItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.jobLayoutControlItem.TextSize = new System.Drawing.Size(50, 13);
            this.jobLayoutControlItem.TextToControlDistance = 2;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.searchSimpleButton;
            this.layoutControlItem2.Location = new System.Drawing.Point(890, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(993, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(22, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // JobTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "JobTabView";
            this.Size = new System.Drawing.Size(1035, 594);
            ((System.ComponentModel.ISupportInitialize)(this.jobGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.createdDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.createdDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl jobGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView jobGridView;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton searchSimpleButton;
        private DevExpress.XtraEditors.DateEdit createdDateEdit;
        private System.Windows.Forms.TextBox jobTextBox;
        private DevExpress.XtraLayout.LayoutControlItem dateLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem jobLayoutControlItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}
