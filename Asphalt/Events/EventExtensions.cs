using System;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventExtensions
    {
        private static readonly Type argsType = typeof(EventArgs);
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Type patchSiteType = typeof(EventPatchSite);
        private static readonly Type eventHandlerType = typeof(EventHandlerAttribute);

        public static bool HasEventHandler(this Type type)
        {
            return type.GetMethods().Any(mi => mi.GetCustomAttribute(eventHandlerType) != null);
        }

        public static bool HasPatchSite(this Type type)
        {
            return type.GetCustomAttribute(patchSiteType) != null;
        }

        public static bool IsEventHandler(this MethodInfo methodInfo)
        {
            var hasAttribute = methodInfo.GetCustomAttribute(eventHandlerType) != null;
            if (!hasAttribute)
            {
                return false;
            }

            var parameters = methodInfo.GetParameters();
            if (parameters.Length != 1)
            {
                return false;
            }

            var paramType = parameters[0].ParameterType;
            return argsType.IsAssignableFrom(paramType);
        }

        public static bool IsEventEmitter(this Type type)
        {
            if (type.BaseType == null || type == emitterType)
            {
                return false;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == emitterType)
            {
                return true;
            }

            return IsEventEmitter(type.BaseType);
        }
    }
}
