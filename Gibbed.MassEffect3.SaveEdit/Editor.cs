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
            this.rootTabControl.SelectedTab = rawTabPage;
            this.splitContainer1.Panel2Collapsed = true;
        }

        private void LoadSaveFromStream(Stream stream)
        {
            var saveFile = FileFormats.SaveFile.Load(stream);
            this.SaveFile = saveFile;
        }

        private void OnOpenFile(object sender, EventArgs e)
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

        private void rootPropertyGrid_SelectedGridItemChanged(
            object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.OldSelection != null)
            {
                var oldPC = e.OldSelection.Value as INotifyPropertyChanged;
                if (oldPC != null)
                {
                    oldPC.PropertyChanged -= new PropertyChangedEventHandler(pc_PropertyChanged);
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
                        newPC.PropertyChanged += new PropertyChangedEventHandler(pc_PropertyChanged);
                    }

                    return;
                }
            }

            this.childPropertyGrid.SelectedObject = null;
            this.splitContainer1.Panel2Collapsed = true;
        }

        void pc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.rootPropertyGrid.Refresh();
        }
    }
}
