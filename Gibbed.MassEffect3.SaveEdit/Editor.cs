/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using ColorPicker;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Gibbed.IO;
using Gibbed.MassEffect3.SaveEdit.Resources;
using Newtonsoft.Json;

namespace Gibbed.MassEffect3.SaveEdit
{
    public partial class Editor : Form
    {
        public Editor()
        {
            this.InitializeComponent();

            this._SavePath = null;

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.DoubleBuffered = true;
            if (Version.Revision > 0)
            {
                this.Text += String.Format(
                    " (Build revision {0} @ {1})",
                    Version.Revision,
                    Version.Date);
            }
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            this.wrexPictureBox.Image = Image.FromStream(new MemoryStream(Images.Wrex), true);

            bool hasSaveFolder = false;
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (string.IsNullOrEmpty(savePath) == false)
            {
                savePath = Path.Combine(savePath, "BioWare");
                savePath = Path.Combine(savePath, "Mass Effect 3");
                savePath = Path.Combine(savePath, "Save");

                if (Directory.Exists(savePath) == true)
                {
                    this._SavePath = savePath;
                    this.openFileDialog.InitialDirectory = savePath;
                    this.saveFileDialog.InitialDirectory = savePath;
                    hasSaveFolder = true;
                }
            }

            if (hasSaveFolder == false)
            {
                this.dontUseCareerPickerToolStripMenuItem.Checked = true;
                this.dontUseCareerPickerToolStripMenuItem.Enabled = false;
                this.openFromCareerMenuItem.Enabled = false;
                this.saveToCareerMenuItem.Enabled = false;
            }

            var presetPath = Path.Combine(GetExecutablePath(), "presets");
            if (Directory.Exists(presetPath) == true)
            {
                this.openAppearancePresetFileDialog.InitialDirectory = presetPath;
                this.saveAppearancePresetFileDialog.InitialDirectory = presetPath;
            }

            // ReSharper disable LocalizableElement
            this.iconImageList.Images.Add("Unknown", new Bitmap(16, 16));
            // ReSharper restore LocalizableElement

            //this.rootTabControl.SelectedTab = rawRootTabPage;
            this.rawSplitContainer.Panel2Collapsed = true;

            this.LoadDefaultMaleSave();

            this.SuspendLayout();
            this.AddTable("Character", BasicTable.Character.Build(this));
            this.AddTable("Reputation", BasicTable.Reputation.Build(this));
            this.AddTable("Resources", BasicTable.Resources.Build(this));
            this.ResumeLayout();
        }

