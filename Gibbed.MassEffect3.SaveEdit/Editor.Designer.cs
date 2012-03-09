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
            this.playerAppearanceToolStrip = new System.Windows.Forms.ToolStrip();
            this.importHeadMorphButton = new System.Windows.Forms.ToolStripButton();
            this.exportHeadMorphButton = new System.Windows.Forms.ToolStripButton();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.rawTabPage = new System.Windows.Forms.TabPage();
            this.rawSplitContainer = new System.Windows.Forms.SplitContainer();
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
            this.playerAppearanceToolStrip.SuspendLayout();
            this.rawTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawSplitContainer)).BeginInit();
            this.rawSplitContainer.Panel1.SuspendLayout();
            this.rawSplitContainer.Panel2.SuspendLayout();
            this.rawSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootToolStrip
            // 
            this.rootToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.openFromGenericButton,
            this.saveToGenericButton,
            this.settingsButton});
            resources.ApplyResources(this.rootToolStrip, "rootToolStrip");
            this.rootToolStrip.Name = "rootToolStrip";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMaleToolStripMenuItem,
            this.newFemaleToolStripMenuItem});
            resources.ApplyResources(this.toolStripSplitButton1, "toolStripSplitButton1");
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            // 
            // newMaleToolStripMenuItem
            // 
            this.newMaleToolStripMenuItem.Name = "newMaleToolStripMenuItem";
            resources.ApplyResources(this.newMaleToolStripMenuItem, "newMaleToolStripMenuItem");
            // 
            // newFemaleToolStripMenuItem
            // 
            this.newFemaleToolStripMenuItem.Name = "newFemaleToolStripMenuItem";
            resources.ApplyResources(this.newFemaleToolStripMenuItem, "newFemaleToolStripMenuItem");
            // 
            // openFromGenericButton
            // 
            this.openFromGenericButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFromCareerMenuItem,
            this.openFromFileMenuItem});
            resources.ApplyResources(this.openFromGenericButton, "openFromGenericButton");
            this.openFromGenericButton.Name = "openFromGenericButton";
            this.openFromGenericButton.ButtonClick += new System.EventHandler(this.OnOpenFromGeneric);
            // 
            // openFromCareerMenuItem
            // 
            this.openFromCareerMenuItem.Name = "openFromCareerMenuItem";
            resources.ApplyResources(this.openFromCareerMenuItem, "openFromCareerMenuItem");
            this.openFromCareerMenuItem.Click += new System.EventHandler(this.OnOpenFromCareer);
            // 
            // openFromFileMenuItem
            // 
            this.openFromFileMenuItem.Name = "openFromFileMenuItem";
            resources.ApplyResources(this.openFromFileMenuItem, "openFromFileMenuItem");
            this.openFromFileMenuItem.Click += new System.EventHandler(this.OnOpenFromFile);
            // 
            // saveToGenericButton
            // 
            this.saveToGenericButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToCareerMenuItem,
            this.saveToFileMenuItem});
            resources.ApplyResources(this.saveToGenericButton, "saveToGenericButton");
            this.saveToGenericButton.Name = "saveToGenericButton";
            this.saveToGenericButton.ButtonClick += new System.EventHandler(this.OnSaveToGeneric);
            // 
            // saveToCareerMenuItem
            // 
            this.saveToCareerMenuItem.Name = "saveToCareerMenuItem";
            resources.ApplyResources(this.saveToCareerMenuItem, "saveToCareerMenuItem");
            this.saveToCareerMenuItem.Click += new System.EventHandler(this.OnSaveToCareer);
            // 
            // saveToFileMenuItem
            // 
            this.saveToFileMenuItem.Name = "saveToFileMenuItem";
            resources.ApplyResources(this.saveToFileMenuItem, "saveToFileMenuItem");
            this.saveToFileMenuItem.Click += new System.EventHandler(this.OnSaveToFile);
            // 
            // settingsButton
            // 
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dontUseCareerPickerToolStripMenuItem});
            resources.ApplyResources(this.settingsButton, "settingsButton");
            this.settingsButton.Name = "settingsButton";
            // 
            // dontUseCareerPickerToolStripMenuItem
            // 
            this.dontUseCareerPickerToolStripMenuItem.CheckOnClick = true;
            this.dontUseCareerPickerToolStripMenuItem.Name = "dontUseCareerPickerToolStripMenuItem";
            resources.ApplyResources(this.dontUseCareerPickerToolStripMenuItem, "dontUseCareerPickerToolStripMenuItem");
            // 
            // rootTabControl
            // 
            resources.ApplyResources(this.rootTabControl, "rootTabControl");
            this.rootTabControl.Controls.Add(this.playerRootTabPage);
            this.rootTabControl.Controls.Add(this.rawTabPage);
            this.rootTabControl.ImageList = this.iconImageList;
            this.rootTabControl.Name = "rootTabControl";
            this.rootTabControl.SelectedIndex = 0;
            // 
            // playerRootTabPage
            // 
            this.playerRootTabPage.Controls.Add(this.playerTabControl);
            resources.ApplyResources(this.playerRootTabPage, "playerRootTabPage");
            this.playerRootTabPage.Name = "playerRootTabPage";
            this.playerRootTabPage.UseVisualStyleBackColor = true;
            // 
            // playerTabControl
            // 
            this.playerTabControl.Controls.Add(this.playerCharacterTabPage);
            this.playerTabControl.Controls.Add(this.playerAppearanceTabPage);
            this.playerTabControl.ImageList = this.iconImageList;
            resources.ApplyResources(this.playerTabControl, "playerTabControl");
            this.playerTabControl.Name = "playerTabControl";
            this.playerTabControl.SelectedIndex = 0;
            // 
            // playerCharacterTabPage
            // 
            resources.ApplyResources(this.playerCharacterTabPage, "playerCharacterTabPage");
            this.playerCharacterTabPage.Name = "playerCharacterTabPage";
            this.playerCharacterTabPage.UseVisualStyleBackColor = true;
            // 
            // playerAppearanceTabPage
            // 
            this.playerAppearanceTabPage.Controls.Add(this.playerAppearanceToolStrip);
            resources.ApplyResources(this.playerAppearanceTabPage, "playerAppearanceTabPage");
            this.playerAppearanceTabPage.Name = "playerAppearanceTabPage";
            this.playerAppearanceTabPage.UseVisualStyleBackColor = true;
            // 
            // playerAppearanceToolStrip
            // 
            this.playerAppearanceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importHeadMorphButton,
            this.exportHeadMorphButton});
            resources.ApplyResources(this.playerAppearanceToolStrip, "playerAppearanceToolStrip");
            this.playerAppearanceToolStrip.Name = "playerAppearanceToolStrip";
            // 
            // importHeadMorphButton
            // 
            resources.ApplyResources(this.importHeadMorphButton, "importHeadMorphButton");
            this.importHeadMorphButton.Name = "importHeadMorphButton";
            this.importHeadMorphButton.Click += new System.EventHandler(this.OnImportHeadMorph);
            // 
            // exportHeadMorphButton
            // 
            resources.ApplyResources(this.exportHeadMorphButton, "exportHeadMorphButton");
            this.exportHeadMorphButton.Name = "exportHeadMorphButton";
            this.exportHeadMorphButton.Click += new System.EventHandler(this.OnExportHeadMorph);
            // 
            // iconImageList
            // 
            this.iconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.iconImageList, "iconImageList");
            this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // rawTabPage
            // 
            this.rawTabPage.Controls.Add(this.rawSplitContainer);
            resources.ApplyResources(this.rawTabPage, "rawTabPage");
            this.rawTabPage.Name = "rawTabPage";
            this.rawTabPage.UseVisualStyleBackColor = true;
            // 
            // rawSplitContainer
            // 
            resources.ApplyResources(this.rawSplitContainer, "rawSplitContainer");
            this.rawSplitContainer.Name = "rawSplitContainer";
            // 
            // rawSplitContainer.Panel1
            // 
            this.rawSplitContainer.Panel1.Controls.Add(this.rootPropertyGrid);
            // 
            // rawSplitContainer.Panel2
            // 
            this.rawSplitContainer.Panel2.Controls.Add(this.childPropertyGrid);
            // 
            // rootPropertyGrid
            // 
            resources.ApplyResources(this.rootPropertyGrid, "rootPropertyGrid");
            this.rootPropertyGrid.Name = "rootPropertyGrid";
            this.rootPropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.OnSelectedGridItemChanged);
            // 
            // childPropertyGrid
            // 
            resources.ApplyResources(this.childPropertyGrid, "childPropertyGrid");
            this.childPropertyGrid.Name = "childPropertyGrid";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "pcsav";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // openHeadMorphDialog
            // 
            resources.ApplyResources(this.openHeadMorphDialog, "openHeadMorphDialog");
            // 
            // saveHeadMorphDialog
            // 
            resources.ApplyResources(this.saveHeadMorphDialog, "saveHeadMorphDialog");
            // 
            // Editor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rootTabControl);
            this.Controls.Add(this.rootToolStrip);
            this.Name = "Editor";
            this.rootToolStrip.ResumeLayout(false);
            this.rootToolStrip.PerformLayout();
            this.rootTabControl.ResumeLayout(false);
            this.playerRootTabPage.ResumeLayout(false);
            this.playerTabControl.ResumeLayout(false);
            this.playerAppearanceTabPage.ResumeLayout(false);
            this.playerAppearanceTabPage.PerformLayout();
            this.playerAppearanceToolStrip.ResumeLayout(false);
            this.playerAppearanceToolStrip.PerformLayout();
            this.rawTabPage.ResumeLayout(false);
            this.rawSplitContainer.Panel1.ResumeLayout(false);
            this.rawSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rawSplitContainer)).EndInit();
            this.rawSplitContainer.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer rawSplitContainer;
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
        private System.Windows.Forms.ToolStrip playerAppearanceToolStrip;
        private System.Windows.Forms.ToolStripButton importHeadMorphButton;
        private System.Windows.Forms.ToolStripButton exportHeadMorphButton;
        private System.Windows.Forms.ToolStripDropDownButton settingsButton;
        private System.Windows.Forms.ToolStripMenuItem dontUseCareerPickerToolStripMenuItem;
    }
}

