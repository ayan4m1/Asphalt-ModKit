﻿using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using Harmony;
using System.IO;
using System.Reflection;
using System.Security.Principal;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin
    {
        public static bool IsInitialized { get; protected set; }

        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());  //Patch injections for default Services onEnable etc.

            IsInitialized = true;

            if (File.Exists("dumpdlls.txt"))
                DllDumper.DumpDlls();
        }

        public string GetStatus()
        {
            return IsInitialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
