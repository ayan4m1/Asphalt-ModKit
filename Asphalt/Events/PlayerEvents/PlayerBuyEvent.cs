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
    public class PlayerBuyEvent : CancelEventArgs
    {
        public User User { get; set; }
        public StoreComponent Store { get; set; }
        public Item Item { get; set; }

        public PlayerBuyEvent(ref User pUser, ref StoreComponent pStore, ref Item pItem) : base()
        {
            User = pUser;
            Store = pStore;
            Item = pItem;
        }
    }

    [AtomicActionEventPatchSite(typeof(BuyPlayerActionManager))]
    internal class PlayerBuyEventEmitter : EventEmitter<PlayerBuyEvent>
    {
        public static bool Prefix(ref User user, ref StoreComponent store, ref Item item, ref IAtomicAction __result)
        {
            var evt = new PlayerBuyEvent(ref user, ref store, ref item);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to buy!"));
            }

            return !evt.Cancel;
        }
    }
}
