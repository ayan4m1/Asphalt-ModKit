using Asphalt.Service;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Utils
{
    //You can also call this class magic ;)
    public static class Injection
    {
        public const BindingFlags PUBLIC_STATIC = BindingFlags.Static | BindingFlags.Public;
        public const BindingFlags PUBLIC_INSTANCE = BindingFlags.Instance | BindingFlags.Public;
        public const BindingFlags NON_PUBLIC_INSTANCE = BindingFlags.Instance | BindingFlags.NonPublic;

        public static void InstallCreateAtomicAction(Type pTypeToReplace, Type pHelperType)
        {
            InstallWithOriginalHelperPublicInstance(pTypeToReplace, pHelperType, "CreateAtomicAction");
        }

        public static void InstallWithOriginalHelperPublicStatic(Type pTypeToReplace, Type pHelperType, string pMethodName)
        {
            Install(
                    pTypeToReplace.GetMethod(pMethodName, PUBLIC_STATIC),
                    pHelperType
                 );
        }

        public static void InstallWithOriginalHelperPublicInstance(Type pTypeToReplace, Type pHelperType, string pMethodName)
        {
            Install(
                    pTypeToReplace.GetMethod(pMethodName, PUBLIC_INSTANCE),
                    pHelperType
                 );
        }

        public static void InstallWithOriginalHelperNonPublicInstance(Type pTypeToReplace, Type pHelperType, string pMethodName)
        {
            Install(
                    pTypeToReplace.GetMethod(pMethodName, NON_PUBLIC_INSTANCE),
                    pHelperType
                 );
        }

        public static void Install(MethodInfo pMethodToReplace, Type pHelperType)
        {
            if (pMethodToReplace == null)
                throw new ArgumentNullException(nameof(pMethodToReplace));

            AsphaltPlugin.Harmony.Patch(
                pMethodToReplace,
                new HarmonyMethod(FindMethod(pHelperType, "Prefix")),
                new HarmonyMethod(FindMethod(pHelperType, "Postfix"))
            );
        }

        public static MethodInfo FindMethod(Type pType, string pName)
        {
            return pType.GetMethod(pName, NON_PUBLIC_INSTANCE) ?? pType.GetMethod(pName, PUBLIC_INSTANCE) ?? pType.GetMethod(pName, PUBLIC_STATIC);
        }

        public static IEnumerable<PropertyFieldInfo> GetPropertyFieldInfos(Type pServerPlugin, Type pType)
        {
            return pServerPlugin.GetProperties().Where(x => x.PropertyType == pType).Select(x => new PropertyFieldInfo(x))
                .Concat(pServerPlugin.GetFields().Where(x => x.FieldType == pType).Select(x => new PropertyFieldInfo(x)));
        }

        public static T GetPropertyFieldValue<T>(object pType, string pfName)
        {
            return (T)(pType.GetType().GetProperty(pfName).GetValue(pType) ?? pType.GetType().GetField(pfName).GetValue(pType));
        }
    }
}
