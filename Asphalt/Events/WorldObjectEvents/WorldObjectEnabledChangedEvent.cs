using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectEnabledChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectEnabledChangedEvent(WorldObject worldObject)
        {
            WorldObject = worldObject;
        }
    }

    [EventPatchSite(typeof(WorldObject), "set_Enabled", CommonBindingFlags.PrivateInstance)]
    internal class WorldObjectEnabledChangedEventEmitter : EventEmitter<WorldObjectEnabledChangedEvent>
    {
        public static void Prefix(WorldObject __instance, ref bool __state)
        {
            __state = __instance.Enabled;
        }

        public static void Postfix(WorldObject __instance, ref bool __state)
        {
            if (__state == __instance.Enabled)
                return;

            Emit(new WorldObjectEnabledChangedEvent(__instance));
        }

    }
}
