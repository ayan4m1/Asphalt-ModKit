using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System.ComponentModel;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when an Item in an Inventory gets moved
    /// </summary>
    public class InventoryMoveItemEvent : CancelEventArgs
    {
        public ItemStack SourceStack { get; protected set; }
        public ItemStack DestinationStack { get; protected set; }
        public User User { get; protected set; }

        public InventoryMoveItemEvent(ItemStack source, ItemStack destination, User user)
        {
            SourceStack = source;
            DestinationStack = destination;
            User = user;
        }
    }

    [EventPatchSite(typeof(InventoryChangeSet), "MoveStacks")]
    internal class InventoryMoveItemEventEmitter : EventEmitter<InventoryMoveItemEvent>
    {
        public static bool Prefix(ItemStack source, ItemStack destination, User user)
        {
            var evt = new InventoryMoveItemEvent(source, destination, user);
            Emit(ref evt);
            return !evt.Cancel;
        }
    }
}
