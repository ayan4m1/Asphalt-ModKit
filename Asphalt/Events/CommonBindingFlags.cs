using System.Reflection;

namespace Asphalt.Events
{
    /// <summary>
    /// Defines bitwise combinations of BindingFlags for common use cases.
    /// </summary>
    public struct CommonBindingFlags
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
}