        private void AddTable(string name, List<BasicTable.TableItem> items)
        {
            int row = 0;

            var panel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = items.Count
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
// ReSharper disable ForCanBeConvertedToForeach
            for (int i = 0; i < items.Count; i++)
// ReSharper restore ForCanBeConvertedToForeach
            {
                panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            foreach (var item in items)
            {
                panel.Controls.Add(item.Control);
                panel.SetRow(item.Control, row);
                panel.SetColumn(item.Control, 1);

                if (string.IsNullOrEmpty(item.Name) == false)
                {
                    var label = new Label()
                    {
// ReSharper disable LocalizableElement
                        Text = item.Name + ":",
// ReSharper restore LocalizableElement
                        Dock = DockStyle.Fill,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleRight,
                    };
                    panel.Controls.Add(label);
                    panel.SetRow(label, row);
                    panel.SetColumn(label, 0);
                }
                else
                {
                    panel.SetColumnSpan(item.Control, 2);
                }

                if (item.Binding != null)
                {
                    item.Control.DataBindings.Add(item.Binding);
                }

                row++;
            }

            panel.AutoSize = true;
            panel.Dock = DockStyle.Fill;

            var group = new GroupBox();
            group.Text = name;
            group.MinimumSize = new Size(320, 0);
            group.AutoSize = true;
            group.Controls.Add(panel);

            this.playerBasicPanel.Controls.Add(group);
        }

        private static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void LoadDefaultMaleSave()
        {
            using (var memory = new MemoryStream(Properties.Resources.DefaultMaleSave))
            {
                this.LoadSaveFromStream(memory);
                this.SaveFile.Player.Guid = Guid.NewGuid();
            }
        }

        private void LoadDefaultFemaleSave()
        {
            using (var memory = new MemoryStream(Properties.Resources.DefaultFemaleSave))
            {
                this.LoadSaveFromStream(memory);
                this.SaveFile.Player.Guid = Guid.NewGuid();
            }
        }

        private readonly string _SavePath;
        private FileFormats.SFXSaveGameFile _SaveFile;

        private FileFormats.SFXSaveGameFile SaveFile
        {
            get { return this._SaveFile; }
            set
            {
                if (this._SaveFile != value)
                {
                    if (this._SaveFile != null)
                    {
                        this._SaveFile.Player.PropertyChanged -= this.OnPlayerPropertyChanged;
                        this._SaveFile.Player.Appearance.PropertyChanged -= this.OnPlayerAppearancePropertyChanged;
                    }

                    var oldValue = this._SaveFile;
                    this._SaveFile = value;

                    if (this._SaveFile != null)
                    {
                        this._SaveFile.Player.PropertyChanged += this.OnPlayerPropertyChanged;
                        this._SaveFile.Player.Appearance.PropertyChanged += this.OnPlayerAppearancePropertyChanged;

                        this.rootPropertyGrid.SelectedObject = value;
                        this.saveFileBindingSource.DataSource = value;
                        this.vectorParametersBindingSource.DataSource =
                            value.Player.Appearance.MorphHead.VectorParameters;

                        this.playerRootTabPage.ImageKey =
                            this._SaveFile.Player.IsFemale == false
                                ? "Tab_Player_Root_Male"
                                : "Tab_Player_Root_Female";
                    }
                }
            }
        }

        private void OnPlayerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // goofy?

            if (e.PropertyName == "IsFemale")
            {
                if (this._SaveFile == null)
                {
                    // ReSharper disable LocalizableElement
                    this.playerRootTabPage.ImageKey = "Tab_Player_Root_Male";
                    // ReSharper restore LocalizableElement
                }
                else
                {
                    this.playerRootTabPage.ImageKey =
                        this._SaveFile.Player.IsFemale == false
                            ? "Tab_Player_Root_Male"
                            : "Tab_Player_Root_Female";
                }
            }
        }

        private void OnPlayerAppearancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MorphHead")
            {
                this.vectorParametersBindingSource.DataSource =
                    this._SaveFile.Player.Appearance.MorphHead.VectorParameters;
            }
        }

