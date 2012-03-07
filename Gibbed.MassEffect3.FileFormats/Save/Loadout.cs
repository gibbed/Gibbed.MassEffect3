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

using System.ComponentModel;

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Loadout : Unreal.ISerializable, INotifyPropertyChanged
    {
        private string _Unknown0;
        private string _Unknown1;
        private string _Unknown2;
        private string _Unknown3;
        private string _Unknown4;
        private string _Unknown5;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._Unknown0);
            stream.Serialize(ref this._Unknown1);
            stream.Serialize(ref this._Unknown2);
            stream.Serialize(ref this._Unknown3);
            stream.Serialize(ref this._Unknown4);
            stream.Serialize(ref this._Unknown5);
        }

        #region Properties
        public string Unknown0
        {
            get { return this._Unknown0; }
            set
            {
                if (value != this._Unknown0)
                {
                    this._Unknown0 = value;
                    this.NotifyPropertyChanged("Unknown0");
                }
            }
        }

        public string Unknown1
        {
            get { return this._Unknown1; }
            set
            {
                if (value != this._Unknown1)
                {
                    this._Unknown1 = value;
                    this.NotifyPropertyChanged("Unknown1");
                }
            }
        }

        public string Unknown2
        {
            get { return this._Unknown2; }
            set
            {
                if (value != this._Unknown2)
                {
                    this._Unknown2 = value;
                    this.NotifyPropertyChanged("Unknown2");
                }
            }
        }

        public string Unknown3
        {
            get { return this._Unknown3; }
            set
            {
                if (value != this._Unknown3)
                {
                    this._Unknown3 = value;
                    this.NotifyPropertyChanged("Unknown3");
                }
            }
        }

        public string Unknown4
        {
            get { return this._Unknown4; }
            set
            {
                if (value != this._Unknown4)
                {
                    this._Unknown4 = value;
                    this.NotifyPropertyChanged("Unknown4");
                }
            }
        }

        public string Unknown5
        {
            get { return this._Unknown5; }
            set
            {
                if (value != this._Unknown5)
                {
                    this._Unknown5 = value;
                    this.NotifyPropertyChanged("Unknown5");
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
