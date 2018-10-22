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

        public WorldObjectPickupEvent(ref WorldObject worldObject, ref Player picker)
        {
            WorldObject = worldObject;
            Picker = picker;
        }
    }

    [EventPatchSite(typeof(WorldObject), "TryPickUp", CommonBindingFlags.Instance)]
    internal class WorldObjectPickupEventEmitter : EventEmitter<WorldObjectPickupEvent>
    {
        public static bool Prefix(ref Player player, ref WorldObject __instance, ref IAtomicAction __result)
        {
            var evt = new WorldObjectPickupEvent(ref __instance, ref player);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldObjectPickupEvent)));
            }

            return !evt.Cancel;
        }
    }
}