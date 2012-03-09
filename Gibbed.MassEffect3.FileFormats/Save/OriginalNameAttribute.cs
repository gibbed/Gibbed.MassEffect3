using System;

namespace Gibbed.MassEffect3.FileFormats.Save
{
    public class OriginalNameAttribute : Attribute
    {
        public readonly string Name;

        public OriginalNameAttribute(string name)
        {
            if (string.IsNullOrEmpty(name) == true)
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
        }
    }
}
