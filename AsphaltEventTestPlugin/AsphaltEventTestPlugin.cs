using Asphalt.Events;
using Eco.Core.Plugins.Interfaces;

namespace AsphaltEventTestPlugin
{
    public class AsphaltEventTestPlugin : IModKitPlugin, IServerPlugin
    {
        public static TestEventHandlers TestListener { get; protected set; }

        public AsphaltEventTestPlugin()
        {
            TestListener = new TestEventHandlers();

            PatchRegistry.RegisterHandlers(TestListener);
        }

        public string GetStatus()
        {
            return "";
        }

        public override string ToString()
        {
            return "EcoTestEventPlugin";
        }
    }
}
