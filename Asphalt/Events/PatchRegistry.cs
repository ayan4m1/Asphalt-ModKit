using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class PatchRegistry
    {
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();
        private static readonly Dictionary<Type, List<EventBinding>> handlers = new Dictionary<Type, List<EventBinding>>();

        public static void RegisterHandler(Type eventType, EventBinding handler)
        {
            if (!patches.ContainsKey(eventType))
            {
                throw new ArgumentOutOfRangeException("eventType", $"The type {eventType.FullName} is not registered!");
            }

            patches[eventType].Patch();

            if (handlers.ContainsKey(eventType))
            {
                handlers.Add(eventType, new List<EventBinding>());
            }

            handlers[eventType].Add(handler);
        }

        public static void UnregisterHandlers(Type eventType)
        {
            if (!patches.ContainsKey(eventType))
            {
                throw new ArgumentOutOfRangeException("eventType", $"The type {eventType.FullName} is not registered!");
            }

            if (handlers.ContainsKey(eventType))
            {
                handlers[eventType].Clear();
                handlers.Remove(eventType);
            }
        }

        public static EventPatch RegisterPatch(Type patchType)
        {
            if (patches.ContainsKey(patchType))
            {
                throw new ArgumentException($"The type {patchType.FullName} already has a patch registered!");
            }

            var patch = EventPatch.FromType(patchType);
            patches.Add(patchType, patch);
            return patch;
        }

        public static void RegisterPatches(Assembly assembly)
        {
            foreach (var type in assembly
                .GetTypes()
                .Where(IsEventEmitter)
                .Where(type => !patches.ContainsKey(type)))
            {
                RegisterPatch(type);
            }
        }

        public static void RegisterPatches()
        {
            RegisterPatches(Assembly.GetCallingAssembly());
        }

        public static void UnregisterPatch(Type patchType)
        {
            if (!patches.ContainsKey(patchType))
            {
                throw new ArgumentException($"The type {patchType.FullName} has not been registered!");
            }

            if (handlers.ContainsKey(patchType))
            {
                handlers[patchType].Clear();
                handlers.Remove(patchType);
            }

            patches.Remove(patchType);
        }

        public static void UnregisterPatches(Assembly assembly)
        {
            foreach (var type in assembly
                .GetTypes()
                .Where(IsEventEmitter)
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

            foreach (var handler in handlers.Select(x => x.Value))
            {
                handler.Clear();
            }
            handlers.Clear();
        }

        private static bool HasEventPatchSiteAttribute(Type type)
        {
            return type.GetCustomAttribute<EventPatchSite>() != null;
        }

        private static bool IsEventEmitter(Type type)
        {
            if (type.BaseType == null)
            {
                return false;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == emitterType)
            {
                return true;
            }

            var interfaces = type
                .GetInterfaces()
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == emitterType);

            if (interfaces.Count() > 0)
            {
                return true;
            }

            return IsEventEmitter(type.BaseType);
        }
    }
}
