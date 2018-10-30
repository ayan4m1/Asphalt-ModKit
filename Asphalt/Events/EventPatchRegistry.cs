using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventPatchRegistry
    {
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();

        public static EventPatch Register(Type patchType)
        {
            var patch = patchType.GetEventPatch();
            if (patches.ContainsKey(patch.EventType))
            {
                throw new ArgumentException($"The type {patch.EventType.FullName} already has a patch registered!");
            }

            patch.Patch();
            patches.Add(patch.EventType, patch);
            return patch;
        }

        public static void RegisterAll(Assembly assembly)
        {
            foreach (var type in assembly.GetEventEmitters())
            {
                Register(type);
            }
        }

        public static void RegisterInternal()
        {
            RegisterAll(typeof(EventPatchRegistry).Assembly);
        }

        public static void Unregister(Type patchType)
        {
            if (!patches.ContainsKey(patchType))
            {
                throw new ArgumentException($"The type {patchType.FullName} has not been registered!");
            }

            patches.Remove(patchType);
        }

        public static void UnregisterAll(Assembly assembly)
        {
            foreach (var type in assembly.GetEventEmitters())
            {
                Unregister(type);
            }
        }

        public static void UnregisterAll()
        {
            foreach (var (type, patch) in patches.Select(x => (x.Key, x.Value)))
            {
                patch.Unpatch();
            }
            patches.Clear();
        }
    }
}
