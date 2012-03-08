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
using System.Windows.Forms;
using System.Text;
using Gibbed.IO;

namespace Gibbed.MassEffect3.SaveEdit
{
    public partial class Editor : Form
    {
        private static string GetExecutablePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private FileFormats.SaveFile _SaveFile = null;

        private FileFormats.SaveFile SaveFile
        {
            get { return this._SaveFile; }
            set
            {
                if (value != this._SaveFile)
                {
                    this._SaveFile = value;
                    var dtd = DynamicTypeDescriptor.ProviderInstaller.Install(value);
                    this.rootPropertyGrid.Site = dtd.GetSite();
                    this.rootPropertyGrid.SelectedObject = value;
                }
            }
        }

        public Editor()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;

            if (Version.Revision > 0)
            {
                this.Text += String.Format(
                    " (Build revision {0} @ {1})",
                    Version.Revision,
                    Version.Date);
            }

            /* This following block is for Mono-build compatability
             * (ie, compiling this code via Mono and running via .NET)
             * 
             * Mono developers are asstwats:
             *   https://bugzilla.novell.com/show_bug.cgi?id=641826
             * 
             * So, instead of using the ImageListStreamer directly, we'll
             * load images from resources.
             */
            this.iconImageList.Images.Clear();
            this.iconImageList.Images.Add("Unknown", new System.Drawing.Bitmap(16, 16));
            this.iconImageList.Images.Add("Tab_Player_Male", Icons.Male);
            this.iconImageList.Images.Add("Tab_Player_Female", Icons.Female);
            this.iconImageList.Images.Add("Tab_Player_Basic", Icons.XRay);
            this.iconImageList.Images.Add("Tab_Player_Appearance", Icons.Shirt);
            this.iconImageList.Images.Add("Tab_Raw", Icons.Binary);

            this.playerRootTabPage.ImageKey = "Tab_Player_Male";
            this.playerCharacterTabPage.ImageKey = "Tab_Player_Basic";
            this.playerAppearanceTabPage.ImageKey = "Tab_Player_Appearance";

            this.rawTabPage.ImageKey = "Tab_Raw";

            this.rootTabControl.SelectedTab = rawTabPage;
            this.splitContainer1.Panel2Collapsed = true;
        }

        private void LoadSaveFromStream(Stream stream)
        {
            var saveFile = FileFormats.SaveFile.Read(stream);
            this.SaveFile = saveFile;
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

        private void OnSaveToFile(object sender, EventArgs e)
        {
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

        private void OnSelectedGridItemChanged(
            object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.OldSelection != null)
            {
                var oldPC = e.OldSelection.Value as INotifyPropertyChanged;
                if (oldPC != null)
                {
                    oldPC.PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
                }
            }

            if (e.NewSelection != null)
            {
                if ((e.NewSelection.Value is FileFormats.Unreal.ISerializable) == true)
                {
                    this.childPropertyGrid.SelectedObject = e.NewSelection.Value;
                    this.splitContainer1.Panel2Collapsed = false;

                    var newPC = e.NewSelection.Value as INotifyPropertyChanged;
                    if (newPC != null)
                    {
                        newPC.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
                    }

                    return;
                }
            }

            this.childPropertyGrid.SelectedObject = null;
            this.splitContainer1.Panel2Collapsed = true;
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
                    "There is no active save.",
                    "Error",
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
                        "That file does not appear to be an exported head morph.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                if (input.ReadValueU8() != 0)
                {
                    MessageBox.Show(
                        "Unsupported head morph export version.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    input.Close();
                    return;
                }

                uint version = input.ReadValueU32();

                if (version != this.SaveFile.Version)
                {
                    if (MessageBox.Show(
                        String.Format(
                            "The head morph you are importing has a different " +
                            "version ({0}) than your current save file ({1}).\n\n" +
                            "Import anyway?",
                            version,
                            this.SaveFile.Version),
                        "Question",
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
                    "There is no active save.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (this.SaveFile.Player.Appearance.HasMorphHead == false ||
                this.SaveFile.Player.Appearance.MorphHead == null)
            {
                MessageBox.Show(
                    "This save does not have a non-default head morph.",
                    "Error",
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
