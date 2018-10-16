using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Asphalt.Utils
{
    public static class DllDumpUtils
    {
        private static readonly string modDll = "Eco.Mods.dll";
        private static readonly string serverExe = "EcoServer.exe";
        private static readonly string[] assemblies = new[]
        {
            "Eco.Core.dll",
            "Eco.Gameplay.dll",
            "Eco.ModKit.dll",
            "Eco.Shared.dll",
            "Eco.Simulation.dll",
            "Eco.World.dll",
            "Eco.Stats.dll",
            "LiteDB.dll"
        };

        public static void Dump()
        {
            Dump(Assembly.GetEntryAssembly());
        }

        public static void Dump(Assembly serverAssembly)
        {
            var destDir = Path.Combine(Path.GetDirectoryName(serverAssembly.Location), "extracted");
            Directory.CreateDirectory(destDir);

            foreach (var assembly in assemblies)
            {
                var asmname = $"costura.{assembly}.compressed".ToLower();
                var destFileName = Path.Combine(destDir, assembly);

                File.Delete(destFileName);

                using (Stream stream = serverAssembly.GetManifestResourceStream(asmname))
                using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                using (FileStream destination = File.OpenWrite(destFileName))
                    deflateStream.CopyTo(destination);
            }

            File.Delete(Path.Combine(destDir, modDll));
            File.Copy(Path.Combine(Path.GetTempPath(), modDll), Path.Combine(destDir, modDll));

            File.Delete(Path.Combine(destDir, serverExe));
            File.Copy(serverAssembly.Location, Path.Combine(destDir, serverExe));
        }
    }
}
