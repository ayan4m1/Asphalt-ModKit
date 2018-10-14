using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System.ComponentModel;
using System;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerProposeVoteEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerProposeVoteEvent(ref User pUser) : base()
        {
            User = pUser;
        }
    }

    internal class PlayerProposeVoteEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerProposeVoteEvent cEvent = new PlayerProposeVoteEvent(ref user);
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
