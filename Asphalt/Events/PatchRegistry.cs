using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    public class PatchRegistry
    {
        private static readonly Type emitterType = typeof(EventEmitter<>);
        private static readonly Dictionary<Type, EventPatch> patches = new Dictionary<Type, EventPatch>();

        public void Register()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var isEventHelper = IsEventEmitter(type);
                if (!isEventHelper)
                {
                    continue;
                }

                var patch = new EventPatch(type);
                patches.Add(type, patch);
            }
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
