﻿using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using Harmony;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin
    {
        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            //Patch injections for default Services onEnable etc.
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());


                if (File.Exists("dumpdlls.txt"))
                    DllDumper.DumpDlls();
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                if (typeLoadException.LoaderExceptions != null)
                    foreach (Exception le in typeLoadException.LoaderExceptions)
                    {
                        Log.WriteErrorLine(le.ToStringPretty());
                    }
                throw;
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                if (args.Name.Contains(".resources"))
                    return null;

                // check for assemblies already loaded
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                if (assembly != null)
                    return assembly;

                string filename = args.Name.Split(',')[0] + ".dll".ToLower();

                string asmFile = Path.Combine("Mods", filename);

                if (!File.Exists(asmFile))
                    return null;

                return Assembly.LoadFrom(asmFile);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetStatus()
        {
            return "Initialized!";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
