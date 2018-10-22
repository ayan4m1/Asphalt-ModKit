using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerSellEvent : CancelEventArgs
    {
        public User User { get; set; }
        public StoreComponent Store { get; set; }
        public Item Item { get; set; }

        public PlayerSellEvent(ref User user, ref StoreComponent store, ref Item item)
        {
            User = user;
            Store = store;
            Item = item;
        }
    }

    [AtomicActionEventPatchSite(typeof(SellPlayerActionManager))]
    internal class PlayerSellEventEmitter : EventEmitter<PlayerSellEvent>
    {
        public static bool Prefix(ref User user, ref StoreComponent store, ref Item item, ref IAtomicAction __result)
        {
            var evt = new PlayerSellEvent(ref user, ref store, ref item);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to sell!"));
            }

            return !evt.Cancel;
        }
    }
}
