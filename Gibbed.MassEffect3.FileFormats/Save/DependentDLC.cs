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

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DependentDLC : Unreal.ISerializable, INotifyPropertyChanged
    {
        private int _ModuleID;
        private string _Name;
        private string _CanonicalName;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._ModuleID);
            stream.Serialize(ref this._Name);
            stream.Serialize(ref this._CanonicalName, (s) => s.Version < 50, () => null);
        }

        public override string ToString()
        {
            return String.Format("{1} ({0})",
                this._ModuleID,
                this._Name);
        }

        #region Properties
        public int ModuleID
        {
            get { return this._ModuleID; }
            set
            {
                if (value != this._ModuleID)
                {
                    this._ModuleID = value;
                    this.NotifyPropertyChanged("ModuleID");
                }
            }
        }

        public string Name
        {
            get { return this._Name; }
            set
            {
                if (value != this._Name)
                {
                    this._Name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public string CanonicalName
        {
            get { return this._CanonicalName; }
            set
            {
                if (value != this._CanonicalName)
                {
                    this._CanonicalName = value;
                    this.NotifyPropertyChanged("CanonicalName");
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
