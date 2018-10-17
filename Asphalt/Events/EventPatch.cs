using Asphalt.Utils;
using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public class EventPatch
    {
        public bool Patched { get; private set; }
        private readonly MethodBase patchSite;
        private readonly HarmonyMethod prefix;
        private readonly HarmonyMethod postfix;

        public EventPatch(Type patchType, string methodName, BindingFlags methodType, Type helper) : this(patchType.GetMethod(methodName, methodType), helper) { }
        public EventPatch(MethodBase patchSite, Type helper)
        {
            Patched = false;
            this.patchSite = patchSite;
            prefix = new HarmonyMethod(helper.GetMethod("Prefix", InjectionUtils.PUBLIC_STATIC));
            postfix = new HarmonyMethod(helper.GetMethod("Postfix", InjectionUtils.PUBLIC_STATIC));
        }

        public void Patch()
        {
            if (Patched) return;
            AsphaltPlugin.Harmony.Patch(patchSite, prefix, postfix);
            Patched = true;
        }

        public void Unpatch()
        {
            if (!Patched) return;
            AsphaltPlugin.Harmony.Unpatch(patchSite, HarmonyPatchType.All);
            Patched = false;
        }
    }
}
