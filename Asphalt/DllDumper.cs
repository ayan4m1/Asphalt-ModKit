using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Asphalt
{
    public static class DllDumper
    {
        private static string[] assemblies = new[]
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

        public static void DumpDlls()
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            var destDir = Path.Combine(Path.GetDirectoryName(entryAssembly.Location), "extracted");
            Directory.CreateDirectory(destDir);

            foreach (var assembly in assemblies)
            {
                var asmname = $"costura.{assembly}.compressed".ToLower();
                var destFileName = Path.Combine(destDir, assembly);

                File.Delete(destFileName);

                using (Stream stream = entryAssembly.GetManifestResourceStream(asmname))
                using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                using (FileStream destination = File.OpenWrite(destFileName))
                    deflateStream.CopyTo(destination);
            }

            File.Delete(Path.Combine(destDir, "Eco.Mods.dll"));
            File.Copy(Path.Combine(Path.GetTempPath(), "Eco.Mods.dll"), Path.Combine(destDir, "Eco.Mods.dll"));

            File.Delete(Path.Combine(destDir, "EcoServer.exe"));
            File.Copy(entryAssembly.Location, Path.Combine(destDir, "EcoServer.exe"));
        }
    }
}
