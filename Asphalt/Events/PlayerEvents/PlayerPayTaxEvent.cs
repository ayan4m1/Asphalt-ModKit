﻿using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
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

        public PlayerPayTaxEvent(ref User pUser, ref Currency pCurrency, ref float pAmount) : base()
        {
            this.User = pUser;
            this.Currency = pCurrency;
            this.Amount = pAmount;
        }
    }

    internal class PlayerPayTaxEventHelper
    {
        public static bool Prefix(ref User user, ref Currency currency, ref float amount, ref IAtomicAction __result)
        {
            PlayerPayTaxEvent cEvent = new PlayerPayTaxEvent(ref user, ref currency, ref amount);
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
