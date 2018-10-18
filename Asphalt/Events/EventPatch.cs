using Asphalt.Utils;
using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public class EventPatch // <E> where E : EventArgs
    {
        public bool Patched { get; private set; }
        public HarmonyMethod Prefix { get; private set; }
        public HarmonyMethod Postfix { get; private set; }

        public EventPatch(Type patchClass)
        {
            Patched = false;
            Prefix = new HarmonyMethod(patchClass.GetMethod("Prefix", InjectionUtils.PUBLIC_STATIC));
            Postfix = new HarmonyMethod(patchClass.GetMethod("Postfix", InjectionUtils.PUBLIC_STATIC));
        }

        public void Patch(MethodBase patchSite)
        {
            if (Patched) return;
            AsphaltPlugin.Harmony.Patch(patchSite, Prefix, Postfix);
            Patched = true;
        }

        public void Unpatch(MethodBase patchSite)
        {
            if (!Patched) return;
            AsphaltPlugin.Harmony.Unpatch(patchSite, HarmonyPatchType.All);
            Patched = false;
        }
    }
}
