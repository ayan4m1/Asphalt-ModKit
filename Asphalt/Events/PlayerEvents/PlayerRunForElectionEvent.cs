using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerRunForElectionEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerRunForElectionEvent(ref User user)
        {
            this.User = user;
        }
    }

    [AtomicActionEventPatchSite(typeof(RunForElectionPlayerActionManager))]
    internal class PlayerRunForElectionEventEmitter : EventEmitter<PlayerRunForElectionEvent>
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            var evt = new PlayerRunForElectionEvent(ref user);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to run for election!"));
            }

            return !evt.Cancel;
        }
    }
}
