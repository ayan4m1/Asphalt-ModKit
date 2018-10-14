using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerRunForElectionEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerRunForElectionEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerRunForElectionEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerRunForElectionEvent cEvent = new PlayerRunForElectionEvent(ref user);
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
