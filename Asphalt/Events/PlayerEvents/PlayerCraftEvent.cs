using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerCraftEvent : CancelEventArgs
    {
        public User User { get; set; }
        public CraftingComponent Table { get; set; }
        public Item Item { get; set; }

        public PlayerCraftEvent(ref User user, ref CraftingComponent table, ref Item item)
        {
            User = user;
            Table = table;
            Item = item;
        }
    }

    [AtomicActionEventPatchSite(typeof(CraftPlayerActionManager))]
    internal class PlayerCraftEventEmitter : EventEmitter<PlayerCraftEvent>
    {
        public static bool Prefix(ref User user, ref CraftingComponent table, ref Item item, ref IAtomicAction __result)
        {
            var evt = new PlayerCraftEvent(ref user, ref table, ref item);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString());
            }

            return !evt.Cancel;
        }
    }
}
