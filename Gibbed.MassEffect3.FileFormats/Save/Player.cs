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

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Player : Unreal.ISerializable, INotifyPropertyChanged
    {
        private bool _IsFemale;
        private string _PlayerClassName;
        private bool _CombatPawn;
        private bool _InjuredPawn;
        private bool _UseCasualAppearance;
        private int _Level;
        private float _CurrentXP;
        private string _FirstName;
        private int _LastName;
        private OriginType _Origin;
        private NotorietyType _Notoriety;
        private int _TalentPoints;
        private string _MappedPower1;
        private string _MappedPower2;
        private string _MappedPower3;
        private Save.Appearance _Appearance;
        private List<Power> _Powers;
        private List<GAWAsset> _GAWAssets;
        private List<Weapon> _Weapons;
        private List<WeaponMod> _WeaponMods;
        private Loadout _LoadoutWeapons;
        private string _PrimaryWeapon;
        private string _SecondaryWeapon;
        private List<int> _LoadoutWeaponGroups;
        private List<HotKey> _HotKeys;
        private float _CurrentHealth;
        private int _Credits;
        private int _Medigel;
        private int _Eezo;
        private int _Iridium;
        private int _Palladium;
        private int _Platinum;
        private int _Probes;
        private float _CurrentFuel;
        private int _Grenades;
        private string _FaceCode;
        private int _ClassFriendlyName;
        private Guid _CharacterGUID;
        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._IsFemale);
            stream.Serialize(ref this._PlayerClassName);
            stream.Serialize(ref this._CombatPawn, (s) => s.Version < 37, () => true);
            stream.Serialize(ref this._InjuredPawn, (s) => s.Version < 48, () => false);
            stream.Serialize(ref this._UseCasualAppearance, (s) => s.Version < 48, () => false);
            stream.Serialize(ref this._Level);
            stream.Serialize(ref this._CurrentXP);
            stream.Serialize(ref this._FirstName);
            stream.Serialize(ref this._LastName);
            stream.SerializeEnum(ref this._Origin);
            stream.SerializeEnum(ref this._Notoriety);
            stream.Serialize(ref this._TalentPoints);
            stream.Serialize(ref this._MappedPower1);
            stream.Serialize(ref this._MappedPower2);
            stream.Serialize(ref this._MappedPower3);
            stream.Serialize(ref this._Appearance);
            stream.Serialize(ref this._Powers);
            stream.Serialize(ref this._GAWAssets, (s) => s.Version < 38, () => new List<GAWAsset>());
            stream.Serialize(ref this._Weapons);
            stream.Serialize(ref this._WeaponMods, (s) => s.Version < 32, () => new List<WeaponMod>());
            stream.Serialize(ref this._LoadoutWeapons, (s) => s.Version < 18, () => new Loadout());
            stream.Serialize(ref this._PrimaryWeapon, (s) => s.Version < 41, () => null);
            stream.Serialize(ref this._SecondaryWeapon, (s) => s.Version < 41, () => null);
            stream.Serialize(ref this._LoadoutWeaponGroups, (s) => s.Version < 33, () => new List<int>());
            stream.Serialize(ref this._HotKeys, (s) => s.Version < 19, () => new List<HotKey>());
            stream.Serialize(ref this._CurrentHealth, (s) => s.Version < 44, () => 0.0f);
            stream.Serialize(ref this._Credits);
            stream.Serialize(ref this._Medigel);
            stream.Serialize(ref this._Eezo);
            stream.Serialize(ref this._Iridium);
            stream.Serialize(ref this._Palladium);
            stream.Serialize(ref this._Platinum);
            stream.Serialize(ref this._Probes);
            stream.Serialize(ref this._CurrentFuel);
            stream.Serialize(ref this._Grenades, (s) => s.Version < 54, () => 0);

            if (stream.Version >= 25)
            {
                stream.Serialize(ref this._FaceCode);
            }
            else
            {
                throw new NotSupportedException();
            }

            stream.Serialize(ref this._ClassFriendlyName, (s) => s.Version < 26, () => 0);
            stream.Serialize(ref this._CharacterGUID, (s) => s.Version < 42, () => Guid.Empty);
        }

        public bool IsFemale
        {
            get { return this._IsFemale; }
            set
            {
                if (value != this._IsFemale)
                {
                    this._IsFemale = value;
                    this.NotifyPropertyChanged("IsFemale");
                }
            }
        }

        public string PlayerClassName
        {
            get { return this._PlayerClassName; }
            set
            {
                if (value != this._PlayerClassName)
                {
                    this._PlayerClassName = value;
                    this.NotifyPropertyChanged("PlayerClassName");
                }
            }
        }

        public bool CombatPawn
        {
            get { return this._CombatPawn; }
            set
            {
                if (value != this._CombatPawn)
                {
                    this._CombatPawn = value;
                    this.NotifyPropertyChanged("CombatPawn");
                }
            }
        }

        public bool InjuredPawn
        {
            get { return this._InjuredPawn; }
            set
            {
                if (value != this._InjuredPawn)
                {
                    this._InjuredPawn = value;
                    this.NotifyPropertyChanged("InjuredPawn");
                }
            }
        }

        public bool UseCasualAppearance
        {
            get { return this._UseCasualAppearance; }
            set
            {
                if (value != this._UseCasualAppearance)
                {
                    this._UseCasualAppearance = value;
                    this.NotifyPropertyChanged("UseCasualAppearance");
                }
            }
        }

        public int Level
        {
            get { return this._Level; }
            set
            {
                if (value != this._Level)
                {
                    this._Level = value;
                    this.NotifyPropertyChanged("Level");
                }
            }
        }

        public float CurrentXP
        {
            get { return this._CurrentXP; }
            set
            {
                if (value != this._CurrentXP)
                {
                    this._CurrentXP = value;
                    this.NotifyPropertyChanged("CurrentXP");
                }
            }
        }

        public string FirstName
        {
            get { return this._FirstName; }
            set
            {
                if (value != this._FirstName)
                {
                    this._FirstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }

        public int LastName
        {
            get { return this._LastName; }
            set
            {
                if (value != this._LastName)
                {
                    this._LastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

        public OriginType Origin
        {
            get { return this._Origin; }
            set
            {
                if (value != this._Origin)
                {
                    this._Origin = value;
                    this.NotifyPropertyChanged("Origin");
                }
            }
        }

        public NotorietyType Notoriety
        {
            get { return this._Notoriety; }
            set
            {
                if (value != this._Notoriety)
                {
                    this._Notoriety = value;
                    this.NotifyPropertyChanged("Notoriety");
                }
            }
        }

        public int TalentPoints
        {
            get { return this._TalentPoints; }
            set
            {
                if (value != this._TalentPoints)
                {
                    this._TalentPoints = value;
                    this.NotifyPropertyChanged("TalentPoints");
                }
            }
        }

        public string MappedPower1
        {
            get { return this._MappedPower1; }
            set
            {
                if (value != this._MappedPower1)
                {
                    this._MappedPower1 = value;
                    this.NotifyPropertyChanged("MappedPower1");
                }
            }
        }

        public string MappedPower2
        {
            get { return this._MappedPower2; }
            set
            {
                if (value != this._MappedPower2)
                {
                    this._MappedPower2 = value;
                    this.NotifyPropertyChanged("MappedPower2");
                }
            }
        }

        public string MappedPower3
        {
            get { return this._MappedPower3; }
            set
            {
                if (value != this._MappedPower3)
                {
                    this._MappedPower3 = value;
                    this.NotifyPropertyChanged("MappedPower3");
                }
            }
        }

        public Save.Appearance Appearance
        {
            get { return this._Appearance; }
            set
            {
                if (value != this._Appearance)
                {
                    this._Appearance = value;
                    this.NotifyPropertyChanged("Appearance");
                }
            }
        }

        public List<Power> Powers
        {
            get { return this._Powers; }
            set
            {
                if (value != this._Powers)
                {
                    this._Powers = value;
                    this.NotifyPropertyChanged("Powers");
                }
            }
        }

        public List<GAWAsset> GAWAssets
        {
            get { return this._GAWAssets; }
            set
            {
                if (value != this._GAWAssets)
                {
                    this._GAWAssets = value;
                    this.NotifyPropertyChanged("GAWAssets");
                }
            }
        }

        public List<Weapon> Weapons
        {
            get { return this._Weapons; }
            set
            {
                if (value != this._Weapons)
                {
                    this._Weapons = value;
                    this.NotifyPropertyChanged("Weapons");
                }
            }
        }

        public List<WeaponMod> WeaponMods
        {
            get { return this._WeaponMods; }
            set
            {
                if (value != this._WeaponMods)
                {
                    this._WeaponMods = value;
                    this.NotifyPropertyChanged("WeaponMods");
                }
            }
        }

        public Loadout LoadoutWeapons
        {
            get { return this._LoadoutWeapons; }
            set
            {
                if (value != this._LoadoutWeapons)
                {
                    this._LoadoutWeapons = value;
                    this.NotifyPropertyChanged("LoadoutWeapons");
                }
            }
        }

        public string PrimaryWeapon
        {
            get { return this._PrimaryWeapon; }
            set
            {
                if (value != this._PrimaryWeapon)
                {
                    this._PrimaryWeapon = value;
                    this.NotifyPropertyChanged("PrimaryWeapon");
                }
            }
        }

        public string SecondaryWeapon
        {
            get { return this._SecondaryWeapon; }
            set
            {
                if (value != this._SecondaryWeapon)
                {
                    this._SecondaryWeapon = value;
                    this.NotifyPropertyChanged("SecondaryWeapon");
                }
            }
        }

        public List<int> LoadoutWeaponGroups
        {
            get { return this._LoadoutWeaponGroups; }
            set
            {
                if (value != this._LoadoutWeaponGroups)
                {
                    this._LoadoutWeaponGroups = value;
                    this.NotifyPropertyChanged("LoadoutWeaponGroups");
                }
            }
        }

        public List<HotKey> HotKeys
        {
            get { return this._HotKeys; }
            set
            {
                if (value != this._HotKeys)
                {
                    this._HotKeys = value;
                    this.NotifyPropertyChanged("HotKeys");
                }
            }
        }

        public float CurrentHealth
        {
            get { return this._CurrentHealth; }
            set
            {
                if (value != this._CurrentHealth)
                {
                    this._CurrentHealth = value;
                    this.NotifyPropertyChanged("CurrentHealth");
                }
            }
        }

        public int Credits
        {
            get { return this._Credits; }
            set
            {
                if (value != this._Credits)
                {
                    this._Credits = value;
                    this.NotifyPropertyChanged("Credits");
                }
            }
        }

        public int Medigel
        {
            get { return this._Medigel; }
            set
            {
                if (value != this._Medigel)
                {
                    this._Medigel = value;
                    this.NotifyPropertyChanged("Medigel");
                }
            }
        }

        public int Eezo
        {
            get { return this._Eezo; }
            set
            {
                if (value != this._Eezo)
                {
                    this._Eezo = value;
                    this.NotifyPropertyChanged("Eezo");
                }
            }
        }

        public int Iridium
        {
            get { return this._Iridium; }
            set
            {
                if (value != this._Iridium)
                {
                    this._Iridium = value;
                    this.NotifyPropertyChanged("Iridium");
                }
            }
        }

        public int Palladium
        {
            get { return this._Palladium; }
            set
            {
                if (value != this._Palladium)
                {
                    this._Palladium = value;
                    this.NotifyPropertyChanged("Palladium");
                }
            }
        }

        public int Platinum
        {
            get { return this._Platinum; }
            set
            {
                if (value != this._Platinum)
                {
                    this._Platinum = value;
                    this.NotifyPropertyChanged("Platinum");
                }
            }
        }

        public int Probes
        {
            get { return this._Probes; }
            set
            {
                if (value != this._Probes)
                {
                    this._Probes = value;
                    this.NotifyPropertyChanged("Probes");
                }
            }
        }

        public float CurrentFuel
        {
            get { return this._CurrentFuel; }
            set
            {
                if (value != this._CurrentFuel)
                {
                    this._CurrentFuel = value;
                    this.NotifyPropertyChanged("CurrentFuel");
                }
            }
        }

        public int Grenades
        {
            get { return this._Grenades; }
            set
            {
                if (value != this._Grenades)
                {
                    this._Grenades = value;
                    this.NotifyPropertyChanged("Grenades");
                }
            }
        }

        public string FaceCode
        {
            get { return this._FaceCode; }
            set
            {
                if (value != this._FaceCode)
                {
                    this._FaceCode = value;
                    this.NotifyPropertyChanged("FaceCode");
                }
            }
        }

        public int ClassFriendlyName
        {
            get { return this._ClassFriendlyName; }
            set
            {
                if (value != this._ClassFriendlyName)
                {
                    this._ClassFriendlyName = value;
                    this.NotifyPropertyChanged("ClassFriendlyName");
                }
            }
        }

        public Guid CharacterGUID
        {
            get { return this._CharacterGUID; }
            set
            {
                if (value != this._CharacterGUID)
                {
                    this._CharacterGUID = value;
                    this.NotifyPropertyChanged("CharacterGUID");
                }
            }
        }

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
