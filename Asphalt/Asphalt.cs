using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using Harmony;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin
    {
        public static HarmonyInstance Harmony { get; protected set; }

        static Asphalt()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Patch injections for default Services onEnable etc.
            Harmony = HarmonyInstance.Create("com.eco.mods.asphalt");
            //Harmony.PatchAll(Assembly.GetExecutingAssembly());

            if (File.Exists("dumpdlls.txt"))
            {
                DllDumper.DumpDlls();
            }

            Log.WriteLine("Finished initializing Asphalt type");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            try
            {
                if (!(eventArgs.ExceptionObject is AggregateException)) return;
                var ex = eventArgs.ExceptionObject as AggregateException;
                foreach (var inner in ex.InnerExceptions)
                {
                    var typeLoadException = ((inner as TargetInvocationException).InnerException as TypeInitializationException).InnerException as ReflectionTypeLoadException;
                    foreach (var innerTypeLoadException in typeLoadException.LoaderExceptions)
                    {
                        Log.WriteErrorLine(innerTypeLoadException.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLine(ex.ToString());
            }
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs eventArgs)
        {
            Log.WriteLine($"Loaded {eventArgs.LoadedAssembly.FullName}");
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Log.WriteLine("Attempting an assembly resolve!");
            try
            {
                if (args.Name.Contains(".resources"))
                    return null;

                // check for assemblies already loaded
                Log.WriteLine("Looking for assy");
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                if (assembly != null)
                    return assembly;

                string filename = args.Name.Split(',')[0] + ".dll".ToLower();
                Log.WriteLine(filename);

                string asmFile = Path.Combine("Mods", filename);
                Log.WriteLine(asmFile);

                if (!File.Exists(asmFile))
                    return null;

                return Assembly.LoadFrom(asmFile);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLine(ex.ToString());
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
