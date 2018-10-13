using Asphalt.Storeable.CommonFileStorage;
using Eco.Core.Serialization;
using System.Collections.Generic;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorageSerializer : IFileStorageSerializer
    {
        Dictionary<string, object> IFileStorageSerializer.Deserialize(string pText)
        {
            return SerializationUtils.DeserializeJson<Dictionary<string, object>>(pText);
        }

        string IFileStorageSerializer.GetFileExtension()
        {
            return ".json";
        }

        string IFileStorageSerializer.Serialize(Dictionary<string, object> pObject)
        {
            return SerializationUtils.SerializeJson(pObject);
        }
    }
}
