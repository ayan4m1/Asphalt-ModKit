using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class PatchRegistry
    {
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();

        public static EventPatch RegisterPatch(Type patchType)
        {
            var patch = EventPatch.FromType(patchType);
            if (patches.ContainsKey(patch.EventType))
            {
                throw new ArgumentException($"The type {patch.EventType.FullName} already has a patch registered!");
            }

            patch.Patch();
            patches.Add(patch.EventType, patch);
            return patch;
        }

        public static void RegisterPatches(Assembly assembly)
        {
            foreach (var type in assembly
                .GetTypes()
                .Where(EventExtensions.IsEventEmitter)
                .Where(type => !patches.ContainsKey(type)))
            {
                RegisterPatch(type);
            }
        }

        public static void RegisterPatches()
        {
            RegisterPatches(typeof(PatchRegistry).Assembly);
        }

        public static void UnregisterPatch(Type patchType)
        {
            if (!patches.ContainsKey(patchType))
            {
                throw new ArgumentException($"The type {patchType.FullName} has not been registered!");
            }

            patches.Remove(patchType);
        }

        public static void UnregisterPatches(Assembly assembly)
        {
            foreach (var type in assembly
                .GetTypes()
                .Where(EventExtensions.IsEventEmitter)
                .Where(type => patches.ContainsKey(type)))
            {
                UnregisterPatch(type);
            }
        }

        public static void UnregisterPatches()
        {
            UnregisterPatches(Assembly.GetCallingAssembly());
        }

        public static void Reset()
        {
            foreach (var (type, patch) in patches.Select(x => (x.Key, x.Value)))
            {
                patch.Unpatch();
            }
            patches.Clear();
        }
    }
}
