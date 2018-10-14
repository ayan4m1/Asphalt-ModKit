using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerCompleteContractEvent : CancelEventArgs
    {
        public Player Player { get; set; }

        public PlayerCompleteContractEvent(ref Player pPlayer) : base()
        {
            this.Player = pPlayer;
        }
    }

    internal class PlayerCompleteContractEventHelper
    {
        public static bool Prefix(ref Player player, ref IAtomicAction __result)
        {
            PlayerCompleteContractEvent cEvent = new PlayerCompleteContractEvent(ref player);
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
