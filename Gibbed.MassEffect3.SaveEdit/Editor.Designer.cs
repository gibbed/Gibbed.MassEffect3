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
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.newMaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFemaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootTabControl = new System.Windows.Forms.TabControl();
            this.playerRootTabPage = new System.Windows.Forms.TabPage();
            this.playerTabControl = new System.Windows.Forms.TabControl();
            this.playerBasicTabPage = new System.Windows.Forms.TabPage();
            this.playerAppearanceTabPage = new System.Windows.Forms.TabPage();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.rawTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rootPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.childPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.rootToolStrip.SuspendLayout();
            this.rootTabControl.SuspendLayout();
            this.playerRootTabPage.SuspendLayout();
            this.playerTabControl.SuspendLayout();
            this.rawTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMaleToolStripMenuItem,
            this.newFemaleToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // newMaleToolStripMenuItem
            // 
            this.newMaleToolStripMenuItem.Name = "newMaleToolStripMenuItem";
            this.newMaleToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.newMaleToolStripMenuItem.Text = "New &Male";
            // 
            // newFemaleToolStripMenuItem
            // 
            this.newFemaleToolStripMenuItem.Name = "newFemaleToolStripMenuItem";
            this.newFemaleToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.newFemaleToolStripMenuItem.Text = "New &Female";
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openFileToolStripMenuItem});
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(68, 22);
            this.toolStripSplitButton2.Text = "Open";
            this.toolStripSplitButton2.ButtonClick += new System.EventHandler(this.OnOpenFromFile);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem1.Text = "Open from &Career...";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openFileToolStripMenuItem.Text = "Open from &File...";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(63, 22);
            this.toolStripButton1.Text = "Save";
            this.toolStripButton1.ButtonClick += new System.EventHandler(this.OnSaveToFile);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem2.Text = "Save to &Career...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveToolStripMenuItem.Text = "Save to &File...";
            // 
            // rootTabControl
            // 
            this.rootTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rootTabControl.Controls.Add(this.playerRootTabPage);
            this.rootTabControl.Controls.Add(this.rawTabPage);
            this.rootTabControl.ImageList = this.iconImageList;
            this.rootTabControl.Location = new System.Drawing.Point(12, 28);
            this.rootTabControl.Name = "rootTabControl";
            this.rootTabControl.SelectedIndex = 0;
            this.rootTabControl.Size = new System.Drawing.Size(776, 440);
            this.rootTabControl.TabIndex = 1;
            // 
            // playerRootTabPage
            // 
            this.playerRootTabPage.Controls.Add(this.playerTabControl);
            this.playerRootTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerRootTabPage.Name = "playerRootTabPage";
            this.playerRootTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerRootTabPage.Size = new System.Drawing.Size(768, 413);
            this.playerRootTabPage.TabIndex = 0;
            this.playerRootTabPage.Text = "Player";
            this.playerRootTabPage.UseVisualStyleBackColor = true;
            // 
            // playerTabControl
            // 
            this.playerTabControl.Controls.Add(this.playerBasicTabPage);
            this.playerTabControl.Controls.Add(this.playerAppearanceTabPage);
            this.playerTabControl.ImageList = this.iconImageList;
            this.playerTabControl.Location = new System.Drawing.Point(6, 6);
            this.playerTabControl.Name = "playerTabControl";
            this.playerTabControl.SelectedIndex = 0;
            this.playerTabControl.Size = new System.Drawing.Size(756, 401);
            this.playerTabControl.TabIndex = 0;
            // 
            // playerBasicTabPage
            // 
            this.playerBasicTabPage.ImageKey = "(none)";
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
            this.playerAppearanceTabPage.ImageKey = "(none)";
            this.playerAppearanceTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerAppearanceTabPage.Name = "playerAppearanceTabPage";
            this.playerAppearanceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerAppearanceTabPage.Size = new System.Drawing.Size(748, 374);
            this.playerAppearanceTabPage.TabIndex = 1;
            this.playerAppearanceTabPage.Text = "Appearance";
            this.playerAppearanceTabPage.UseVisualStyleBackColor = true;
            // 
            // iconImageList
            // 
            this.iconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // rawTabPage
            // 
            this.rawTabPage.Controls.Add(this.splitContainer1);
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
            this.rootPropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.OnSelectedGridItemChanged);
            // 
            // childPropertyGrid
            // 
            this.childPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.childPropertyGrid.Name = "childPropertyGrid";
            this.childPropertyGrid.Size = new System.Drawing.Size(377, 407);
            this.childPropertyGrid.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "pcsav";
            this.openFileDialog.Filter = "Mass Effect 3 Save (*.pcsav, *.xbsav)|*.pcsav;*.xbsav|All Files (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Mass Effect 3 PC Save (*.pcsav)|*.pcsav|Mass Effect 3 XBOX 360 Save (*.xbsav)|*.x" +
                "bsav|All Files (*.*)|*.*";
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
            this.playerTabControl.ResumeLayout(false);
            this.rawTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ImageList iconImageList;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.TabControl playerTabControl;
        private System.Windows.Forms.TabPage playerBasicTabPage;
        private System.Windows.Forms.TabPage playerAppearanceTabPage;
        private System.Windows.Forms.ToolStripMenuItem newMaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFemaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

