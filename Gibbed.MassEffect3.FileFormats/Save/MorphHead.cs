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
    public class MorphHead : Unreal.ISerializable, INotifyPropertyChanged
    {
        private string _HairMesh;
        private List<string> _AccessoryMeshes;
        private List<MorphFeature> _MorphFeatures;
        private List<OffsetBone> _OffsetBones;
        private List<Vector> _LOD0Vertices;
        private List<Vector> _LOD1Vertices;
        private List<Vector> _LOD2Vertices;
        private List<Vector> _LOD3Vertices;
        private List<ScalarParameter> _ScalarParameters;
        private List<VectorParameter> _VectorParameters;
        private List<TextureParameter> _TextureParameters;

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._HairMesh);
            stream.Serialize(ref this._AccessoryMeshes);
            stream.Serialize(ref this._MorphFeatures);
            stream.Serialize(ref this._OffsetBones);
            stream.Serialize(ref this._LOD0Vertices);
            stream.Serialize(ref this._LOD1Vertices);
            stream.Serialize(ref this._LOD2Vertices);
            stream.Serialize(ref this._LOD3Vertices);
            stream.Serialize(ref this._ScalarParameters);
            stream.Serialize(ref this._VectorParameters);
            stream.Serialize(ref this._TextureParameters);
        }

        #region Properties
        public string HairMesh
        {
            get { return this._HairMesh; }
            set
            {
                if (value != this._HairMesh)
                {
                    this._HairMesh = value;
                    this.NotifyPropertyChanged("HairMesh");
                }
            }
        }

        public List<string> AccessoryMeshes
        {
            get { return this._AccessoryMeshes; }
            set
            {
                if (value != this._AccessoryMeshes)
                {
                    this._AccessoryMeshes = value;
                    this.NotifyPropertyChanged("AccessoryMeshes");
                }
            }
        }

        public List<MorphFeature> MorphFeatures
        {
            get { return this._MorphFeatures; }
            set
            {
                if (value != this._MorphFeatures)
                {
                    this._MorphFeatures = value;
                    this.NotifyPropertyChanged("MorphFeatures");
                }
            }
        }

        public List<OffsetBone> OffsetBones
        {
            get { return this._OffsetBones; }
            set
            {
                if (value != this._OffsetBones)
                {
                    this._OffsetBones = value;
                    this.NotifyPropertyChanged("OffsetBones");
                }
            }
        }

        public List<Vector> LOD0Vertices
        {
            get { return this._LOD0Vertices; }
            set
            {
                if (value != this._LOD0Vertices)
                {
                    this._LOD0Vertices = value;
                    this.NotifyPropertyChanged("LOD0Vertices");
                }
            }
        }

        public List<Vector> LOD1Vertices
        {
            get { return this._LOD1Vertices; }
            set
            {
                if (value != this._LOD1Vertices)
                {
                    this._LOD1Vertices = value;
                    this.NotifyPropertyChanged("LOD1Vertices");
                }
            }
        }

        public List<Vector> LOD2Vertices
        {
            get { return this._LOD2Vertices; }
            set
            {
                if (value != this._LOD2Vertices)
                {
                    this._LOD2Vertices = value;
                    this.NotifyPropertyChanged("LOD2Vertices");
                }
            }
        }

        public List<Vector> LOD3Vertices
        {
            get { return this._LOD3Vertices; }
            set
            {
                if (value != this._LOD3Vertices)
                {
                    this._LOD3Vertices = value;
                    this.NotifyPropertyChanged("LOD3Vertices");
                }
            }
        }

        public List<ScalarParameter> ScalarParameters
        {
            get { return this._ScalarParameters; }
            set
            {
                if (value != this._ScalarParameters)
                {
                    this._ScalarParameters = value;
                    this.NotifyPropertyChanged("ScalarParameters");
                }
            }
        }

        public List<VectorParameter> VectorParameters
        {
            get { return this._VectorParameters; }
            set
            {
                if (value != this._VectorParameters)
                {
                    this._VectorParameters = value;
                    this.NotifyPropertyChanged("VectorParameters");
                }
            }
        }

        public List<TextureParameter> TextureParameters
        {
            get { return this._TextureParameters; }
            set
            {
                if (value != this._TextureParameters)
                {
                    this._TextureParameters = value;
                    this.NotifyPropertyChanged("TextureParameters");
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
        public class MorphFeature : Unreal.ISerializable, INotifyPropertyChanged
        {
            private string _Feature;
            private float _Offset;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._Feature);
                stream.Serialize(ref this._Offset);
            }

            #region Properties
            public string Feature
            {
                get { return this._Feature; }
                set
                {
                    if (value != this._Feature)
                    {
                        this._Feature = value;
                        this.NotifyPropertyChanged("Feature");
                    }
                }
            }

            public float Offset
            {
                get { return this._Offset; }
                set
                {
                    if (value != this._Offset)
                    {
                        this._Offset = value;
                        this.NotifyPropertyChanged("Offset");
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

        public class OffsetBone : Unreal.ISerializable, INotifyPropertyChanged
        {
            private string _Name;
            private Vector _Offset;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._Name);
                stream.Serialize(ref this._Offset);
            }

            #region Properties
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

            public Vector Offset
            {
                get { return this._Offset; }
                set
                {
                    if (value != this._Offset)
                    {
                        this._Offset = value;
                        this.NotifyPropertyChanged("Offset");
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

        public class ScalarParameter : Unreal.ISerializable, INotifyPropertyChanged
        {
            private string _Name;
            private float _Value;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._Name);
                stream.Serialize(ref this._Value);
            }

            #region Properties
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

            public float Value
            {
                get { return this._Value; }
                set
                {
                    if (value != this._Value)
                    {
                        this._Value = value;
                        this.NotifyPropertyChanged("Value");
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

        public class VectorParameter : Unreal.ISerializable, INotifyPropertyChanged
        {
            private string _Name;
            private LinearColor _Value;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._Name);
                stream.Serialize(ref this._Value);
            }

            #region Properties
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

            public LinearColor Value
            {
                get { return this._Value; }
                set
                {
                    if (value != this._Value)
                    {
                        this._Value = value;
                        this.NotifyPropertyChanged("Value");
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

        public class TextureParameter : Unreal.ISerializable, INotifyPropertyChanged
        {
            private string _Name;
            private string _Value;

            public void Serialize(Unreal.ISerializer stream)
            {
                stream.Serialize(ref this._Name);
                stream.Serialize(ref this._Value);
            }

            #region Properties
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

            public string Value
            {
                get { return this._Value; }
                set
                {
                    if (value != this._Value)
                    {
                        this._Value = value;
                        this.NotifyPropertyChanged("Value");
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
