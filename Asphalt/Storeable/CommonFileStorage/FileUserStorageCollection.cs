using Asphalt.Service;
using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;

namespace Asphalt.Storeable.CommonFileStorage
{
    public class FileUserStorageContainer : FileStorageContainer, IUserStorageContainer, IReloadable
    {
        public FileUserStorageContainer(IConfigurationSerializer serializer, string directory, Dictionary<string, object> defaultValues = null) : base(serializer, directory, defaultValues) { }

        public IStorage GetUserStorage(User user)
        {
            var id = user.SteamId ?? user.SlgId;

            if (id != null)
            {
                return GetStorage(id);
            }

            throw new InvalidOperationException($"User named {user.Name} with ID {user.Client.ID} does not have a SteamId nor an SlgId. This should never occur!");
        }
    }
}
