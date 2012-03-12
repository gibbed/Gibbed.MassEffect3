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
using Newtonsoft.Json;

namespace Gibbed.MassEffect3.SaveEdit
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AppearancePreset
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name;

        [JsonProperty(PropertyName = "HairMesh")]
        public string HairMesh;

        [JsonProperty(PropertyName = "Scalars")]
        public Parameter<float> Scalars = new Parameter<float>();

        [JsonProperty(PropertyName = "Textures")]
        public Parameter<string> Textures = new Parameter<string>();

        [JsonProperty(PropertyName = "Vectors")]
        public Parameter<LinearColor> Vectors = new Parameter<LinearColor>();

        [JsonObject(MemberSerialization.OptIn)]
        public class Parameter<TType>
        {
            [JsonProperty(PropertyName = "Remove")]
            public List<string> Remove = new List<string>();

            [JsonProperty(PropertyName = "Add")]
            public List<KeyValuePair<string, TType>> Add = new List<KeyValuePair<string, TType>>();

            [JsonProperty(PropertyName = "Set")]
            public List<KeyValuePair<string, TType>> Set = new List<KeyValuePair<string, TType>>();
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class LinearColor
        {
            [JsonProperty(PropertyName = "R")]
            public float R;

            [JsonProperty(PropertyName = "G")]
            public float G;

            [JsonProperty(PropertyName = "B")]
            public float B;

            [JsonProperty(PropertyName = "A")]
            public float A;
        }
    }
}
