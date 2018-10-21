using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerCompleteContractEvent : CancelEventArgs
    {
        public Player Player { get; set; }

        public PlayerCompleteContractEvent(ref Player pPlayer)
        {
            Player = pPlayer;
        }
    }

    [AtomicActionEventPatchSite(typeof(CompleteContractPlayerActionManager))]
    internal class PlayerCompleteContractEventEmitter : EventEmitter<PlayerCompleteContractEvent>
    {
        public static bool Prefix(ref Player player, ref IAtomicAction __result)
        {
            var evt = new PlayerCompleteContractEvent(ref player);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to complete contract!"));
            }

            return !evt.Cancel;
        }
    }
}
