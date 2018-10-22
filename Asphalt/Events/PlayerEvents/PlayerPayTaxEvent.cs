using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerPayTaxEvent : CancelEventArgs
    {
        public User User { get; set; }
        public Currency Currency { get; set; }
        public float Amount { get; set; }

        public PlayerPayTaxEvent(ref User user, ref Currency currency, ref float amount)
        {
            User = user;
            Currency = currency;
            Amount = amount;
        }
    }

    [AtomicActionEventPatchSite(typeof(PayTaxPlayerActionManager))]
    internal class PlayerPayTaxEventEmitter : EventEmitter<PlayerPayTaxEvent>
    {
        public static bool Prefix(ref User user, ref Currency currency, ref float amount, ref IAtomicAction __result)
        {
            var evt = new PlayerPayTaxEvent(ref user, ref currency, ref amount);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to pay tax!"));
            }

            return !evt.Cancel;
        }
    }
}
