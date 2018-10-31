﻿using Eco.Core.Plugins.Interfaces;
using Asphalt.Service;
using Asphalt.Storeable;
using Asphalt;
using Asphalt.Service.Permissions;

namespace AsphaltFindEasterEggsPlugin
{
    [AsphaltPlugin]
    public class AsphaltFindEasterEggsPlugin : IModKitPlugin
    {
        [Inject]
        [StorageLocation("Config")]
        [DefaultValues(nameof(GetConfig))]
        public static IStorage ConfigStorage { get; set; }

        [Inject]
        [StorageLocation("CollectedEggsStorage")]
        public static IUserStorageContainer CollectedEggsStorage { get; set; }

        [Inject]
        public static IPermissionService PermissionChecker { get; set; }

        public AsphaltFindEasterEggsPlugin()
        {
            
        }

        public void OnPreEnable()
        {
            
        }

        public void OnEnable()
        {
            int maximumEggsInWorld = ConfigStorage.GetInt("MaximumEggsInWorld");
        }

        public static KeyDefaultValue[] GetConfig()
        {
            return new KeyDefaultValue[]
            {
                new KeyDefaultValue("MaximumEggsInWorld","42"),
            };
        }

        public static DefaultPermission[] GetDefaultPermissions()
        {
            return new DefaultPermission[]
            {
                new DefaultPermission("easteregg.collect", PermissionGroup.User),
                new DefaultPermission("easteregg.create", PermissionGroup.User)
            };
        }

        public string GetStatus()
        {
            return "Unknown";
        }
    }
}
