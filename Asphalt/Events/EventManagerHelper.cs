using Asphalt.Utils;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.PlayerEvents;
using Asphalt.Events.RpcEvents;
using Asphalt.Events.WorldEvents;
using Asphalt.Events.WorldObjectEvents;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Networking;
using System;
using System.Linq;

namespace Asphalt.Events
{
    internal static class EventManagerHelper
    {
        internal static void InjectEvent(Type pEventType)
        {
            switch (pEventType.Name) //We hope Event names are unique
            {
                // Inventory Events
                case nameof(InventoryChangeSelectedSlotEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(SelectionInventory), typeof(InventoryChangeSelectedSlotEventHelper), "SelectIndex");
                    break;
                case nameof(InventoryMoveItemEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(InventoryChangeSet), typeof(InventoryMoveItemEventHelper), "MoveStacks");
                    break;

                // Player Events
                case nameof(PlayerBuyEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(BuyPlayerActionManager), typeof(PlayerBuyEventHelper));
                    break;
                case nameof(PlayerClaimPropertyEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(ClaimPropertyPlayerActionManager), typeof(PlayerClaimPropertyEventHelper));
                    break;
                case nameof(PlayerCompleteContractEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(CompleteContractPlayerActionManager), typeof(PlayerCompleteContractEventHelper));
                    break;
                case nameof(PlayerCraftEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(CraftPlayerActionManager), typeof(PlayerCraftEventHelper));
                    break;
                case nameof(PlayerEatEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(Stomach), typeof(PlayerEatEventHelper), "Eat");
                    break;
                case nameof(PlayerGainSkillEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(GainSkillPlayerActionManager), typeof(PlayerGainSkillEventHelper));
                    break;
                case nameof(PlayerGetElectedEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(GetElectedPlayerActionManager), typeof(PlayerGetElectedEventHelper));
                    break;
                case nameof(PlayerHarvestEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(HarvestPlayerActionManager), typeof(PlayerHarvestEventHelper));
                    break;
                case nameof(PlayerInteractEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicStatic(typeof(InteractionExtensions), typeof(PlayerInteractEventHelper), "MakeContext");
                    break;
                case nameof(PlayerLoginEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLoginEventHelper), "Login");
                    break;
                case nameof(PlayerLogoutEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLogoutEventHelper), "Logout");
                    break;
                case nameof(PlayerPayTaxEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(PayTaxPlayerActionManager), typeof(PlayerPayTaxEventHelper));
                    break;
                case nameof(PlayerPickUpEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(PickUpPlayerActionManager), typeof(PlayerPickUpEventHelper));
                    break;
                case nameof(PlayerPlaceEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(PlacePlayerActionManager), typeof(PlayerPlaceEventHelper));
                    break;
                case nameof(PlayerProposeVoteEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(ProposeVotePlayerActionManager), typeof(PlayerProposeVoteEventHelper));
                    break;
                case nameof(PlayerReceiveGovernmentFundsEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(ReceiveGovernmentFundsPlayerActionManager), typeof(PlayerReceiveGovernmentFundsEventHelper));
                    break;
                case nameof(PlayerRunForElectionEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(RunForElectionPlayerActionManager), typeof(PlayerRunForElectionEventHelper));
                    break;
                case nameof(PlayerSellEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(SellPlayerActionManager), typeof(PlayerSellEventHelper));
                    break;
                case nameof(PlayerSendMessageEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(MessagePlayerActionManager), typeof(PlayerSendMessageEventHelper));
                    break;
                case nameof(PlayerTeleportEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(Player), typeof(PlayerTeleportEventHelper), "SetPosition");
                    break;
                case nameof(PlayerUnlearnSkillEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(UnlearnSkillPlayerActionManager), typeof(PlayerUnlearnSkillEventHelper));
                    break;
                case nameof(PlayerVoteEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(VotePlayerActionManager), typeof(PlayerVoteEventHelper));
                    break;

                // RPC Events
                case nameof(RpcInvokeEvent):
                    InjectionUtils.Install(typeof(RPCManager).GetMethods(InjectionUtils.PUBLIC_STATIC).First(mi => mi.Name == "InvokeOn" && mi.GetParameters().Length == 5), typeof(RpcInvokeEventHelper));
                    break;

                // World Events
                case nameof(WorldPolluteEvent):
                    InjectionUtils.InstallCreateAtomicAction(typeof(PolluteAirPlayerActionManager), typeof(WorldPolluteEventHelper));
                    break;

                // WorldObject Events
                case nameof(RubbleSpawnEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicStatic(typeof(EcoObjectManager), typeof(RubbleSpawnEventHelper), "Add");
                    break;
                case nameof(TreeFellEvent):
                    InjectionUtils.InstallWithOriginalHelperNonPublicInstance(typeof(TreeEntity), typeof(TreeFellEventHelper), "FellTree");
                    break;
                case nameof(TreeChopEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(TreeEntity), typeof(TreeChopEventHelper), "TryApplyDamage");
                    break;
                case nameof(WorldObjectChangeTextEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(CustomTextComponent), typeof(WorldObjectChangeTextEventHelper), "SetText");
                    break;
                case nameof(WorldObjectDestroyedEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectDestroyedEventHelper), "Destroy");
                    break;
                case nameof(WorldObjectEnabledChangedEvent):
                    InjectionUtils.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectEnabledChangedEventHelper), "set_Enabled");
                    break;
                case nameof(WorldObjectNameChangedEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectNameChangedEventHelper), "SetName");
                    break;
                case nameof(WorldObjectOperatingChangedEvent):
                    InjectionUtils.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectOperatingChangedEventHelper), "set_Operating");
                    break;
                case nameof(WorldObjectPickupEvent):
                    InjectionUtils.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectPickupEventHelper), "TryPickUp");
                    break;
            }
        }
    }
}
