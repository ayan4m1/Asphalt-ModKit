using Asphalt.Events;
using Asphalt.Utils;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Harmony;
using System.IO;

namespace Asphalt
{
    public class Asphalt : IModKitPlugin, IInitializablePlugin
    {
        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");

            // apply service hook patches
            Harmony.PatchAll();
        }

        public string GetStatus()
        {
            return "Initialized!";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }

        public void Initialize(TimedTask timer)
        {
            // register event emitters
            EventPatchRegistry.RegisterInternal();

            if (File.Exists("dumpdlls.txt"))
            {
                DllDumpUtils.Dump();
            }
        }
    }
}
