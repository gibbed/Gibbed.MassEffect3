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
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Gibbed.IO;
using Gibbed.MassEffect3.SaveEdit.Resources;

namespace Gibbed.MassEffect3.SaveEdit
{
    public partial class Editor : Form
    {
        private readonly string _SavePath;
        private FileFormats.SaveFile _SaveFile;

        private FileFormats.SaveFile SaveFile
        {
            get { return this._SaveFile; }
            set
            {
                if (value != this._SaveFile)
                {
                    this._SaveFile = value;
                    /*var dtd = DynamicTypeDescriptor.ProviderInstaller.Install(value);
                    this.rootPropertyGrid.Site = dtd.GetSite();*/
                    this.rootPropertyGrid.SelectedObject = value;
                }
            }
        }

        public Editor()
        {
            this._SavePath = null;
            this.InitializeComponent();

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

            string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            savePath = Path.Combine(savePath, "BioWare");
            savePath = Path.Combine(savePath, "Mass Effect 3");
            savePath = Path.Combine(savePath, "Save");

            if (Directory.Exists(savePath) == true)
            {
                this._SavePath = savePath;
                this.openFileDialog.InitialDirectory = savePath;
                this.saveFileDialog.InitialDirectory = savePath;
            }
            else
            {
                this.dontUseCareerPickerToolStripMenuItem.Checked = true;
                this.dontUseCareerPickerToolStripMenuItem.Enabled = false;
                this.openFromCareerMenuItem.Enabled = false;
                this.saveToCareerMenuItem.Enabled = false;
            }
            
            // ReSharper disable LocalizableElement
            this.iconImageList.Images.Add("Unknown", new System.Drawing.Bitmap(16, 16));
            // ReSharper restore LocalizableElement

            this.rootTabControl.SelectedTab = rawTabPage;
            this.rawSplitContainer.Panel2Collapsed = true;
        }

        private void LoadSaveFromStream(Stream stream)
        {
            var saveFile = FileFormats.SaveFile.Read(stream);
            this.SaveFile = saveFile;
        }

        private void OnOpenFromGeneric(object sender, EventArgs e)
        {
            if (this.dontUseCareerPickerToolStripMenuItem.Checked == false)
            {
                this.OnOpenFromCareer(sender, e);
            }
            else
            {
                this.OnOpenFromFile(sender, e);
            }
        }

        private void OnOpenFromCareer(object sender, EventArgs e)
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

                if (string.IsNullOrEmpty(picker.SelectedPath) == false)
                {
                    using (var input = File.OpenRead(picker.SelectedPath))
                    {
                        this.LoadSaveFromStream(input);
                    }
                }
                else
                {
                    MessageBox.Show(
                        Localization.Editor_ThisShouldNeverHappen,
                        Localization.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnOpenFromFile(object sender, EventArgs e)
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

        private void OnSaveToGeneric(object sender, EventArgs e)
        {
            if (this.dontUseCareerPickerToolStripMenuItem.Checked == false)
            {
                this.OnSaveToCareer(sender, e);
            }
            else
            {
                this.OnSaveToFile(sender, e);
            }
        }

        private void OnSaveToFile(object sender, EventArgs e)
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

            this.SaveFile.Endian = this.saveFileDialog.FilterIndex != 2 ?
                Endian.Little : Endian.Big;
            using (var output = this.saveFileDialog.OpenFile())
            {
                FileFormats.SaveFile.Write(this.SaveFile, output);
            }
        }

        private void OnSaveToCareer(object sender, EventArgs e)
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
                    Directory.CreateDirectory(Path.GetDirectoryName(picker.SelectedPath));
                    using (var output = File.Create(picker.SelectedPath))
                    {
                        FileFormats.SaveFile.Write(this.SaveFile, output);
                    }
                }
                else
                {
                    MessageBox.Show(
                        Localization.Editor_ThisShouldNeverHappen,
                        Localization.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnSelectedGridItemChanged(
            object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.OldSelection != null)
            {
                var oldPC = e.OldSelection.Value as INotifyPropertyChanged;
                if (oldPC != null)
                {
                    oldPC.PropertyChanged -= this.OnPropertyChanged;
                }
            }

            if (e.NewSelection != null)
            {
                if ((e.NewSelection.Value is FileFormats.Unreal.ISerializable) == true)
                {
                    this.childPropertyGrid.SelectedObject = e.NewSelection.Value;
                    this.rawSplitContainer.Panel2Collapsed = false;

                    var newPC = e.NewSelection.Value as INotifyPropertyChanged;
                    if (newPC != null)
                    {
                        newPC.PropertyChanged += this.OnPropertyChanged;
                    }

                    return;
                }
            }

            this.childPropertyGrid.SelectedObject = null;
            this.rawSplitContainer.Panel2Collapsed = true;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
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

            if (this.SaveFile.Player.Appearance.HasMorphHead == false ||
                this.SaveFile.Player.Appearance.MorphHead == null)
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
    }
}
