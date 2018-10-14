using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
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
            this.User = pUser;
            this.Store = pStore;
            this.Item = pItem;
        }
    }

    internal class PlayerBuyEventHelper
    {
        public static bool Prefix(ref User user, ref StoreComponent store, ref Item item, ref IAtomicAction __result)
        {
            PlayerBuyEvent cEvent = new PlayerBuyEvent(ref user, ref store, ref item);
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
