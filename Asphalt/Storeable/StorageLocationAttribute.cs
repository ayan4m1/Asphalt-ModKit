using System;
using System.IO;

namespace Asphalt.Storeable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StorageLocationAttribute : Attribute
    {
        public string Location { get; protected set; }

        public StorageLocationAttribute(string location)
        {
            if (!string.IsNullOrEmpty(Path.GetExtension(location)))
                throw new ArgumentException($"{nameof(location)} should never be a filename with an extension. Please provide only the name of a storage object (i.e. the filename without extension)");

            Location = location;
        }
    }
}
