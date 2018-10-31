using Asphalt.Service;
using Asphalt.Service.Permissions;
using Asphalt.Storeable.CommonFileStorage;
using Asphalt.Storeable.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Storeable
{
    public static class StorageFactory
    {
        public static IConfigurationSerializer GetSerializer(Type pServerPlugin, Type pProperty)
        {
            return new JsonFileStorageSerializer();
        }

        public static Func<PropertyFieldInfo, Dictionary<string, object>, object> GetStorageFactory(Type pServerPlugin, Type pProperty)
        {
            IConfigurationSerializer serializer = GetSerializer(pServerPlugin, pProperty);

            if (pProperty == typeof(IStorage))
                return (l_pfi, defaultValues) => new FileStorage(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues, true);

            if (pProperty == typeof(IStorageContainer))
                return (l_pfi, defaultValues) => new FileStorageContainer(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues);

            if (pProperty == typeof(IUserStorageContainer))
                return (l_pfi, defaultValues) => new FileUserStorageContainer(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues);

            if (pProperty == typeof(IPermissionService))
                return (l_pfi, defaultValues) => new FilePermissionStorage(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), "Permissions"), defaultValues);


            throw new NotImplementedException(pProperty?.ToString());
        }
    }
}
