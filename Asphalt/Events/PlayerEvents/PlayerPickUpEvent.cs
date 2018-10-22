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
    public class PlayerPickUpEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public BlockItem PickedUpItem { get; set; }
        public Vector3i Position { get; set; }

        public PlayerPickUpEvent(ref Player player, ref BlockItem pickedUpItem, ref Vector3i position)
        {
            Player = player;
            PickedUpItem = pickedUpItem;
            Position = position;
        }
    }

    [AtomicActionEventPatchSite(typeof(PickUpPlayerActionManager))]
    internal class PlayerPickUpEventEmitter : EventEmitter<PlayerPickUpEvent>
    {
        public static bool Prefix(ref Player player, ref BlockItem pickedUpItem, ref Vector3i position, ref IAtomicAction __result)
        {
            var evt = new PlayerPickUpEvent(ref player, ref pickedUpItem, ref position);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to pick up!"));
            }

            return !evt.Cancel;
        }
    }
}
