using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerVoteEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerVoteEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerVoteEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerVoteEvent cEvent = new PlayerVoteEvent(ref user);
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
