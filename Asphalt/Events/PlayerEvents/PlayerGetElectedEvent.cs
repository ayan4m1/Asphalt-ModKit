using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
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
            User = pUser;
        }
    }

    [AtomicActionEventPatchSite(typeof(GetElectedPlayerActionManager))]
    internal class PlayerGetElectedEventEmitter : EventEmitter<PlayerGetElectedEvent>
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            var evt = new PlayerGetElectedEvent(ref user);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to get elected!"));
            }

            return !evt.Cancel;
        }
    }
}
