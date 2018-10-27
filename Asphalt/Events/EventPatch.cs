using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public class EventPatch
    {
        public bool Patched { get; private set; } = false;
        public Type EventType { get; private set; }
        public MethodBase PatchSite { get; private set; }
        public HarmonyMethod Prefix { get; private set; }
        public HarmonyMethod Postfix { get; private set; }

        public static EventPatch FromType(Type patchClass)
        {
            var patchSiteAttribute = patchClass.GetCustomAttribute<EventPatchSite>();
            if (patchSiteAttribute == null)
            {
                throw new ArgumentNullException("EventPatchSite", $"EventPatchSite attribute missing on {patchClass.FullName}");
            }

            var prefixSite = patchClass.GetMethod("Prefix", CommonBindingFlags.Static);
            var prefix = (prefixSite != null) ? new HarmonyMethod(prefixSite) : null;

            var postfixSite = patchClass.GetMethod("Postfix", CommonBindingFlags.Static);
            var postfix = (postfixSite != null) ? new HarmonyMethod(postfixSite) : null;

            var emitterType = patchClass.BaseType;
            var genericTypes = emitterType.GetGenericArguments();
            if (genericTypes == null || genericTypes.Length != 1)
            {
                throw new InvalidOperationException("Asked to generate an EventPatch from an object which does not have generic type arguments!");
            }

            return new EventPatch()
            {
                EventType = genericTypes[0],
                PatchSite = patchSiteAttribute.PatchSite,
                Prefix = prefix,
                Postfix = postfix
            };
        }

        public void Patch()
        {
            if (Patched) return;
            AsphaltPlugin.Harmony.Patch(PatchSite, Prefix, Postfix);
            Patched = true;
        }

        public void Unpatch()
        {
            if (!Patched) return;
            // using .Unpatch(PatchSite, HarmonyPatchType.All) does not work
            AsphaltPlugin.Harmony.Unpatch(PatchSite, Prefix?.method);
            AsphaltPlugin.Harmony.Unpatch(PatchSite, Postfix?.method);
            Patched = false;
        }
    }
}
