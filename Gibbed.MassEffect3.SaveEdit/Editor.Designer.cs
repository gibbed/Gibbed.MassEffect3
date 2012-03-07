namespace Gibbed.MassEffect3.SaveEdit
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.rootToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.rootTabControl = new System.Windows.Forms.TabControl();
            this.playerRootTabPage = new System.Windows.Forms.TabPage();
            this.rawTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rootPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.childPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.playerTabControl = new System.Windows.Forms.TabControl();
            this.playerBasicTabPage = new System.Windows.Forms.TabPage();
            this.playerAppearanceTabPage = new System.Windows.Forms.TabPage();
            this.rootToolStrip.SuspendLayout();
            this.rootTabControl.SuspendLayout();
            this.playerRootTabPage.SuspendLayout();
            this.rawTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.playerTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootToolStrip
            // 
            this.rootToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripSplitButton2,
            this.toolStripButton1});
            this.rootToolStrip.Location = new System.Drawing.Point(0, 0);
            this.rootToolStrip.Name = "rootToolStrip";
            this.rootToolStrip.Size = new System.Drawing.Size(800, 25);
            this.rootToolStrip.TabIndex = 0;
            this.rootToolStrip.Text = "toolStrip1";
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
            this.toolStripSplitButton2.ButtonClick += new System.EventHandler(this.OnOpenFile);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // rootTabControl
            // 
            this.rootTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rootTabControl.Controls.Add(this.playerRootTabPage);
            this.rootTabControl.Controls.Add(this.rawTabPage);
            this.rootTabControl.ImageList = this.tabImageList;
            this.rootTabControl.Location = new System.Drawing.Point(12, 28);
            this.rootTabControl.Name = "rootTabControl";
            this.rootTabControl.SelectedIndex = 0;
            this.rootTabControl.Size = new System.Drawing.Size(776, 440);
            this.rootTabControl.TabIndex = 1;
            // 
            // playerRootTabPage
            // 
            this.playerRootTabPage.Controls.Add(this.playerTabControl);
            this.playerRootTabPage.ImageIndex = 2;
            this.playerRootTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerRootTabPage.Name = "playerRootTabPage";
            this.playerRootTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerRootTabPage.Size = new System.Drawing.Size(768, 413);
            this.playerRootTabPage.TabIndex = 0;
            this.playerRootTabPage.Text = "Player";
            this.playerRootTabPage.UseVisualStyleBackColor = true;
            // 
            // rawTabPage
            // 
            this.rawTabPage.Controls.Add(this.splitContainer1);
            this.rawTabPage.ImageIndex = 1;
            this.rawTabPage.Location = new System.Drawing.Point(4, 23);
            this.rawTabPage.Name = "rawTabPage";
            this.rawTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.rawTabPage.Size = new System.Drawing.Size(768, 413);
            this.rawTabPage.TabIndex = 1;
            this.rawTabPage.Text = "Raw";
            this.rawTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rootPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.childPropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(762, 407);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.TabIndex = 0;
            // 
            // rootPropertyGrid
            // 
            this.rootPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.rootPropertyGrid.Name = "rootPropertyGrid";
            this.rootPropertyGrid.Size = new System.Drawing.Size(381, 407);
            this.rootPropertyGrid.TabIndex = 1;
            this.rootPropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.rootPropertyGrid_SelectedGridItemChanged);
            // 
            // childPropertyGrid
            // 
            this.childPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.childPropertyGrid.Name = "childPropertyGrid";
            this.childPropertyGrid.Size = new System.Drawing.Size(377, 407);
            this.childPropertyGrid.TabIndex = 0;
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImageList.Images.SetKeyName(0, "robot.png");
            this.tabImageList.Images.SetKeyName(1, "script-binary.png");
            this.tabImageList.Images.SetKeyName(2, "user-thief.png");
            this.tabImageList.Images.SetKeyName(3, "user-thief-female.png");
            this.tabImageList.Images.SetKeyName(4, "x-ray.png");
            this.tabImageList.Images.SetKeyName(5, "t-shirt-print-gray.png");
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // playerTabControl
            // 
            this.playerTabControl.Controls.Add(this.playerBasicTabPage);
            this.playerTabControl.Controls.Add(this.playerAppearanceTabPage);
            this.playerTabControl.ImageList = this.tabImageList;
            this.playerTabControl.Location = new System.Drawing.Point(6, 6);
            this.playerTabControl.Name = "playerTabControl";
            this.playerTabControl.SelectedIndex = 0;
            this.playerTabControl.Size = new System.Drawing.Size(756, 401);
            this.playerTabControl.TabIndex = 0;
            // 
            // playerBasicTabPage
            // 
            this.playerBasicTabPage.ImageKey = "x-ray.png";
            this.playerBasicTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerBasicTabPage.Name = "playerBasicTabPage";
            this.playerBasicTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerBasicTabPage.Size = new System.Drawing.Size(748, 374);
            this.playerBasicTabPage.TabIndex = 0;
            this.playerBasicTabPage.Text = "Basic";
            this.playerBasicTabPage.UseVisualStyleBackColor = true;
            // 
            // playerAppearanceTabPage
            // 
            this.playerAppearanceTabPage.ImageKey = "t-shirt-print-gray.png";
            this.playerAppearanceTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerAppearanceTabPage.Name = "playerAppearanceTabPage";
            this.playerAppearanceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerAppearanceTabPage.Size = new System.Drawing.Size(748, 374);
            this.playerAppearanceTabPage.TabIndex = 1;
            this.playerAppearanceTabPage.Text = "Appearance";
            this.playerAppearanceTabPage.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.rootTabControl);
            this.Controls.Add(this.rootToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Editor";
            this.Text = "Gibbed\'s Mass Effect 3 Save Editor";
            this.rootToolStrip.ResumeLayout(false);
            this.rootToolStrip.PerformLayout();
            this.rootTabControl.ResumeLayout(false);
            this.playerRootTabPage.ResumeLayout(false);
            this.rawTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.playerTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip rootToolStrip;
        private System.Windows.Forms.TabControl rootTabControl;
        private System.Windows.Forms.TabPage playerRootTabPage;
        private System.Windows.Forms.TabPage rawTabPage;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid rootPropertyGrid;
        private System.Windows.Forms.PropertyGrid childPropertyGrid;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.TabControl playerTabControl;
        private System.Windows.Forms.TabPage playerBasicTabPage;
        private System.Windows.Forms.TabPage playerAppearanceTabPage;
    }
}

