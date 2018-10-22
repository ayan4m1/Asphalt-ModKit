using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System.ComponentModel;
using System;
using Eco.Gameplay.Stats.ConcretePlayerActions;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerProposeVoteEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerProposeVoteEvent(ref User user)
        {
            User = user;
        }
    }

    [AtomicActionEventPatchSite(typeof(ProposeVotePlayerActionManager))]
    internal class PlayerProposeVoteEventEmitter : EventEmitter<PlayerProposeVoteEvent>
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            var evt = new PlayerProposeVoteEvent(ref user);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to propose vote!"));
            }

            return !evt.Cancel;
        }
    }
}
