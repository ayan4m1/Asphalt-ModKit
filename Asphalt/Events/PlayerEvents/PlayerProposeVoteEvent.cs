﻿using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerProposeVoteEvent : CancellableEvent
    {
        public User User { get; set; }

        public PlayerProposeVoteEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerProposeVoteEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerProposeVoteEvent cEvent = new PlayerProposeVoteEvent(ref user);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
