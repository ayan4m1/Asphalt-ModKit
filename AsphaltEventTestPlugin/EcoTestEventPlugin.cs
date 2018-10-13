using Asphalt.Api.Event;
using Eco.Core.Plugins.Interfaces;

namespace EcoTestEventPlugin
{
    public class EcoTestEventPlugin : IModKitPlugin, IServerPlugin
    {
        public static TestEventHandlers TestListener { get; protected set; }

        public EcoTestEventPlugin()
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
