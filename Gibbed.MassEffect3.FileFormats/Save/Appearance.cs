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
    public class Appearance : Unreal.ISerializable, INotifyPropertyChanged
    {
        private PlayerAppearanceType _CombatAppearance;
        private int _CasualID;
        private int _FullBodyID;
        private int _TorsoID;
        private int _ShoulderID;
        private int _ArmID;
        private int _LegID;
        private int _SpecID;
        private int _Tint1ID;
        private int _Tint2ID;
        private int _Tint3ID;
        private int _PatternID;
        private int _PatternColorID;
        private int _HelmetID;
        private bool _HasMorphHead;
        private MorphHead _MorphHead;
        private int _EmissiveID;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.SerializeEnum(ref this._CombatAppearance);
            stream.Serialize(ref this._CasualID);
            stream.Serialize(ref this._FullBodyID);
            stream.Serialize(ref this._TorsoID);
            stream.Serialize(ref this._ShoulderID);
            stream.Serialize(ref this._ArmID);
            stream.Serialize(ref this._LegID);
            stream.Serialize(ref this._SpecID);
            stream.Serialize(ref this._Tint1ID);
            stream.Serialize(ref this._Tint2ID);
            stream.Serialize(ref this._Tint3ID);
            stream.Serialize(ref this._PatternID);
            stream.Serialize(ref this._PatternColorID);
            stream.Serialize(ref this._HelmetID);
            stream.Serialize(ref this._HasMorphHead);

            if (this._HasMorphHead == true)
            {
                stream.Serialize(ref this._MorphHead);
            }

            stream.Serialize(ref this._EmissiveID, (s) => s.Version < 55, () => 0);
        }

        #region Properties
        [Category("Body")]
        [DisplayName("Combat Appearance")]
        public PlayerAppearanceType CombatAppearance
        {
            get { return this._CombatAppearance; }
            set
            {
                if (value != this._CombatAppearance)
                {
                    this._CombatAppearance = value;
                    this.NotifyPropertyChanged("CombatAppearance");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Casual ID")]
        public int CasualID
        {
            get { return this._CasualID; }
            set
            {
                if (value != this._CasualID)
                {
                    this._CasualID = value;
                    this.NotifyPropertyChanged("CasualID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Full Body ID")]
        public int FullBodyID
        {
            get { return this._FullBodyID; }
            set
            {
                if (value != this._FullBodyID)
                {
                    this._FullBodyID = value;
                    this.NotifyPropertyChanged("FullBodyID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Torso ID")]
        public int TorsoID
        {
            get { return this._TorsoID; }
            set
            {
                if (value != this._TorsoID)
                {
                    this._TorsoID = value;
                    this.NotifyPropertyChanged("TorsoID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Shoulder ID")]
        public int ShoulderID
        {
            get { return this._ShoulderID; }
            set
            {
                if (value != this._ShoulderID)
                {
                    this._ShoulderID = value;
                    this.NotifyPropertyChanged("ShoulderID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Arm ID")]
        public int ArmID
        {
            get { return this._ArmID; }
            set
            {
                if (value != this._ArmID)
                {
                    this._ArmID = value;
                    this.NotifyPropertyChanged("ArmID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Leg ID")]
        public int LegID
        {
            get { return this._LegID; }
            set
            {
                if (value != this._LegID)
                {
                    this._LegID = value;
                    this.NotifyPropertyChanged("LegID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Specular ID")]
        public int SpecID
        {
            get { return this._SpecID; }
            set
            {
                if (value != this._SpecID)
                {
                    this._SpecID = value;
                    this.NotifyPropertyChanged("SpecID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Tint #1 ID")]
        public int Tint1ID
        {
            get { return this._Tint1ID; }
            set
            {
                if (value != this._Tint1ID)
                {
                    this._Tint1ID = value;
                    this.NotifyPropertyChanged("Tint1ID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Tint #2 ID")]
        public int Tint2ID
        {
            get { return this._Tint2ID; }
            set
            {
                if (value != this._Tint2ID)
                {
                    this._Tint2ID = value;
                    this.NotifyPropertyChanged("Tint2ID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Tint #3 ID")]
        public int Tint3ID
        {
            get { return this._Tint3ID; }
            set
            {
                if (value != this._Tint3ID)
                {
                    this._Tint3ID = value;
                    this.NotifyPropertyChanged("Tint3ID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Pattern ID")]
        public int PatternID
        {
            get { return this._PatternID; }
            set
            {
                if (value != this._PatternID)
                {
                    this._PatternID = value;
                    this.NotifyPropertyChanged("PatternID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Pattern Color ID")]
        public int PatternColorID
        {
            get { return this._PatternColorID; }
            set
            {
                if (value != this._PatternColorID)
                {
                    this._PatternColorID = value;
                    this.NotifyPropertyChanged("PatternColorID");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Helmet ID")]
        public int HelmetID
        {
            get { return this._HelmetID; }
            set
            {
                if (value != this._HelmetID)
                {
                    this._HelmetID = value;
                    this.NotifyPropertyChanged("HelmetID");
                }
            }
        }

        [Category("Head")]
        [DisplayName("Has Morph Head")]
        public bool HasMorphHead
        {
            get { return this._HasMorphHead; }
            set
            {
                if (value != this._HasMorphHead)
                {
                    this._HasMorphHead = value;
                    this.NotifyPropertyChanged("HasMorphHead");
                }
            }
        }

        [Category("Head")]
        [DisplayName("Morph Head")]
        public MorphHead MorphHead
        {
            get { return this._MorphHead; }
            set
            {
                if (value != this._MorphHead)
                {
                    this._MorphHead = value;
                    this.NotifyPropertyChanged("MorphHead");
                }
            }
        }

        [Category("Body")]
        [DisplayName("Emissive ID")]
        public int EmissiveID
        {
            get { return this._EmissiveID; }
            set
            {
                if (value != this._EmissiveID)
                {
                    this._EmissiveID = value;
                    this.NotifyPropertyChanged("EmissiveID");
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
