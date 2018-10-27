using Asphalt.Events;
using Asphalt.Utils;
using Eco.Core.Plugins.Interfaces;
using Harmony;
using System.IO;

namespace Asphalt
{
    public class Asphalt : IModKitPlugin
    {
        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            // apply service hook patches
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            Harmony.PatchAll();

            // register event emitters
            PatchRegistry.RegisterPatches();

            if (File.Exists("dumpdlls.txt"))
            {
                DllDumpUtils.Dump();
            }
        }

        public string GetStatus()
        {
            return "Initialized!";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
