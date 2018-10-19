﻿using Asphalt.Utils;
using Harmony;
using System;
using System.Reflection;

namespace Asphalt.Events
{
    public class EventPatch
    {
        public bool Patched { get; private set; } = false;
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

            var prefixSite = patchClass.GetMethod("Prefix", InjectionUtils.PUBLIC_STATIC);
            var prefix = (prefixSite != null) ? new HarmonyMethod(prefixSite) : null;

            var postfixSite = patchClass.GetMethod("Postfix", InjectionUtils.PUBLIC_STATIC);
            var postfix = (postfixSite != null) ? new HarmonyMethod(postfixSite) : null;

            return new EventPatch()
            {
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
            AsphaltPlugin.Harmony.Unpatch(PatchSite, HarmonyPatchType.All);
            Patched = false;
        }
    }
}