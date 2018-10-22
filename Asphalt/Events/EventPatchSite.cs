using System;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    internal struct CommonBindingFlags
    {
        public const BindingFlags Static = BindingFlags.Public | BindingFlags.Static;
        public const BindingFlags Instance = BindingFlags.Public | BindingFlags.Instance;
        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;
        public const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public const BindingFlags GetField = BindingFlags.Public | BindingFlags.GetField;
        public const BindingFlags GetProperty = BindingFlags.Public | BindingFlags.GetProperty;
        public const BindingFlags SetField = BindingFlags.Public | BindingFlags.SetField;
        public const BindingFlags SetProperty = BindingFlags.Public | BindingFlags.SetProperty;
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventPatchSite : Attribute
    {
        public MethodBase PatchSite { get; private set; }

        /// <summary>
        /// Defines a patch site on the specified type with the specified name.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        public EventPatchSite(Type type, string methodName)
        {
            PatchSite = HuntForMethod(type, methodName);
            if (PatchSite == null)
            {
                throw new ArgumentException($"Could not find patch site for {type.FullName}.{methodName}");
            }
        }

        /// <summary>
        /// Defines a patch site on the specified type with the specified name and flags.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        /// <param name="flags">BindingFlags describing the method to bind</param>
        public EventPatchSite(Type type, string methodName, BindingFlags flags)
        {
            PatchSite = type.GetMethod(methodName, flags);
            if (PatchSite == null)
            {
                PatchSite = HuntForMethod(type, methodName);
                if (PatchSite == null)
                {
                    throw new ArgumentException($"Could not find patch site for {type.FullName}.{methodName}");
                }
            }
        }

        /// <summary>
        /// Defines a patch site on the specified type with the specified name, flags, and parameter count.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        /// <param name="flags">BindingFlags describing the method to bind</param>
        /// <param name="paramCount">Number of parameters in the method to bind</param>
        public EventPatchSite(Type type, string methodName, BindingFlags flags, int paramCount)
        {
            var method = type.GetMethods(flags).First(mi => mi.Name == methodName && mi.GetParameters().Length == paramCount);
            PatchSite = method ?? throw new ArgumentException($"Could not find patch site for {type.FullName}.{methodName} with {paramCount} parameters!");
        }

        /// <summary>
        /// Uses a list of BindingFlags combinations to try and find a method with the specified name.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        /// <returns>MethodBase of the found method, or null if none found</returns>
        private static MethodBase HuntForMethod(Type type, string methodName)
        {
            foreach (var rawFlag in Enum.GetValues(typeof(CommonBindingFlags)))
            {
                var flag = (BindingFlags)rawFlag;
                var method = type.GetMethod(methodName, flag);
                if (method != null)
                {
                    return method;
                }
            }

            return null;
        }
    }
}
