using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectNameChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectNameChangedEvent(WorldObject worldObject)
        {
            WorldObject = worldObject;
        }
    }

    [EventPatchSite(typeof(WorldObject), "SetName", CommonBindingFlags.Instance)]
    internal class WorldObjectNameChangedEventEmitter : EventEmitter<WorldObjectNameChangedEvent>
    {
        public static void Prefix(WorldObject __instance, ref string __state)
        {
            __state = __instance.Name;
        }

        public static void Postfix(WorldObject __instance, ref string __state)
        {
            if (__state == __instance.Name)
                return;

            Emit(new WorldObjectNameChangedEvent(__instance));
        }
    }
}
