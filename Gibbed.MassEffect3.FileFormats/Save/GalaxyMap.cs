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

using System.Collections.Generic;
using System.ComponentModel;

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GalaxyMap : Unreal.ISerializable, INotifyPropertyChanged
    {
        private List<Planet> _Planets;
        private List<System> _Systems;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._Planets);
            stream.Serialize(ref this._Systems, (s) => s.Version < 51, () => new List<System>());
        }

        #region Properties
        public List<Planet> Planets
        {
            get { return this._Planets; }
            set
            {
                if (value != this._Planets)
                {
                    this._Planets = value;
                    this.NotifyPropertyChanged("Planets");
                }
            }
        }

        public List<System> Systems
        {
            get { return this._Systems; }
            set
            {
                if (value != this._Systems)
                {
                    this._Systems = value;
                    this.NotifyPropertyChanged("Systems");
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

        #region Children
        public class Planet : Unreal.ISerializable, INotifyPropertyChanged
        {
            private int _PlanetID;
            private bool _Visited;
            private List<Vector2D> _Probes;
            private bool _ShowAsScanned;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._PlanetID);
                stream.Serialize(ref this._Visited);
                stream.Serialize(ref this._Probes);
                stream.Serialize(ref this._ShowAsScanned, (s) => s.Version < 51, () => false);
            }

            #region Properties
            public int PlanetID
            {
                get { return this._PlanetID; }
                set
                {
                    if (value != this._PlanetID)
                    {
                        this._PlanetID = value;
                        this.NotifyPropertyChanged("PlanetID");
                    }
                }
            }

            public bool Visited
            {
                get { return this._Visited; }
                set
                {
                    if (value != this._Visited)
                    {
                        this._Visited = value;
                        this.NotifyPropertyChanged("Visited");
                    }
                }
            }

            public List<Vector2D> Probes
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

            public bool ShowAsScanned
            {
                get { return this._ShowAsScanned; }
                set
                {
                    if (value != this._ShowAsScanned)
                    {
                        this._ShowAsScanned = value;
                        this.NotifyPropertyChanged("ShowAsScanned");
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

        public class System : Unreal.ISerializable, INotifyPropertyChanged
        {
            private int _SystemID;
            private float _ReaperAlertLevel;
            private bool _ReapersDetected;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._SystemID);
                stream.Serialize(ref this._ReaperAlertLevel);
                stream.Serialize(ref this._ReapersDetected, (s) => s.Version < 58, () => false);
            }

            #region Properties
            public int SystemID
            {
                get { return this._SystemID; }
                set
                {
                    if (value != this._SystemID)
                    {
                        this._SystemID = value;
                        this.NotifyPropertyChanged("SystemID");
                    }
                }
            }

            public float ReaperAlertLevel
            {
                get { return this._ReaperAlertLevel; }
                set
                {
                    if (value != this._ReaperAlertLevel)
                    {
                        this._ReaperAlertLevel = value;
                        this.NotifyPropertyChanged("ReaperAlertLevel");
                    }
                }
            }

            public bool ReapersDetected
            {
                get { return this._ReapersDetected; }
                set
                {
                    if (value != this._ReapersDetected)
                    {
                        this._ReapersDetected = value;
                        this.NotifyPropertyChanged("ReapersDetected");
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
        #endregion
    }
}
