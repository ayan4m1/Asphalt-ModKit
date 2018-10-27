﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Events
{
    /// <summary>
    /// Indicates that a Harmony patch should be installed using the targeted class as its source.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventPatchSite : Attribute
    {
        private static readonly List<FieldInfo> bindingFlags = typeof(CommonBindingFlags).GetFields(CommonBindingFlags.Static).ToList();

        public MethodBase PatchSite { get; private set; }

        /// <summary>
        /// Defines a patch site on the specified type with the specified name.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        public EventPatchSite(Type type, string methodName)
        {
            PatchSite = HuntForMethod(type, methodName) ?? throw new ArgumentException($"Could not find patch site for {type.FullName}.{methodName}"); ;
        }

        /// <summary>
        /// Defines a patch site on the specified type with the specified name and flags.
        /// </summary>
        /// <param name="type">Type that contains the method to bind</param>
        /// <param name="methodName">Name of the method to bind</param>
        /// <param name="flags">BindingFlags describing the method to bind</param>
        public EventPatchSite(Type type, string methodName, BindingFlags flags)
        {
            PatchSite = type.GetMethod(methodName, flags) ?? throw new ArgumentException($"Could not find patch site for {type.FullName}.{methodName}");
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
            var method = type.GetMethods(flags).FirstOrDefault(mi => mi.Name == methodName && mi.GetParameters().Length == paramCount);
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
            foreach (var rawFlag in bindingFlags)
            {
                // no need to pass a "this" as const fields are effectively static
                var flag = (BindingFlags)rawFlag.GetValue(null);
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
