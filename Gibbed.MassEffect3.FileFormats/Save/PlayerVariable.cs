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
    public class PlayerVariable : Unreal.ISerializable, INotifyPropertyChanged
    {
        private string _VariableName;
        private int _VariableValue;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._VariableName);
            stream.Serialize(ref this._VariableValue);
        }

        // for CollectionEditor
        public string Name { get { return this._VariableName; } }
        public override string ToString()
        {
            return this.Name ?? "(null)";
        }

        #region Properties
        [DisplayName("Name")]
        public string VariableName
        {
            get { return this._VariableName; }
            set
            {
                if (value != this._VariableName)
                {
                    this._VariableName = value;
                    this.NotifyPropertyChanged("VariableName");
                }
            }
        }

        [DisplayName("Value")]
        public int VariableValue
        {
            get { return this._VariableValue; }
            set
            {
                if (value != this._VariableValue)
                {
                    this._VariableValue = value;
                    this.NotifyPropertyChanged("VariableValue");
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
