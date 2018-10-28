using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public struct EventPatch
    {
        public bool Patched { get; private set; }
        public Type EventType { get; private set; }
        public MethodBase PatchSite { get; private set; }
        public HarmonyMethod Prefix { get; private set; }
        public HarmonyMethod Postfix { get; private set; }

        public static EventPatch FromType(Type patchClass)
        {
            var patchSiteAttribute = patchClass.GetCustomAttribute<EventPatchSiteAttribute>();
            if (patchSiteAttribute == null)
            {
                throw new ArgumentException($"EventPatchSite attribute missing on {patchClass.FullName}!");
            }

            var emitterType = patchClass.BaseType;
            var genericTypes = emitterType.GetGenericArguments();
            if (genericTypes == null || genericTypes.Length != 1)
            {
                throw new ArgumentException($"{patchClass.FullName} does not implement EventEmitter<E>!");
            }

            var prefixSite = patchClass.GetMethod("Prefix", CommonBindingFlags.Static);
            var prefix = (prefixSite != null) ? new HarmonyMethod(prefixSite) : null;

            var postfixSite = patchClass.GetMethod("Postfix", CommonBindingFlags.Static);
            var postfix = (postfixSite != null) ? new HarmonyMethod(postfixSite) : null;

            return new EventPatch()
            {
                Patched = false,
                EventType = genericTypes[0],
                PatchSite = patchSiteAttribute.PatchSite,
                Prefix = prefix,
                Postfix = postfix
            };
        }

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