        private void LoadSaveFromStream(Stream stream)
        {
            if (stream.ReadValueU32(Endian.Big) == 0x434F4E20)
            {
                MessageBox.Show(Localization.Editor_CannotLoadXbox360CONFile,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            stream.Seek(-4, SeekOrigin.Current);

            FileFormats.SFXSaveGameFile saveFile;
            try
            {
                saveFile = FileFormats.SFXSaveGameFile.Read(stream);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(CultureInfo.InvariantCulture, Localization.Editor_SaveReadException, e),
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (saveFile.Version < 59)
            {
                MessageBox.Show(
                    Localization.Editor_SaveFileTooOld,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.SaveFile = saveFile;
        }


        private void OnSaveNewMale(object sender, EventArgs e)
        {
            this.LoadDefaultMaleSave();
        }

        private void OnSaveNewFemale(object sender, EventArgs e)
        {
            this.LoadDefaultFemaleSave();
        }

        private void OnSaveOpenFromGeneric(object sender, EventArgs e)
        {
            if (this.dontUseCareerPickerToolStripMenuItem.Checked == false)
            {
                this.OnSaveOpenFromCareer(sender, e);
            }
            else
            {
                this.OnSaveOpenFromFile(sender, e);
            }
        }

        private void OnSaveOpenFromCareer(object sender, EventArgs e)
        {
            using (var picker = new SavePicker())
            {
                picker.Owner = this;
                picker.FileMode = SavePicker.PickerMode.Load;
                picker.FilePath = this._SavePath;

                var result = picker.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrEmpty(picker.SelectedPath) == true)
                {
                    MessageBox.Show(
                        Localization.Editor_ThisShouldNeverHappen,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                using (var input = File.OpenRead(picker.SelectedPath))
                {
                    this.LoadSaveFromStream(input);
                }
            }
        }

        private void OnSaveOpenFromFile(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var input = this.openFileDialog.OpenFile())
            {
                this.LoadSaveFromStream(input);
            }
        }

        private void OnSaveSaveToGeneric(object sender, EventArgs e)
        {
            if (this.dontUseCareerPickerToolStripMenuItem.Checked == false)
            {
                this.OnSaveSaveToCareer(sender, e);
            }
            else
            {
                this.OnSaveSaveToFile(sender, e);
            }
        }

        private void OnSaveSaveToFile(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.saveFileDialog.FilterIndex =
                this.SaveFile.Endian == Endian.Little ? 1 : 2;
            if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.SaveFile.Endian = this.saveFileDialog.FilterIndex != 2
                                       ? Endian.Little
                                       : Endian.Big;
            using (var output = this.saveFileDialog.OpenFile())
            {
                FileFormats.SFXSaveGameFile.Write(this.SaveFile, output);
            }
        }

        private void OnSaveSaveToCareer(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using (var picker = new SavePicker())
            {
                picker.Owner = this;
                picker.FileMode = SavePicker.PickerMode.Save;
                picker.FilePath = this._SavePath;
                picker.SaveFile = this.SaveFile;

                var result = picker.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrEmpty(picker.SelectedPath) == false)
                {
                    var selectedDirectory = Path.GetDirectoryName(picker.SelectedPath);
                    if (selectedDirectory != null)
                    {
                        Directory.CreateDirectory(selectedDirectory);
                    }

                    using (var output = File.Create(picker.SelectedPath))
                    {
                        FileFormats.SFXSaveGameFile.Write(this.SaveFile, output);
                    }
                }
                else
                {
                    MessageBox.Show(
                        Localization.Editor_ThisShouldNeverHappen,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void OnSelectedGridItemChanged(
            object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.OldSelection != null)
            {
                var oldPc = e.OldSelection.Value as INotifyPropertyChanged;
                if (oldPc != null)
                {
                    oldPc.PropertyChanged -= this.OnPropertyChanged;
                }
            }

            if (e.NewSelection != null)
            {
                if ((e.NewSelection.Value is FileFormats.Unreal.ISerializable) == true)
                {
                    this.childPropertyGrid.SelectedObject = e.NewSelection.Value;
                    this.rawSplitContainer.Panel2Collapsed = false;

                    var newPc = e.NewSelection.Value as INotifyPropertyChanged;
                    if (newPc != null)
                    {
                        newPc.PropertyChanged += this.OnPropertyChanged;
                    }

                    return;
                }
            }

            this.childPropertyGrid.SelectedObject = null;
            this.rawSplitContainer.Panel2Collapsed = true;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.rootPropertyGrid.Refresh();
        }

        private const string HeadMorphMagic = "GIBBEDMASSEFFECT3HEADMORPH";

        private void OnImportHeadMorph(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.openHeadMorphDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var input = this.openHeadMorphDialog.OpenFile())
            {
                if (input.ReadString(HeadMorphMagic.Length, Encoding.ASCII) != HeadMorphMagic)
                {
                    MessageBox.Show(
                        Localization.Editor_HeadMorphInvalid,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                if (input.ReadValueU8() != 0)
                {
                    MessageBox.Show(
                        Localization.Editor_HeadMorphVersionUnsupported,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                uint version = input.ReadValueU32();

                if (version != this.SaveFile.Version)
                {
                    if (MessageBox.Show(
                        string.Format(
                            Localization.Editor_HeadMorphVersionMaybeIncompatible,
                            version,
                            this.SaveFile.Version),
                        Localization.Question,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        input.Close();
                        return;
                    }
                }

                var reader = new FileFormats.Unreal.FileReader(
                    input, version, Endian.Little);
                var morphHead = new FileFormats.Save.MorphHead();
                morphHead.Serialize(reader);
                this.SaveFile.Player.Appearance.MorphHead = morphHead;
                this.SaveFile.Player.Appearance.HasMorphHead = true;
            }
        }

        private void OnExportHeadMorph(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.SaveFile.Player.Appearance.HasMorphHead == false)
            {
                MessageBox.Show(
                    Localization.Editor_NoHeadMorph,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.saveHeadMorphDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var output = this.saveHeadMorphDialog.OpenFile())
            {
                output.WriteString(HeadMorphMagic, Encoding.ASCII);
                output.WriteValueU8(0); // "version" in case I break something in the future
                output.WriteValueU32(this.SaveFile.Version);
                var writer = new FileFormats.Unreal.FileWriter(
                    output, this.SaveFile.Version, Endian.Little);
                this.SaveFile.Player.Appearance.MorphHead.Serialize(writer);
            }
        }

        private const string HeadMorphMagicLegacy = "GIBBEDMASSEFFECT2HEADMORPH";

        private void OnImportHeadMorphLegacy(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(
                Localization.Editor_HeadMorphLegacy,
                Localization.Warning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            if (this.openHeadMorphLegacyDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var input = this.openHeadMorphLegacyDialog.OpenFile())
            {
                if (input.ReadString(HeadMorphMagicLegacy.Length, Encoding.ASCII) != HeadMorphMagicLegacy)
                {
                    MessageBox.Show(
                        Localization.Editor_HeadMorphInvalid,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                if (input.ReadValueU8() != 0)
                {
                    MessageBox.Show(
                        Localization.Editor_HeadMorphVersionUnsupported,
                        Localization.Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                uint version = input.ReadValueU32();

                if (version != 29)
                {
                    if (MessageBox.Show(
                        string.Format(
                            Localization.Editor_HeadMorphVersionMaybeIncompatible,
                            version,
                            this.SaveFile.Version),
                        Localization.Question,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        input.Close();
                        return;
                    }
                }

                var reader = new FileFormats.Unreal.FileReader(
                    input, version, Endian.Little);
                var morphHead = new FileFormats.Save.MorphHead();
                morphHead.Serialize(reader);
                this.SaveFile.Player.Appearance.MorphHead = morphHead;
                this.SaveFile.Player.Appearance.HasMorphHead = true;
            }
        }

        private void AppendToPlotManualLog(string format, params object[] args)
        {
            if (string.IsNullOrEmpty(this.plotManualLogTextBox.Text) == false)
            {
                this.plotManualLogTextBox.AppendText(Environment.NewLine);
            }
            this.plotManualLogTextBox.AppendText(
                string.Format(
                    Thread.CurrentThread.CurrentCulture,
                    format,
                    args));
        }

        private void OnPlotManualGetBool(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualBoolIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var value = this.SaveFile.Plot.GetBoolVariable(id);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogBoolGet,
                                       id,
                                       value);
            this.plotManualBoolValueCheckBox.Checked = value;
        }

        private void OnPlotManualSetBool(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualBoolIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var newValue = this.plotManualBoolValueCheckBox.Checked;
            var oldValue = this.SaveFile.Plot.GetBoolVariable(id);
            this.SaveFile.Plot.SetBoolVariable(id, newValue);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogBoolSet,
                                       id,
                                       newValue,
                                       oldValue);
        }

        private void OnPlotManualGetInt(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualIntIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var value = this.SaveFile.Plot.GetIntVariable(id);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogIntGet,
                                       id,
                                       value);
            this.plotManualIntValueTextBox.Text =
                value.ToString(Thread.CurrentThread.CurrentCulture);
        }

        private void OnPlotManualSetInt(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualIntIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int newValue;
            if (int.TryParse(
                this.plotManualIntValueTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out newValue) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseValue,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var oldValue = this.SaveFile.Plot.GetIntVariable(id);
            this.SaveFile.Plot.SetIntVariable(id, newValue);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogIntSet,
                                       id,
                                       newValue,
                                       oldValue);
        }

        private void OnPlotManualGetFloat(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualFloatIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var value = this.SaveFile.Plot.GetFloatVariable(id);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogFloatGet,
                                       id,
                                       value);
            this.plotManualFloatValueTextBox.Text =
                value.ToString(Thread.CurrentThread.CurrentCulture);
        }

        private void OnPlotManualSetFloat(object sender, EventArgs e)
        {
            if (this.SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int id;
            if (int.TryParse(
                this.plotManualFloatIdTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out id) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseId,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            float newValue;
            if (float.TryParse(
                this.plotManualIntValueTextBox.Text,
                NumberStyles.None,
                Thread.CurrentThread.CurrentCulture,
                out newValue) == false)
            {
                MessageBox.Show(Localization.Editor_FailedToParseValue,
                                Localization.Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var oldValue = this.SaveFile.Plot.GetFloatVariable(id);
            this.SaveFile.Plot.SetFloatVariable(id, newValue);
            this.AppendToPlotManualLog(Localization.Editor_PlotManualLogFloatSet,
                                       id,
                                       newValue,
                                       oldValue);
        }

        private void OnPlotManualClearLog(object sender, EventArgs e)
        {
            this.plotManualLogTextBox.Clear();
        }

        private void OnLinkFaq(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/me3tools/wiki/FAQ");
        }

        private void OnLinkIssues(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/me3tools/wiki/IssuesNotice?tm=3");
        }

        private static int GetTotalTalentPoints(int level)
        {
            int points = 0;

            for (int current = 1; current <= level; current++)
            {
                if (current == 1)
                {
                    points += 3;
                }
                else if (current >= 2 && current <= 30)
                {
                    points += 2;
                }
                else if (current >= 31 && current <= 60)
                {
                    points += 4;
                }
            }

            return points;
        }

        private void OnBalanceTalentPoints(object sender, EventArgs e)
        {
            int spentPoints = 0;
            foreach (var power in this.SaveFile.Player.Powers)
            {
                var currentRank = (int)power.CurrentRank;
                while (currentRank > 0)
                {
                    spentPoints += currentRank;
                    currentRank--;
                }
            }

            int totalPoints = GetTotalTalentPoints(this.SaveFile.Player.Level);
            this.SaveFile.Player.TalentPoints = totalPoints - spentPoints;
        }

        private static void ApplyAppearancePreset(FileFormats.Save.MorphHead morphHead,
                                                  AppearancePreset preset)
        {
            if (morphHead == null)
            {
                throw new ArgumentNullException("morphHead");
            }

            if (preset == null)
            {
                throw new ArgumentNullException("preset");
            }

            if (string.IsNullOrEmpty(preset.HairMesh) == false)
            {
                morphHead.HairMesh = preset.HairMesh;
            }

            if (preset.Scalars != null)
            {
                if (preset.Scalars.Clear == true)
                {
                    morphHead.ScalarParameters.Clear();
                }

                if (preset.Scalars.Remove != null)
                {
                    foreach (var scalar in preset.Scalars.Remove)
                    {
                        morphHead.ScalarParameters.RemoveAll(
                            p => string.Compare(p.Name, scalar, StringComparison.InvariantCultureIgnoreCase) == 0);
                    }
                }

                if (preset.Scalars.Add != null)
                {
                    foreach (var scalar in preset.Scalars.Add)
                    {
                        morphHead.ScalarParameters.Add(
                            new FileFormats.Save.MorphHead.ScalarParameter()
                            {
                                Name = scalar.Key,
                                Value = scalar.Value,
                            });
                    }
                }

                if (preset.Scalars.Set != null)
                {
                    foreach (var scalar in preset.Scalars.Set)
                    {
                        morphHead.ScalarParameters.RemoveAll(
                            p => string.Compare(p.Name, scalar.Key, StringComparison.InvariantCultureIgnoreCase) == 0);
                        morphHead.ScalarParameters.Add(
                            new FileFormats.Save.MorphHead.ScalarParameter()
                            {
                                Name = scalar.Key,
                                Value = scalar.Value,
                            });
                    }
                }
            }

            if (preset.Textures != null)
            {
                if (preset.Textures.Clear == true)
                {
                    morphHead.TextureParameters.Clear();
                }

                if (preset.Textures.Remove != null)
                {
                    foreach (var texture in preset.Textures.Remove)
                    {
                        morphHead.TextureParameters.RemoveAll(
                            p => string.Compare(p.Name, texture, StringComparison.InvariantCultureIgnoreCase) == 0);
                    }
                }

                if (preset.Textures.Add != null)
                {
                    foreach (var texture in preset.Textures.Add)
                    {
                        morphHead.TextureParameters.Add(
                            new FileFormats.Save.MorphHead.TextureParameter()
                            {
                                Name = texture.Key,
                                Value = texture.Value,
                            });
                    }
                }

                if (preset.Textures.Set != null)
                {
                    foreach (var texture in preset.Textures.Set)
                    {
                        morphHead.TextureParameters.RemoveAll(
                            p => string.Compare(p.Name, texture.Key, StringComparison.InvariantCultureIgnoreCase) == 0);
                        morphHead.TextureParameters.Add(
                            new FileFormats.Save.MorphHead.TextureParameter()
                            {
                                Name = texture.Key,
                                Value = texture.Value,
                            });
                    }
                }
            }

            if (preset.Vectors != null)
            {
                if (preset.Vectors.Clear == true)
                {
                    morphHead.VectorParameters.Clear();
                }

                if (preset.Vectors.Remove != null)
                {
                    foreach (var vector in preset.Vectors.Remove)
                    {
                        string temp = vector;
                        morphHead.VectorParameters.RemoveAll(
                            p => string.Compare(p.Name, temp, StringComparison.InvariantCultureIgnoreCase) == 0);
                    }
                }

                if (preset.Vectors.Add != null)
                {
                    foreach (var vector in preset.Vectors.Add)
                    {
                        morphHead.VectorParameters.Add(
                            new FileFormats.Save.MorphHead.VectorParameter()
                            {
                                Name = vector.Key,
                                Value = new FileFormats.Save.LinearColor()
                                {
                                    R = vector.Value.R,
                                    G = vector.Value.G,
                                    B = vector.Value.B,
                                    A = vector.Value.A,
                                },
                            });
                    }
                }

                if (preset.Vectors.Set != null)
                {
                    foreach (var vector in preset.Vectors.Set)
                    {
                        var temp = vector;
                        morphHead.VectorParameters.RemoveAll(
                            p => string.Compare(p.Name, temp.Key, StringComparison.InvariantCultureIgnoreCase) == 0);
                        morphHead.VectorParameters.Add(
                            new FileFormats.Save.MorphHead.VectorParameter()
                            {
                                Name = vector.Key,
                                Value = new FileFormats.Save.LinearColor()
                                {
                                    R = vector.Value.R,
                                    G = vector.Value.G,
                                    B = vector.Value.B,
                                    A = vector.Value.A,
                                },
                            });
                    }
                }
            }
        }

        private void OnLoadAppearancePresetFromFile(object sender, EventArgs e)
        {
            if (this._SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this._SaveFile.Player.Appearance.HasMorphHead == false)
            {
                MessageBox.Show(
                    Localization.Editor_NoHeadMorph,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.openAppearancePresetFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string text;
            using (var input = this.openAppearancePresetFileDialog.OpenFile())
            {
                var reader = new StreamReader(input);
                text = reader.ReadToEnd();
            }

            var preset = JsonConvert.DeserializeObject<AppearancePreset>(text);
            ApplyAppearancePreset(this._SaveFile.Player.Appearance.MorphHead, preset);
        }

        private void OnSaveAppearancePresetToFile(object sender, EventArgs e)
        {
            if (this.saveAppearancePresetFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (this._SaveFile == null)
            {
                MessageBox.Show(
                    Localization.Editor_NoActiveSave,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this._SaveFile.Player.Appearance.HasMorphHead == false)
            {
                MessageBox.Show(
                    Localization.Editor_NoHeadMorph,
                    Localization.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var headMorph = this.SaveFile.Player.Appearance.MorphHead;

            // ReSharper disable UseObjectOrCollectionInitializer
            var preset = new AppearancePreset();
            // ReSharper restore UseObjectOrCollectionInitializer

            preset.HairMesh = headMorph.HairMesh;

            foreach (var scalar in headMorph.ScalarParameters)
            {
                preset.Scalars.Set.Add(new KeyValuePair<string, float>(scalar.Name, scalar.Value));
            }

            foreach (var texture in headMorph.TextureParameters)
            {
                preset.Textures.Set.Add(new KeyValuePair<string, string>(texture.Name, texture.Value));
            }

            foreach (var vector in headMorph.VectorParameters)
            {
                preset.Vectors.Set.Add(new KeyValuePair<string, AppearancePreset.
                                           LinearColor>(vector.Name,
                                                        new AppearancePreset.
                                                            LinearColor()
                                                        {
                                                            R = vector.Value.R,
                                                            G = vector.Value.G,
                                                            B = vector.Value.B,
                                                            A = vector.Value.A,
                                                        }));
            }

            using (var output = File.Create(this.saveAppearancePresetFileDialog.FileName))
            {
                var writer = new StreamWriter(output);
                writer.Write(JsonConvert.SerializeObject(
                    preset, Formatting.Indented));
                writer.Flush();
            }
        }

        private static ColorBgra LinearColorToBgra(FileFormats.Save.LinearColor linearColor)
        {
            return LinearColorToBgra(
                linearColor.R,
                linearColor.G,
                linearColor.B,
                linearColor.A);
        }

        private static ColorBgra LinearColorToBgra(float r, float g, float b, float a)
        {
            var rb = (byte)Math.Round(r * 255);
            var gb = (byte)Math.Round(g * 255);
            var bb = (byte)Math.Round(b * 255);
            var ab = (byte)Math.Round(a * 255);
            return ColorBgra.FromBgra(bb, gb, rb, ab);
        }

        private static FileFormats.Save.LinearColor BgraToLinearColor(ColorBgra bgra)
        {
            return new FileFormats.Save.LinearColor(
                (float)bgra.R / 255,
                (float)bgra.G / 255,
                (float)bgra.B / 255,
                (float)bgra.A / 255);
        }

        private void OnDrawColorListBoxItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            var g = e.Graphics;
            var listbox = (ListBox)sender;

            var backColor = (e.State & DrawItemState.Selected) != 0
                                ? SystemColors.Highlight
                                : listbox.BackColor;
            var foreColor = (e.State & DrawItemState.Selected) != 0
                                ? SystemColors.HighlightText
                                : listbox.ForeColor;

            g.FillRectangle(new SolidBrush(backColor), e.Bounds);

            var colorBounds = e.Bounds;

            colorBounds.Width = 30;
            colorBounds.Height -= 4;
            colorBounds.X += 2;
            colorBounds.Y += 2;

            var textBounds = e.Bounds;
            textBounds.Offset(30, 0);
            textBounds.Inflate(-2, -2);
            var textBoundsF = new RectangleF(textBounds.X, textBounds.Y, textBounds.Width, textBounds.Height);

            g.FillRectangle(
                new HatchBrush(
                    HatchStyle.LargeCheckerBoard,
                    Color.White,
                    Color.Gray),
                colorBounds);

            var item = listbox.Items[e.Index];

            var vector = item as FileFormats.Save.MorphHead.VectorParameter;
            if (vector != null)
            {
                var valueColor = LinearColorToBgra(vector.Value).ToColor();

                g.FillRectangle(new SolidBrush(valueColor), colorBounds);
                g.DrawRectangle(Pens.Black, colorBounds);

                var format = StringFormat.GenericDefault;
                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(vector.Name,
                                      listbox.Font,
                                      new SolidBrush(foreColor),
                                      textBoundsF,
                                      format);
            }
        }

        private void OnPlayerAppearanceColorRemove(object sender, EventArgs e)
        {
            var item = this.playerAppearanceColorsListBox.SelectedItem as FileFormats.Save.MorphHead.VectorParameter;
            if (item != null)
            {
                this._SaveFile.Player.Appearance.MorphHead.VectorParameters.Remove(item);
            }
        }

        private void OnPlayerAppearanceColorAdd(object sender, EventArgs e)
        {
            var input = new InputBox
            {
                Owner = this,
                Text = Localization.Editor_ColorName,
                InputText = "",
            };

            if (input.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this._SaveFile.Player.Appearance.MorphHead.VectorParameters.Add(
                new FileFormats.Save.MorphHead.VectorParameter()
                {
                    Name = input.InputText,
                    Value = new FileFormats.Save.LinearColor(1, 1, 1, 1),
                });
        }

        private void OnPlayerAppearanceColorChange(object sender, EventArgs e)
        {
            var item = this.playerAppearanceColorsListBox.SelectedItem as FileFormats.Save.MorphHead.VectorParameter;
            if (item != null)
            {
                var bgra = LinearColorToBgra(item.Value);

// ReSharper disable UseObjectOrCollectionInitializer
                var picker = new ColorPicker.ColorDialog();
// ReSharper restore UseObjectOrCollectionInitializer
                picker.WheelColor = bgra;

                if (picker.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                item.Value = BgraToLinearColor(picker.WheelColor);
            }
        }
    }
}
