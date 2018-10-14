using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectEnabledChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectEnabledChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectEnabledChangedEventHelper
    {
        public static void Prefix(WorldObject __instance, ref bool __state)
        {
            __state = __instance.Enabled;
        }

        public static void Postfix(WorldObject __instance, ref bool __state)
        {
            if (__state == __instance.Enabled)
                return;

            WorldObjectEnabledChangedEvent cEvent = new WorldObjectEnabledChangedEvent(__instance);
            EventArgs EventArgs = cEvent;

            EventManager.CallEvent(ref EventArgs);
        }

    }
}
