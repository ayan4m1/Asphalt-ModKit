using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public struct EventPatch
    {
        public bool Patched;
        public Type EventType;
        public MethodBase PatchSite;
        public HarmonyMethod Prefix;
        public HarmonyMethod Postfix;

        public void Patch()
        {
            if (Patched) return;
            Asphalt.Harmony.Patch(PatchSite, Prefix, Postfix);
            Patched = true;
        }

        public void Unpatch()
        {
            if (!Patched) return;
            // using .Unpatch(PatchSite, HarmonyPatchType.All) does not work
            Asphalt.Harmony.Unpatch(PatchSite, Prefix?.method);
            Asphalt.Harmony.Unpatch(PatchSite, Postfix?.method);
            Patched = false;
        }
    }
}
