using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectOperatingChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectOperatingChangedEvent(WorldObject worldObject)
        {
            WorldObject = worldObject;
        }
    }

    [EventPatchSite(typeof(WorldObject), "set_Operating", CommonBindingFlags.PrivateInstance)]
    internal class WorldObjectOperatingChangedEventEmitter : EventEmitter<WorldObjectOperatingChangedEvent>
    {
        public static void Prefix(WorldObject __instance, ref bool __state)
        {
            __state = __instance.Operating;
        }

        public static void Postfix(WorldObject __instance, ref bool __state)
        {
            if (__state == __instance.Operating)
                return;

            Emit(new WorldObjectOperatingChangedEvent(__instance));
        }
    }
}
