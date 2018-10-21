using System;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    public static class EventExtensions
    {
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Type patchSiteType = typeof(EventPatchSite);

        public static bool HasPatchSite(this Type type)
        {
            return type.GetCustomAttribute(patchSiteType) != null;
        }

        public static bool IsEventEmitter(this Type type)
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
