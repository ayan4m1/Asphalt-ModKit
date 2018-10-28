using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventHandlerRegistry
    {
        private static readonly Dictionary<Type, List<EventBinding>> handlers = new Dictionary<Type, List<EventBinding>>();

        public static void Handle<E>(ref E rawEvent) where E : EventArgs
        {
            var eventType = rawEvent.GetType();
            if (!handlers.ContainsKey(eventType))
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

                binding.Handler.Invoke(binding.HandlerInstance, new object[] { rawEvent });
            }
        }

        public static void Register(Type eventType, EventBinding handler)
        {
            if (!handlers.ContainsKey(eventType))
            {
                handlers.Add(eventType, new List<EventBinding>());
            }
            handlers[eventType].Add(handler);
        }

        public static void RegisterAll(object handlers)
        {
            var type = handlers.GetType();
            foreach (var method in type
                .GetMethods()
                .Where(EventExtensions.IsEventHandler))
            {
                var handlerInfo = method.GetCustomAttribute<EventHandlerAttribute>();
                var argType = method.GetParameters()[0].ParameterType;

                Register(argType, new EventBinding()
                {
                    AllowCancel = handlerInfo.AllowCancel,
                    Priority = handlerInfo.Priority,
                    Handler = method,
                    HandlerInstance = handlers
                });
            }
        }

        public static void RegisterAll(Assembly assembly)
        {
            foreach (var type in assembly
                .GetTypes()
                .Where(EventExtensions.HasEventHandler))
            {
                RegisterAll(type);
            }
        }

        public static void Unregister(Type eventType)
        {
            if (handlers.ContainsKey(eventType))
            {
                handlers[eventType].Clear();
                handlers.Remove(eventType);
            }
        }

        public static void UnregisterAll()
        {
            foreach (var handler in handlers.Select(x => x.Value))
            {
                handler.Clear();
            }
            handlers.Clear();
        }
    }
}
