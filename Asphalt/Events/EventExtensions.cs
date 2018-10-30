using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventExtensions
    {
        private static readonly Type argsType = typeof(EventArgs);
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Type patchSiteType = typeof(EventPatchSiteAttribute);
        private static readonly Type eventHandlerType = typeof(EventHandlerAttribute);

        public static bool HasEventHandler(this Assembly assembly)
        {
            return assembly.GetTypes().Any(HasEventHandler);
        }

        private static bool HasEventHandler(this Type type)
        {
            return type.GetMethods().Any(IsEventHandler);
        }

        public static IEnumerable<MethodInfo> GetEventHandlers(this Type type)
        {
            return type.GetMethods().Where(IsEventHandler);
        }

        public static IEnumerable<Type> GetEventEmitters(this Assembly assembly)
        {
            return assembly.GetTypes().Where(IsEventEmitter);
        }

        public static IEnumerable<EventPatch> GetEventPatches(this Assembly assembly)
        {
            return GetEventEmitters(assembly).Select(GetEventPatch);
        }

        public static IEnumerable<EventMapping> GetEventMappings(this object instance)
        {
            return GetEventHandlers(instance.GetType()).Select(mi => GetEventMapping(mi, instance));
        }

        public static EventPatch GetEventPatch(this Type type)
        {
            if (!IsEventEmitter(type))
            {
                throw new ArgumentException("Asked to generate an event patch from a non-emitter! Ensure the class extends from EventEmitter<E> and contains an EventPatchSite attribute.");
            }

            var patchSiteAttribute = type.GetCustomAttribute<EventPatchSiteAttribute>();
            var emitterType = type.BaseType;
            var eventType = emitterType.GetGenericArguments()[0];

            var prefixSite = type.GetMethod("Prefix", CommonBindingFlags.Static);
            var prefix = (prefixSite != null) ? new HarmonyMethod(prefixSite) : null;

            var postfixSite = type.GetMethod("Postfix", CommonBindingFlags.Static);
            var postfix = (postfixSite != null) ? new HarmonyMethod(postfixSite) : null;

            return new EventPatch()
            {
                Patched = false,
                Prefix = prefix,
                Postfix = postfix,
                EventType = eventType,
                PatchSite = patchSiteAttribute.PatchSite
            };
        }

        private static bool IsEventEmitter(Type type)
        {
            if (type.BaseType == null || type == emitterType)
            {
                return false;
            }

            if (type.GetCustomAttribute(patchSiteType) == null)
            {
                return false;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == emitterType)
            {
                return type.GetGenericArguments().Length == 1;
            }

            return IsEventEmitter(type.BaseType);
        }

        private static bool IsEventHandler(MethodInfo method)
        {
            var hasAttribute = method.GetCustomAttribute(eventHandlerType) != null;
            if (!hasAttribute)
            {
                return false;
            }

            var parameters = method.GetParameters();
            if (parameters.Length != 1)
            {
                return false;
            }

            var eventType = parameters[0].ParameterType;
            return argsType.IsAssignableFrom(eventType);
        }

        private static EventMapping GetEventMapping(MethodInfo method, object instance)
        {
            if (!IsEventHandler(method))
            {
                throw new ArgumentException("Asked to generate an event mapping from a non-handler! Ensure the method has an EventHandler attribute and accepts a single parameter which descends from EventArgs.");
            }

            var eventType = method.GetParameters()[0].ParameterType;
            var handlerAttribute = method.GetCustomAttribute<EventHandlerAttribute>();

            return new EventMapping()
            {
                Handler = method,
                Instance = instance,
                EventType = eventType,
                Priority = handlerAttribute.Priority,
                AllowCancel = handlerAttribute.AllowCancel
            };
        }
    }
}
