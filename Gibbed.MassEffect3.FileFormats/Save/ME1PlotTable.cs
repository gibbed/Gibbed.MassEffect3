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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [OriginalName("ME1PlotTableRecord")]
    public class ME1PlotTable : Unreal.ISerializable, INotifyPropertyChanged
    {
        public ME1PlotTable()
        {
            this._BoolVariablesWrapper = new BoolVariablesWrapper(this);
        }

        #region Fields
        [OriginalName("BoolVariables")]
        private BitArray _BoolVariables;
        private BoolVariablesWrapper _BoolVariablesWrapper;

        [OriginalName("IntVariables")]
        private List<int> _IntVariables;

        [OriginalName("FloatVariables")]
        private List<float> _FloatVariables;
        #endregion

        public void Serialize(Unreal.ISerializer stream)
        {
            stream.Serialize(ref this._BoolVariables);
            stream.Serialize(ref this._IntVariables);
            stream.Serialize(ref this._FloatVariables);
        }

        #region BoolVariablesWrapper
        public class BoolVariablesWrapper : IList
        {
            public readonly ME1PlotTable Target;

            // for CollectionEditor, so it knows the correct Item type
            public bool Item { get; private set; }

            public BoolVariablesWrapper()
                : this(null)
            {
            }

            public BoolVariablesWrapper(ME1PlotTable target)
            {
                this.Target = target;
            }

            #region IEnumerable Members
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.Target._BoolVariables.GetEnumerator();
            }
            #endregion

            #region IList Members
            int IList.Add(object value)
            {
                if ((value is bool) == false)
                {
                    throw new ArgumentException("value");
                }

                var index = this.Target._BoolVariables.Length;
                this.Target._BoolVariables.Length++;
                this.Target._BoolVariables[index] = (bool)value;
                return index;
            }

            void IList.Clear()
            {
                this.Target._BoolVariables.Length = 0;
            }

            bool IList.Contains(object value)
            {
                throw new NotSupportedException();
            }

            int IList.IndexOf(object value)
            {
                throw new NotSupportedException();
            }

            void IList.Insert(int index, object value)
            {
                if ((value is bool) == false)
                {
                    throw new ArgumentException("value");
                }

                if (index >= this.Target._BoolVariables.Length)
                {
                    this.Target._BoolVariables.Length = index + 1;
                    this.Target._BoolVariables[index] = (bool)value;
                }
                else
                {
                    this.Target._BoolVariables.Length++;
                    for (int i = this.Target._BoolVariables.Length - 1; i > index; i--)
                    {
                        this.Target._BoolVariables[i] = this.Target._BoolVariables[i - 1];
                    }
                    this.Target._BoolVariables[index] = (bool)value;
                }
            }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            bool IList.IsReadOnly
            {
                get { return false; }
            }

            void IList.Remove(object value)
            {
                throw new NotSupportedException();
            }

            void IList.RemoveAt(int index)
            {
                if (index >= this.Target._BoolVariables.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                for (int i = this.Target._BoolVariables.Length - 1; i > index; i--)
                {
                    this.Target._BoolVariables[i - 1] = this.Target._BoolVariables[i];
                }
                this.Target._BoolVariables.Length--;
            }

            object IList.this[int index]
            {
                get { return this.Target._BoolVariables[index]; }
                set
                {
                    if ((value is bool) == false)
                    {
                        throw new ArgumentException("value");
                    }

                    this.Target._BoolVariables[index] = (bool)value;
                }
            }
            #endregion

            #region ICollection Members
            void ICollection.CopyTo(Array array, int index)
            {
                for (int i = 0; i < this.Target._BoolVariables.Length; i++)
                {
                    array.SetValue(this.Target._BoolVariables[i], index + i);
                }
            }

            int ICollection.Count
            {
                get { return this.Target._BoolVariables.Length; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return this; }
            }
            #endregion
        }
        #endregion

        #region Properties
        public BoolVariablesWrapper BoolVariables
        {
            get { return this._BoolVariablesWrapper; }
            /*set
            {
                if (value != this._BoolVariables)
                {
                    this._BoolVariables = value;
                    this.NotifyPropertyChanged("BoolVariables");
                }
            }*/
        }

        public List<int> IntVariables
        {
            get { return this._IntVariables; }
            set
            {
                if (value != this._IntVariables)
                {
                    this._IntVariables = value;
                    this.NotifyPropertyChanged("IntVariables");
                }
            }
        }

        public List<float> FloatVariables
        {
            get { return this._FloatVariables; }
            set
            {
                if (value != this._FloatVariables)
                {
                    this._FloatVariables = value;
                    this.NotifyPropertyChanged("FloatVariables");
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
