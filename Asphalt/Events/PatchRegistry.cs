using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public class PatchRegistry
    {
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();
        private static readonly Dictionary<Type, List<EventHandlerData>> handlers = new Dictionary<Type, List<EventHandlerData>>();

        public static void RegisterHandler(Type eventType, EventPatch patch)
        {
            if (!patches.ContainsKey(eventType))
            {
                throw new ArgumentOutOfRangeException("eventType", "Event type has no patches registered!");
            }

            if (handlers.ContainsKey(eventType))
            {
                handlers.Add(eventType, new List<EventHandlerData>());
            }

            handlers[eventType].Add();
        }

        public void RegisterPatch(Type type)
        {
            var patch = EventPatch.FromType(type);
            patches.Add(type, patch);
        }

        public void RegisterPatches(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var isEventHelper = IsEventEmitter(type);
                if (!isEventHelper)
                {
                    continue;
                }

                RegisterPatch(type);
            }
        }

        public void RegisterPatches()
        {
            RegisterPatches(Assembly.GetCallingAssembly());
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
