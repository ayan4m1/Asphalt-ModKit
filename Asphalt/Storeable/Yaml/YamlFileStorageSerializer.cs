using Asphalt.Storeable.CommonFileStorage;
using SharpYaml.Serialization;

namespace Asphalt.Storeable.Yaml
{
    public class YamlFileStorageSerializer : IConfigurationSerializer
    {
        private static Serializer serializer = new Serializer(new SerializerSettings()
        {
            EmitShortTypeName = true,
            EmitAlias = false
        });

        public string FileExtension => "yaml";

        public dynamic Deserialize(string rawConfiguration)
        {
            return serializer.Deserialize(rawConfiguration);
        }

        public string Serialize(dynamic configuration)
        {
            return serializer.Serialize(configuration);
        }
    }
}
