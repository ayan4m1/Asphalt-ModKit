using Eco.Gameplay.Objects;
using System;
using System.ComponentModel;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectDestroyedEvent : CancelEventArgs
    {
        public WorldObject WorldObject { get; set; }

        public WorldObjectDestroyedEvent(ref WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectDestroyedEventHelper
    {
        public static bool Prefix(ref WorldObject __instance, ref bool __state)
        {
            WorldObjectDestroyedEvent dEvent = new WorldObjectDestroyedEvent(ref __instance);
            EventArgs args = dEvent;

            EventManager.CallEvent(ref args);

            if (dEvent.Cancel)
                return false;

            return true;
        }

    }
}
