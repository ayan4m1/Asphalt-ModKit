using Eco.Gameplay.Objects;
using System.ComponentModel;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectDestroyedEvent : CancelEventArgs
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectDestroyedEvent(ref WorldObject worldObject)
        {
            WorldObject = worldObject;
        }
    }

    [EventPatchSite(typeof(WorldObject), "Destroy", CommonBindingFlags.Instance)]
    internal class WorldObjectDestroyedEventEmitter : EventEmitter<WorldObjectDestroyedEvent>
    {
        public static bool Prefix(ref WorldObject __instance, ref bool __state)
        {
            var evt = new WorldObjectDestroyedEvent(ref __instance);
            Emit(ref evt);

            return !evt.Cancel;
        }

    }
}
