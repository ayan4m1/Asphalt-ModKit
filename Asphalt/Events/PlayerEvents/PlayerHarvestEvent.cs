using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
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

        public PlayerHarvestEvent(ref Player pPlayer, ref Organism pTarget) : base()
        {
            this.Player = pPlayer;
            this.Target = pTarget;
        }
    }

    internal class PlayerHarvestEventHelper
    {
        public static bool Prefix(ref Player player, ref Organism target, ref IAtomicAction __result)
        {
            PlayerHarvestEvent cEvent = new PlayerHarvestEvent(ref player, ref target);
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
