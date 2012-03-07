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
using Gibbed.IO;

namespace Gibbed.MassEffect3.SaveEdit
{
    public partial class Editor : Form
    {
        private static string GetExecutablePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private DynamicTypeDescriptor.DynamicCustomTypeDescriptor CustomTypeDescriptor;
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
            this.playerBasicTabPage.ImageKey = "Tab_Player_Basic";
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
    }
}
