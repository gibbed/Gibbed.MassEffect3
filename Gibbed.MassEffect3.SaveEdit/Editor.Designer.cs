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
            this.openFromGenericButton = new System.Windows.Forms.ToolStripSplitButton();
            this.openFromCareerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToGenericButton = new System.Windows.Forms.ToolStripSplitButton();
            this.saveToCareerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.dontUseCareerPickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootTabControl = new System.Windows.Forms.TabControl();
            this.playerRootTabPage = new System.Windows.Forms.TabPage();
            this.playerTabControl = new System.Windows.Forms.TabControl();
            this.playerCharacterTabPage = new System.Windows.Forms.TabPage();
            this.playerAppearanceTabPage = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.importHeadMorphButton = new System.Windows.Forms.ToolStripButton();
            this.exportHeadMorphButton = new System.Windows.Forms.ToolStripButton();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.rawTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rootPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.childPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openHeadMorphDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveHeadMorphDialog = new System.Windows.Forms.SaveFileDialog();
            this.rootToolStrip.SuspendLayout();
            this.rootTabControl.SuspendLayout();
            this.playerRootTabPage.SuspendLayout();
            this.playerTabControl.SuspendLayout();
            this.playerAppearanceTabPage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.openFromGenericButton,
            this.saveToGenericButton,
            this.settingsButton});
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
            // openFromGenericButton
            // 
            this.openFromGenericButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFromCareerMenuItem,
            this.openFromFileMenuItem});
            this.openFromGenericButton.Image = ((System.Drawing.Image)(resources.GetObject("openFromGenericButton.Image")));
            this.openFromGenericButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFromGenericButton.Name = "openFromGenericButton";
            this.openFromGenericButton.Size = new System.Drawing.Size(68, 22);
            this.openFromGenericButton.Text = "Open";
            this.openFromGenericButton.ButtonClick += new System.EventHandler(this.OnOpenFromGeneric);
            // 
            // openFromCareerMenuItem
            // 
            this.openFromCareerMenuItem.Name = "openFromCareerMenuItem";
            this.openFromCareerMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openFromCareerMenuItem.Text = "Open from &Career...";
            this.openFromCareerMenuItem.Click += new System.EventHandler(this.OnOpenFromCareer);
            // 
            // openFromFileMenuItem
            // 
            this.openFromFileMenuItem.Name = "openFromFileMenuItem";
            this.openFromFileMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openFromFileMenuItem.Text = "Open from &File...";
            this.openFromFileMenuItem.Click += new System.EventHandler(this.OnOpenFromFile);
            // 
            // saveToGenericButton
            // 
            this.saveToGenericButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToCareerMenuItem,
            this.saveToFileMenuItem});
            this.saveToGenericButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToGenericButton.Image")));
            this.saveToGenericButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToGenericButton.Name = "saveToGenericButton";
            this.saveToGenericButton.Size = new System.Drawing.Size(63, 22);
            this.saveToGenericButton.Text = "Save";
            this.saveToGenericButton.ButtonClick += new System.EventHandler(this.OnSaveToGeneric);
            // 
            // saveToCareerMenuItem
            // 
            this.saveToCareerMenuItem.Name = "saveToCareerMenuItem";
            this.saveToCareerMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveToCareerMenuItem.Text = "Save to &Career...";
            this.saveToCareerMenuItem.Click += new System.EventHandler(this.OnSaveToCareer);
            // 
            // saveToFileMenuItem
            // 
            this.saveToFileMenuItem.Name = "saveToFileMenuItem";
            this.saveToFileMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveToFileMenuItem.Text = "Save to &File...";
            this.saveToFileMenuItem.Click += new System.EventHandler(this.OnSaveToFile);
            // 
            // settingsButton
            // 
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dontUseCareerPickerToolStripMenuItem});
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(29, 22);
            this.settingsButton.Text = "Settings";
            // 
            // dontUseCareerPickerToolStripMenuItem
            // 
            this.dontUseCareerPickerToolStripMenuItem.CheckOnClick = true;
            this.dontUseCareerPickerToolStripMenuItem.Name = "dontUseCareerPickerToolStripMenuItem";
            this.dontUseCareerPickerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.dontUseCareerPickerToolStripMenuItem.Text = "Don\'t use career picker";
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
            this.playerTabControl.Controls.Add(this.playerCharacterTabPage);
            this.playerTabControl.Controls.Add(this.playerAppearanceTabPage);
            this.playerTabControl.ImageList = this.iconImageList;
            this.playerTabControl.Location = new System.Drawing.Point(6, 6);
            this.playerTabControl.Name = "playerTabControl";
            this.playerTabControl.SelectedIndex = 0;
            this.playerTabControl.Size = new System.Drawing.Size(756, 401);
            this.playerTabControl.TabIndex = 0;
            // 
            // playerCharacterTabPage
            // 
            this.playerCharacterTabPage.ImageKey = "(none)";
            this.playerCharacterTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerCharacterTabPage.Name = "playerCharacterTabPage";
            this.playerCharacterTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerCharacterTabPage.Size = new System.Drawing.Size(748, 374);
            this.playerCharacterTabPage.TabIndex = 0;
            this.playerCharacterTabPage.Text = "Character";
            this.playerCharacterTabPage.UseVisualStyleBackColor = true;
            // 
            // playerAppearanceTabPage
            // 
            this.playerAppearanceTabPage.Controls.Add(this.toolStrip1);
            this.playerAppearanceTabPage.ImageKey = "(none)";
            this.playerAppearanceTabPage.Location = new System.Drawing.Point(4, 23);
            this.playerAppearanceTabPage.Name = "playerAppearanceTabPage";
            this.playerAppearanceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.playerAppearanceTabPage.Size = new System.Drawing.Size(748, 374);
            this.playerAppearanceTabPage.TabIndex = 1;
            this.playerAppearanceTabPage.Text = "Appearance";
            this.playerAppearanceTabPage.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importHeadMorphButton,
            this.exportHeadMorphButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(742, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // importHeadMorphButton
            // 
            this.importHeadMorphButton.Image = ((System.Drawing.Image)(resources.GetObject("importHeadMorphButton.Image")));
            this.importHeadMorphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importHeadMorphButton.Name = "importHeadMorphButton";
            this.importHeadMorphButton.Size = new System.Drawing.Size(133, 22);
            this.importHeadMorphButton.Text = "Import Head Morph";
            this.importHeadMorphButton.Click += new System.EventHandler(this.OnImportHeadMorph);
            // 
            // exportHeadMorphButton
            // 
            this.exportHeadMorphButton.Image = ((System.Drawing.Image)(resources.GetObject("exportHeadMorphButton.Image")));
            this.exportHeadMorphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportHeadMorphButton.Name = "exportHeadMorphButton";
            this.exportHeadMorphButton.Size = new System.Drawing.Size(130, 22);
            this.exportHeadMorphButton.Text = "Export Head Morph";
            this.exportHeadMorphButton.Click += new System.EventHandler(this.OnExportHeadMorph);
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
            // openHeadMorphDialog
            // 
            this.openHeadMorphDialog.Filter = "Mass Effect 3 Head Morphs (*.me3headmorph)|*.me3headmorph";
            // 
            // saveHeadMorphDialog
            // 
            this.saveHeadMorphDialog.Filter = "Mass Effect 3 Head Morphs (*.me3headmorph)|*.me3headmorph";
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
            this.playerAppearanceTabPage.ResumeLayout(false);
            this.playerAppearanceTabPage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSplitButton openFromGenericButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid rootPropertyGrid;
        private System.Windows.Forms.PropertyGrid childPropertyGrid;
        private System.Windows.Forms.ImageList iconImageList;
        private System.Windows.Forms.ToolStripSplitButton saveToGenericButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.TabControl playerTabControl;
        private System.Windows.Forms.TabPage playerCharacterTabPage;
        private System.Windows.Forms.TabPage playerAppearanceTabPage;
        private System.Windows.Forms.ToolStripMenuItem newMaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFemaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFromCareerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFromFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToCareerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openHeadMorphDialog;
        private System.Windows.Forms.SaveFileDialog saveHeadMorphDialog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton importHeadMorphButton;
        private System.Windows.Forms.ToolStripButton exportHeadMorphButton;
        private System.Windows.Forms.ToolStripDropDownButton settingsButton;
        private System.Windows.Forms.ToolStripMenuItem dontUseCareerPickerToolStripMenuItem;
    }
}

