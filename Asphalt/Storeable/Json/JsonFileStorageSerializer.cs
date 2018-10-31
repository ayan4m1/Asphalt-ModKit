using Asphalt.Storeable.CommonFileStorage;
using Eco.Core.Serialization;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorageSerializer : IConfigurationSerializer
    {
        public string FileExtension => "json";

        public dynamic Deserialize(string rawConfiguration)
        {
            return SerializationUtils.DeserializeJson<dynamic>(rawConfiguration);
        }

        public string Serialize(dynamic configuration)
        {
            return SerializationUtils.SerializeJson(configuration);
        }
    }
}
