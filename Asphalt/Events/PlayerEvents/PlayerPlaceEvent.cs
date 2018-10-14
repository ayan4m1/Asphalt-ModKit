using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
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

        public PlayerPlaceEvent(ref Player pPlayer, ref BlockItem pPlacedItem, ref Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.Item = pPlacedItem;
            this.Position = pPosition;
        }
    }

    internal class PlayerPlaceEventHelper
    {
        public static bool Prefix(ref Player player, ref BlockItem placedItem, ref Vector3i position, ref IAtomicAction __result)
        {
            PlayerPlaceEvent cEvent = new PlayerPlaceEvent(ref player, ref placedItem, ref position);
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
