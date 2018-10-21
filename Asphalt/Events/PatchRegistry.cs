using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class PatchRegistry
    {
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();
        private static readonly Dictionary<Type, List<EventBinding>> handlers = new Dictionary<Type, List<EventBinding>>();

        public static void HandleEvent<E>(ref E rawEvent) where E : EventArgs {
            var eventType = rawEvent.GetType();
            if (!patches.ContainsKey(eventType) || !handlers.ContainsKey(eventType))
            {
                return;
            }

            var bindings = handlers[eventType];
            var cancellable = rawEvent as CancelEventArgs;
            foreach (var binding in bindings)
            {
                if (cancellable != null && cancellable.Cancel && binding.AllowCancel)
                {
                    continue;
                }

                binding.Handler.Invoke(binding.HandlerType, new object[] { rawEvent });
            }
        }

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
            patch.Patch();
            patches.Add(patchType, patch);
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

            foreach (var handler in handlers.Select(x => x.Value))
            {
                handler.Clear();
            }
            handlers.Clear();
        }
    }
}
