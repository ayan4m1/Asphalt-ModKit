using System.Collections.Generic;

namespace Asphalt.Storeable.CommonFileStorage
{
    public interface IFileStorageSerializer
    {
        Dictionary<string, object> Deserialize(string pText);

        string Serialize(Dictionary<string, object> pObject);

        string GetFileExtension();
    }
}
