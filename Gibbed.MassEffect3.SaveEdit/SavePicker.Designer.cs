namespace Gibbed.MassEffect3.SaveEdit
{
    partial class SavePicker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavePicker));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "(new career)"}, -1, System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("(new save file)");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.deleteCareerButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.deleteSaveButton = new System.Windows.Forms.ToolStripButton();
            this.loadFileButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileButton = new System.Windows.Forms.ToolStripButton();
            this.cancelButton = new System.Windows.Forms.ToolStripButton();
            this.careerListView = new Gibbed.MassEffect3.SaveEdit.DoubleBufferedListView();
            this.careerHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveListView = new Gibbed.MassEffect3.SaveEdit.DoubleBufferedListView();
            this.saveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.careerListView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.saveListView);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(640, 320);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 1;
            // 
            // iconImageList
            // 
            this.iconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteCareerButton,
            this.refreshButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 295);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(213, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // deleteCareerButton
            // 
            this.deleteCareerButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteCareerButton.Image")));
            this.deleteCareerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteCareerButton.Name = "deleteCareerButton";
            this.deleteCareerButton.Size = new System.Drawing.Size(97, 22);
            this.deleteCareerButton.Text = "Delete Career";
            this.deleteCareerButton.Click += new System.EventHandler(this.OnDeleteCareer);
            // 
            // refreshButton
            // 
            this.refreshButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.Click += new System.EventHandler(this.OnRefresh);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSaveButton,
            this.loadFileButton,
            this.saveFileButton,
            this.cancelButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 295);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(423, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // deleteSaveButton
            // 
            this.deleteSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteSaveButton.Image")));
            this.deleteSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSaveButton.Name = "deleteSaveButton";
            this.deleteSaveButton.Size = new System.Drawing.Size(87, 22);
            this.deleteSaveButton.Text = "Delete Save";
            this.deleteSaveButton.Click += new System.EventHandler(this.OnDeleteSave);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.loadFileButton.Image = ((System.Drawing.Image)(resources.GetObject("loadFileButton.Image")));
            this.loadFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(53, 22);
            this.loadFileButton.Text = "Load";
            this.loadFileButton.Click += new System.EventHandler(this.OnChooseSave);
            // 
            // saveFileButton
            // 
            this.saveFileButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("saveFileButton.Image")));
            this.saveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(51, 22);
            this.saveFileButton.Text = "Save";
            this.saveFileButton.Click += new System.EventHandler(this.OnChooseSave);
            // 
            // cancelButton
            // 
            this.cancelButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cancelButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelButton.Image")));
            this.cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 22);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.OnCancel);
            // 
            // careerListView
            // 
            this.careerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.careerHeader});
            this.careerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.careerListView.FullRowSelect = true;
            this.careerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.careerListView.HideSelection = false;
            this.careerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.careerListView.LargeImageList = this.iconImageList;
            this.careerListView.Location = new System.Drawing.Point(0, 0);
            this.careerListView.Name = "careerListView";
            this.careerListView.Size = new System.Drawing.Size(213, 295);
            this.careerListView.SmallImageList = this.iconImageList;
            this.careerListView.TabIndex = 0;
            this.careerListView.UseCompatibleStateImageBehavior = false;
            this.careerListView.View = System.Windows.Forms.View.Details;
            this.careerListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.OnSelectCareer);
            // 
            // careerHeader
            // 
            this.careerHeader.Text = "Career";
            this.careerHeader.Width = 190;
            // 
            // saveListView
            // 
            this.saveListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.saveHeader});
            this.saveListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveListView.FullRowSelect = true;
            this.saveListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.saveListView.HideSelection = false;
            this.saveListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.saveListView.LargeImageList = this.iconImageList;
            this.saveListView.Location = new System.Drawing.Point(0, 0);
            this.saveListView.Name = "saveListView";
            this.saveListView.Size = new System.Drawing.Size(423, 295);
            this.saveListView.SmallImageList = this.iconImageList;
            this.saveListView.TabIndex = 1;
            this.saveListView.UseCompatibleStateImageBehavior = false;
            this.saveListView.View = System.Windows.Forms.View.Details;
            this.saveListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.OnSelectSave);
            // 
            // saveHeader
            // 
            this.saveHeader.Text = "Save";
            this.saveHeader.Width = 390;
            // 
            // SavePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 320);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SavePicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose a save...";
            this.Shown += new System.EventHandler(this.OnShown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DoubleBufferedListView careerListView;
        private System.Windows.Forms.ColumnHeader careerHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton deleteCareerButton;
        private DoubleBufferedListView saveListView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton deleteSaveButton;
        private System.Windows.Forms.ToolStripButton loadFileButton;
        private System.Windows.Forms.ImageList iconImageList;
        private System.Windows.Forms.ColumnHeader saveHeader;
        private System.Windows.Forms.ToolStripButton cancelButton;
        private System.Windows.Forms.ToolStripButton saveFileButton;
        private System.Windows.Forms.ToolStripButton refreshButton;
    }
}