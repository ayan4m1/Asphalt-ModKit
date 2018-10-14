﻿using Eco.Gameplay.Objects;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    public class WorldObjectOperatingChangedEvent : EventArgs
    {
        public WorldObject WorldObject { get; protected set; }

        public WorldObjectOperatingChangedEvent(WorldObject pWorldObject) : base()
        {
            WorldObject = pWorldObject;
        }
    }

    internal class WorldObjectOperatingChangedEventHelper
    {
        public static void Prefix(WorldObject __instance, ref bool __state)
        {
            __state = __instance.Operating;
        }

        public static void Postfix(WorldObject __instance, ref bool __state)
        {
            if (__state == __instance.Operating)
                return;

            WorldObjectOperatingChangedEvent cEvent = new WorldObjectOperatingChangedEvent(__instance);
            EventArgs EventArgs = cEvent;

            EventManager.CallEvent(ref EventArgs);
        }
    }
}
