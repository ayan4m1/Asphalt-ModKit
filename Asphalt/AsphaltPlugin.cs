using System;

namespace Asphalt
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AsphaltPlugin : Attribute
    {
        public string ModName { get; set; }

        public AsphaltPlugin(string name = null)
        {
            ModName = name;
        }
    }
}
