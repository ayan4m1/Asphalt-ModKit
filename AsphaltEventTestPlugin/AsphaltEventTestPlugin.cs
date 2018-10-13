using Asphalt.Api.Event;
using Eco.Core.Plugins.Interfaces;

namespace AsphaltEventTestPlugin
{
    public class AsphaltEventTestPlugin : IModKitPlugin, IServerPlugin
    {
        public static TestEventHandlers TestListener { get; protected set; }

        public AsphaltEventTestPlugin()
        {
            TestListener = new TestEventHandlers();

            EventManager.RegisterListener(TestListener);
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
