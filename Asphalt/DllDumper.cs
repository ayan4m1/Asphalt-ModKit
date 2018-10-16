using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Asphalt
{
    public static class DllDumper
    {
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

            File.Delete(Path.Combine(destDir, "Eco.Mods.dll"));
            File.Copy(Path.Combine(Path.GetTempPath(), "Eco.Mods.dll"), Path.Combine(destDir, "Eco.Mods.dll"));

            File.Delete(Path.Combine(destDir, "EcoServer.exe"));
            File.Copy(serverAssembly.Location, Path.Combine(destDir, "EcoServer.exe"));
        }
    }
}
