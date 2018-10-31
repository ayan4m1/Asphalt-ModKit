namespace Asphalt.Storeable
{
    /// <summary>
    /// Container for storage objects. Allows you to fetch a "default" storage object or named storage objects.
    /// </summary>
    public interface IStorageContainer
    {
        /// <summary>
        /// Gets the "default" storage object which is shared across all Asphalt plugins.
        /// Don't store any plugin-specific data here, as keys may collide with other plugins!
        /// </summary>
        /// <returns>A storage object representing the default namespace.</returns>
        IStorage GetDefaultStorage();

        /// <summary>
        /// Gets a storage object by its name.
        /// Store your plugin-specific data here, using your plugin name or package identifier as the storage name.
        /// </summary>
        /// <param name="storageName"></param>
        /// <returns>A storage object representing the specfied namespace.</returns>
        IStorage GetStorage(string storageName);
    }
}
