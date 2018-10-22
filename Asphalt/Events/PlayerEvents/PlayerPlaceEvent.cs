using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerPlaceEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public BlockItem Item { get; set; }
        public Vector3i Position { get; set; }

        public PlayerPlaceEvent(ref Player player, ref BlockItem item, ref Vector3i position)
        {
            Player = player;
            Item = item;
            Position = position;
        }
    }

    [AtomicActionEventPatchSite(typeof(PlacePlayerActionManager))]
    internal class PlayerPlaceEventEmitter : EventEmitter<PlayerPlaceEvent>
    {
        public static bool Prefix(ref Player player, ref BlockItem item, ref Vector3i position, ref IAtomicAction __result)
        {
            var evt = new PlayerPlaceEvent(ref player, ref item, ref position);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to place!"));
            }

            return !evt.Cancel;
        }
    }
}
