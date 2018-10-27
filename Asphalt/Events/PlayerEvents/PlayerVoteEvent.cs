using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerVoteEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerVoteEvent(ref User user)
        {
            User = user;
        }
    }
    
    [AtomicActionEventPatchSite(typeof(VotePlayerActionManager))]
    internal class PlayerVoteEventEmitter : EventEmitter<PlayerVoteEvent>
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            var evt = new PlayerVoteEvent(ref user);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to vote!"));
            }

            return !evt.Cancel;
        }
    }
}
