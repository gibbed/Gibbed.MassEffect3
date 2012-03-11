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
using System.Drawing.Design;
using DynamicTypeDescriptor;

namespace Gibbed.MassEffect3.FileFormats.Save
{
    [Editor(typeof(StandardValueEditor), typeof(UITypeEditor))]
    [OriginalName("EAutoReplyModeOptions")]
    public enum AutoReplyModeOptions : byte
    {
        [StandardValue(null, DisplayName = "All Descisions")]
        [OriginalName("ARMO_All_Decisions")]
        AllDecisions = 0,
        
        [StandardValue(null, DisplayName = "Major Decisions")]
        [OriginalName("ARMO_Major_Decisions")]
        MajorDecisions = 1,
        
        [StandardValue(null, DisplayName = "No Decisions")]
        [OriginalName("ARMO_No_Decisions")]
        NoDecisions = 2,
    }
}
