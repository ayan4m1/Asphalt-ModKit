namespace Asphalt.Storeable.CommonFileStorage
{
    public interface IConfigurationSerializer
    {
        string FileExtension { get; }

        string Serialize(dynamic configuration);
        dynamic Deserialize(string rawConfiguration);
    }
}
