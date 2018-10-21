using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when the Player changes the Selected Hotbar Slot
    /// </summary>
    public class InventoryChangeSelectedSlotEvent : EventArgs
    {
        public Player Player { get; protected set; }
        public int SelectedSlot { get; protected set; }
        public ItemStack SelectedStack { get; protected set; }
        public SelectionInventory Inventory { get; protected set; }

        public InventoryChangeSelectedSlotEvent(int slot, Player player, ItemStack itemStack, SelectionInventory inv)
        {
            SelectedSlot = slot;
            Player = player;
            SelectedStack = itemStack;
            Inventory = inv;
        }
    }
    
    [EventPatchSite(typeof(SelectionInventory), "SelectIndex")]
    internal class InventoryChangeSelectedSlotEventEmitter : EventEmitter<InventoryChangeSelectedSlotEvent>
    {
        public static void Prefix(Player player, int slot, SelectionInventory __instance)
        {
            Emit(new InventoryChangeSelectedSlotEvent(slot, player, __instance.SelectedStack, __instance));
        }
    }
}
