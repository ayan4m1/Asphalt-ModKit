using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.WorldObjectEvents
{
    /// <summary>
    /// Called when a player attempts to pick up a WorldObject using a hammer
    /// </summary>
    public class WorldObjectPickupEvent : CancelEventArgs
    {
        public WorldObject WorldObject { get; set; }
        public Player Picker { get; set; }

        public WorldObjectPickupEvent(ref WorldObject worldObject, ref Player picker) : base()
        {
            WorldObject = worldObject;
            Picker = picker;
        }
    }

    internal class WorldObjectPickupEventHelper
    {
        public static bool Prefix(ref Player player, ref WorldObject __instance, ref IAtomicAction __result)
        {
            var wope = new WorldObjectPickupEvent(ref __instance, ref player);
            var wopeEvent = (EventArgs)wope;

            EventManager.CallEvent(ref wopeEvent);

            if (wope.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldObjectPickupEvent)));
                return false;
            }

            return true;
        }
    }
}