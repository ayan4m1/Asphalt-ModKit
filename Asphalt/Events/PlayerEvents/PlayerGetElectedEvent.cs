using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerGetElectedEvent : CancelEventArgs
    {
        public User User { get; set; }

        public PlayerGetElectedEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerGetElectedEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerGetElectedEvent cEvent = new PlayerGetElectedEvent(ref user);
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
