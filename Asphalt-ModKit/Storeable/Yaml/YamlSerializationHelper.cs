using SharpYaml.Serialization;

namespace Asphalt.Storeable.Yaml
{
    internal class YamlSerializationHelper
    {
        public static Serializer Serializer = new Serializer(new SerializerSettings()
        {
            EmitShortTypeName = true,
            EmitAlias = false
        });
    }
}
