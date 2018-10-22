using System;
using System.Reflection;

namespace Asphalt.Events
{
    internal struct CommonBindingFlags
    {
        public const BindingFlags Static = BindingFlags.Public | BindingFlags.Static;
        public const BindingFlags Instance = BindingFlags.Public | BindingFlags.Instance;
        public const BindingFlags GetField = BindingFlags.Public | BindingFlags.GetField;
        public const BindingFlags GetProperty = BindingFlags.Public | BindingFlags.GetProperty;
        public const BindingFlags SetField = BindingFlags.Public | BindingFlags.SetField;
        public const BindingFlags SetProperty = BindingFlags.Public | BindingFlags.SetProperty;
        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;
        public const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventPatchSite : Attribute
    {
        public MethodBase PatchSite { get; private set; }

        public EventPatchSite(Type type, string methodName)
        {
            PatchSite = HuntForMethod(type, methodName);
            if (PatchSite == null)
            {
                throw new ArgumentException($"Could not find PatchSite for {type.FullName}.{methodName}");
            }
        }

        public EventPatchSite(Type type, string methodName, BindingFlags flags)
        {
            try
            {
                PatchSite = type.GetMethod(methodName, flags);
                if (PatchSite == null)
                {
                    PatchSite = HuntForMethod(type, methodName);
                    if (PatchSite == null)
                    {
                        throw new ArgumentException($"Could not locate patch site for {type.FullName}.{methodName}!");
                    }
                }
            } catch (Exception e)
            {
                throw new ArgumentException($"Could not set up patch site while patching {type.FullName}.{methodName}!", e);
            }
        }

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
