using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
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

        public PlayerPickUpEvent(ref Player pPlayer, ref BlockItem pPickedUpItem, ref Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.PickedUpItem = pPickedUpItem;
            this.Position = pPosition;
        }
    }

    internal class PlayerPickUpEventHelper
    {
        public static bool Prefix(ref Player player, ref BlockItem pickedUpItem, ref Vector3i position, ref IAtomicAction __result)
        {
            PlayerPickUpEvent cEvent = new PlayerPickUpEvent(ref player, ref pickedUpItem, ref position);
            EventArgs args = cEvent;

            EventManager.CallEvent(ref args);

            if (cEvent.Cancel)
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
