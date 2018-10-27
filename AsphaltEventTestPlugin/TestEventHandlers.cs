﻿using Asphalt.Events;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.PlayerEvents;
using Asphalt.Events.RpcEvents;
using Asphalt.Events.WorldEvents;
using Asphalt.Events.WorldObjectEvents;
using System;
using EventHandler = Asphalt.Events.EventHandler;

namespace AsphaltEventTestPlugin
{
    public class TestEventHandlers
    {
        [EventHandler(EventPriority.Normal, AllowCancel = true)]
        public void OnPlayerMessage(PlayerSendMessageEvent evt)
        {
            // evt.Cancel = true;
            Console.WriteLine(evt.Message.Text);
        }

        [EventHandler]
        public void OnPlayerInteract(PlayerInteractEvent evt)
        {
            evt.Cancel = true;
            Console.WriteLine(evt.Context.CarriedItem);
            Console.WriteLine(evt.Context.Player.FriendlyName);
        }

        [EventHandler]
        public void OnPlayerJoin(PlayerLoginEvent evt)
        {
            Console.WriteLine("Hello " + evt.Player.FriendlyName);
        }

        [EventHandler]
        public void OnPlayerTeleport(PlayerTeleportEvent evt)
        {
            Console.WriteLine("Teleport " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Position);
            // evt.Cancel = true;
        }

        [EventHandler]
        public void OnPlayerLogout(PlayerLogoutEvent evt)
        {
            Console.WriteLine("Bye " + evt.User.Name);
        }

        [EventHandler]
        public void OnPlayerCraft(PlayerCraftEvent evt)
        {
            Console.WriteLine("Craft " + evt.User.Player.FriendlyName);
            Console.WriteLine(evt.Table);
            // evt.Cancel = true;
        }

        [EventHandler]
        public void OnPlayerGainSkill(PlayerGainSkillEvent evt)
        {
            Console.WriteLine("GainSkill " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Skill.FriendlyName);
            // evt.Cancel = true;
        }

        [EventHandler]
        public void OnPlayerUnlearnSkill(PlayerUnlearnSkillEvent evt)
        {
            Console.WriteLine("UnlearnSkill " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Skill.FriendlyName);
            // evt.Cancel = true;
        }

        [EventHandler]
        public void OnPlayerClaimProperty(PlayerClaimPropertyEvent evt)
        {
            Console.WriteLine("ClaimProperty " + evt.User.Player.FriendlyName);
            Console.WriteLine(evt.Position);
            // evt.Cancel = true;
        }

        [EventHandler]
        public void OnWorldObjectDestroyed(WorldObjectDestroyedEvent evt)
        {
            Console.WriteLine("Destroyed" + evt.WorldObject.ToString());
        }

        [EventHandler]
        public void OnWorldObjectEnabled(WorldObjectEnabledChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
            //   Console.WriteLine(evt.User);
            //   Console.WriteLine(evt.Value);
            //    evt.Cancel = true;
            //     evt.Value = 1;
        }

        [EventHandler]
        public void OnWorldObjectNameChanged(WorldObjectNameChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
        }

        [EventHandler]
        public void OnWorldObjectOperatingChanged(WorldObjectOperatingChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
        }

        [EventHandler]
        public void OnWorldPollute(WorldPolluteEvent evt)
        {
            evt.Cancel = true;
            Console.WriteLine(evt.User.ToString());
        }

        [EventHandler]
        public void OnPlayerEatEvent(PlayerEatEvent evt)
        {
            Console.WriteLine(evt.FoodItem);
        }

        [EventHandler]
        public void OnPlayerSendMessageEvent(PlayerSendMessageEvent evt)
        {
            Console.WriteLine($"{evt.User.Name} sent message '{evt.Message}'");
            evt.Message.Text = "Test";
        }

        [EventHandler]
        public void OnInventoryMoveItemEvent(InventoryMoveItemEvent evt)
        {
            Console.Write($"InventoryMoveItemEvent: ");
            if (evt.User != null) Console.Write($"{evt.User.Name}");
            Console.Write($" moved from {evt.SourceStack.Quantity}x {evt.SourceStack.Item.FriendlyName}");
            if (!evt.DestinationStack.Empty) Console.Write($" to {evt.DestinationStack.Quantity}x {evt.DestinationStack.Item.FriendlyName}");
            // evt.Cancel = true;
        }


        [EventHandler]
        public void OnInventoryChangeSelectedSlotEvent(InventoryChangeSelectedSlotEvent evt)
        {
            Console.Write($"InventoryChangeSelectedSlotEvent: {evt.Player.FriendlyName} changed to slot {evt.SelectedSlot}");
            if (!evt.SelectedStack.Empty) Console.Write($" with {evt.SelectedStack.Quantity}x {evt.SelectedStack.Item.FriendlyName}");
            Console.WriteLine("");
        }


        [EventHandler]
        public void OnSignChangeEvent(WorldObjectChangeTextEvent evt)
        {
            Console.WriteLine($"SignChangeEvent: {evt.Player.FriendlyName} set text to {evt.Text}");
        }

        [EventHandler]
        public void OnSpawnRubbleEvent(RubbleSpawnEvent evt)
        {
            Console.WriteLine($"SpawnRubbleEvent: spawned {evt.RubbleObject.GetType().ToString()} at {evt.RubbleObject.Position.ToString()}");
            //if (evt.RubbleObject.IsBreakable)
            //    evt.RubbleObject.Breakup();
            //evt.Cancel = true;
        }

        [EventHandler]
        public void OnSpawnRubbleEvent(RpcInvokeEvent evt)
        {
            Console.WriteLine($"rpc received {evt.MethodName}");
        }
    }
}
