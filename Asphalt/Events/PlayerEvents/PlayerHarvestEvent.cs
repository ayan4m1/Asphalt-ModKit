using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using Eco.Simulation.Agents;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerHarvestEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public Organism Target { get; set; }

        public PlayerHarvestEvent(ref Player pPlayer, ref Organism pTarget)
        {
            this.Player = pPlayer;
            this.Target = pTarget;
        }
    }

    [AtomicActionEventPatchSite(typeof(HarvestPlayerActionManager))]
    internal class PlayerHarvestEventEmitter : EventEmitter<PlayerHarvestEvent>
    {
        public static bool Prefix(ref Player player, ref Organism target, ref IAtomicAction __result)
        {
            var evt = new PlayerHarvestEvent(ref player, ref target);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to harvest!"));
            }

            return !evt.Cancel;
        }
    }
}
