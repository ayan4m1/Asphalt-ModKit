using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventMappingRegistry
    {
        private static readonly object locker = new object();
        private static readonly EventMappingComparer mappingSorter = new EventMappingComparer();
        private static readonly Dictionary<Type, List<EventMapping>> mappings = new Dictionary<Type, List<EventMapping>>();

        public static void Handle<E>(ref E rawEvent) where E : EventArgs
        {
            var eventType = rawEvent.GetType();
            if (!mappings.ContainsKey(eventType))
            {
                return;
            }

            lock (locker)
            {
                var bindings = mappings[eventType];
                var cancellable = rawEvent as CancelEventArgs;
                foreach (var binding in bindings)
                {
                    if (cancellable != null && cancellable.Cancel && binding.AllowCancel)
                    {
                        continue;
                    }

                    binding.Handler.Invoke(binding.Instance, new object[] { rawEvent });
                }
            }
        }

        public static void Register(EventMapping mapping)
        {
            var eventType = mapping.EventType;
            if (!mappings.ContainsKey(eventType))
            {
                mappings.Add(eventType, new List<EventMapping>());
            }
            mappings[eventType].Add(mapping);
            // todo: consider performance implications of sorting after each Register()
            //       is a transaction framework overkill? could add a "sort" boolean parameter...
            mappings[eventType].Sort(mappingSorter);
        }

        public static void RegisterAll(object instance)
        {
            foreach (var mapping in instance.GetEventMappings())
            {
                Register(mapping);
            }
        }

        public static void Unregister(Type eventType)
        {
            if (mappings.ContainsKey(eventType))
            {
                mappings[eventType].Clear();
                mappings.Remove(eventType);
            }
        }

        public static void UnregisterAll()
        {
            foreach (var handler in mappings.Select(x => x.Value))
            {
                handler.Clear();
            }
            mappings.Clear();
        }
    }
}
