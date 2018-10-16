using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace Asphalt.Events.InventoryEvents
{
    [TestFixture]
    public class EventTests
    {
        /*[Test]
        public void TestInventoryChangeSelectedSlotEvent()
        {
            var player = A.Fake<Player>();
            var slot = 0;
            var inventory = A.Fake<Inventory>();
            var stack = new ItemStack(new FriedTomatoesItem(), 1, inventory);
            var selection = new SelectionInventory(10);
            var specificEvent = new InventoryChangeSelectedSlotEvent(slot, player, stack, selection);
            var genericEvent = (EventArgs)specificEvent;

            EventManager.RegisterListener(new EventHandlerTest());
            EventManager.CallEvent(ref genericEvent);
        }

        internal class EventHandlerTest
        {
            [EventHandler]
            public void OnInventoryChangeSelectedSlot(InventoryChangeSelectedSlotEvent evt)
            {
                Assert.Pass();
            }
        }*/
    }
}
