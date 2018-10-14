using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectNameChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectNameChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectNameChangedEventHelper
    {
        public static void Prefix(WorldObject __instance, ref string __state)
        {
            __state = __instance.Name;
        }

        public static void Postfix(WorldObject __instance, ref string __state)
        {
            if (__state == __instance.Name)
                return;

            WorldObjectNameChangedEvent cEvent = new WorldObjectNameChangedEvent(__instance);
            EventArgs EventArgs = cEvent;

            EventManager.CallEvent(ref EventArgs);
        }
    }
}
