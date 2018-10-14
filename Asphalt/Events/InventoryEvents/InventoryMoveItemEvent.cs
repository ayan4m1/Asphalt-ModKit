using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System;
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

        public InventoryMoveItemEvent(ItemStack source, ItemStack destination, User user) : base()
        {
            SourceStack = source;
            DestinationStack = destination;
            User = user;
        }
    }

    internal class InventoryMoveItemEventHelper
    {
        public static bool Prefix(ItemStack source, ItemStack destination, User user)
        {
            InventoryMoveItemEvent imie = new InventoryMoveItemEvent(source, destination, user);
            EventArgs imieEvent = imie;

            EventManager.CallEvent(ref imieEvent);

            if (imie.Cancel)
                return false;

            return true;
        }
    }
}
