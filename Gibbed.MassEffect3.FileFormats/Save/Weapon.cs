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
    public class Weapon : Unreal.ISerializable, INotifyPropertyChanged
    {
        private string _WeaponClassName;
        private int _AmmoUsedCount;
        private int _TotalAmmo;
        private bool _CurrentWeapon;
        private bool _LastWeapon;
        private string _AmmoPowerName;
        private string _AmmoPowerSourceTag;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._WeaponClassName);
            stream.Serialize(ref this._AmmoUsedCount);
            stream.Serialize(ref this._TotalAmmo);
            stream.Serialize(ref this._CurrentWeapon);
            stream.Serialize(ref this._LastWeapon);
            stream.Serialize(ref this._AmmoPowerName, (s) => s.Version < 17, () => null);
            stream.Serialize(ref this._AmmoPowerSourceTag, (s) => s.Version < 59, () => null);
        }

        public override string ToString()
        {
            return this._WeaponClassName;
        }

        #region Properties
        [DisplayName("Class Name")]
        public string WeaponClassName
        {
            get { return this._WeaponClassName; }
            set
            {
                if (value != this._WeaponClassName)
                {
                    this._WeaponClassName = value;
                    this.NotifyPropertyChanged("WeaponClassName");
                }
            }
        }

        [DisplayName("Ammo Used Count")]
        public int AmmoUsedCount
        {
            get { return this._AmmoUsedCount; }
            set
            {
                if (value != this._AmmoUsedCount)
                {
                    this._AmmoUsedCount = value;
                    this.NotifyPropertyChanged("AmmoUsedCount");
                }
            }
        }

        [DisplayName("Ammo Total")]
        public int TotalAmmo
        {
            get { return this._TotalAmmo; }
            set
            {
                if (value != this._TotalAmmo)
                {
                    this._TotalAmmo = value;
                    this.NotifyPropertyChanged("TotalAmmo");
                }
            }
        }

        [DisplayName("Is Current Weapon")]
        public bool CurrentWeapon
        {
            get { return this._CurrentWeapon; }
            set
            {
                if (value != this._CurrentWeapon)
                {
                    this._CurrentWeapon = value;
                    this.NotifyPropertyChanged("CurrentWeapon");
                }
            }
        }

        [DisplayName("Was Last Weapon")]
        public bool LastWeapon
        {
            get { return this._LastWeapon; }
            set
            {
                if (value != this._LastWeapon)
                {
                    this._LastWeapon = value;
                    this.NotifyPropertyChanged("LastWeapon");
                }
            }
        }

        [DisplayName("Ammo Power Name")]
        public string AmmoPowerName
        {
            get { return this._AmmoPowerName; }
            set
            {
                if (value != this._AmmoPowerName)
                {
                    this._AmmoPowerName = value;
                    this.NotifyPropertyChanged("AmmoPowerName");
                }
            }
        }

        [DisplayName("Ammo Power Source Tag")]
        public string AmmoPowerSourceTag
        {
            get { return this._AmmoPowerSourceTag; }
            set
            {
                if (value != this._AmmoPowerSourceTag)
                {
                    this._AmmoPowerSourceTag = value;
                    this.NotifyPropertyChanged("AmmoPowerSourceTag");
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
