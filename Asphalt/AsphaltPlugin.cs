using Asphalt.Utils;
using Eco.Core.Plugins.Interfaces;
using Harmony;
using System.IO;
using System.Reflection;

namespace Asphalt
{
    public class AsphaltPlugin : IModKitPlugin
    {
        public static HarmonyInstance Harmony { get; protected set; }

        static AsphaltPlugin()
        {
            // Patch injections for default Services onEnable etc.
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());

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
