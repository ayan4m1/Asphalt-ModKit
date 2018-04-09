﻿/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 29, 2018
* ------------------------------------
**/

using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Util;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Utils;
using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        public static bool IsInitialized { get; protected set; }

        public Asphalt()
        {
            if (!IsAdministrator)
                Log.WriteError("If Asphalt is not working, try running Eco as Administrator!");

            Injection.InstallCreateAtomicAction(typeof(BuyPlayerActionManager), typeof(PlayerBuyEventHelper));
            Injection.InstallCreateAtomicAction(typeof(ClaimPropertyPlayerActionManager), typeof(PlayerClaimPropertyEventHelper));
            Injection.InstallCreateAtomicAction(typeof(CompleteContractPlayerActionManager), typeof(PlayerCompleteContractEventHelper));
            Injection.InstallCreateAtomicAction(typeof(CraftPlayerActionManager), typeof(PlayerCraftEventHelper));
            Injection.InstallCreateAtomicAction(typeof(GainSkillPlayerActionManager), typeof(PlayerGainSkillEventHelper));
            Injection.InstallCreateAtomicAction(typeof(GetElectedPlayerActionManager), typeof(PlayerGetElectedEventHelper));
            Injection.InstallCreateAtomicAction(typeof(HarvestPlayerActionManager), typeof(PlayerHarvestEventHelper));
            Injection.InstallWithOriginalHelperPublicStatic(typeof(InteractionExtensions), typeof(PlayerInteractEventHelper), "MakeContext");     
            Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLoginEventHelper), "Login");
            Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLogoutEventHelper), "Logout");
            Injection.InstallCreateAtomicAction(typeof(PayTaxPlayerActionManager), typeof(PlayerPayTaxEventHelper));
            Injection.InstallCreateAtomicAction(typeof(PickUpPlayerActionManager), typeof(PlayerPickUpEventHelper));
            Injection.InstallCreateAtomicAction(typeof(PlacePlayerActionManager), typeof(PlayerPlaceEventHelper));
            Injection.InstallCreateAtomicAction(typeof(ProposeVotePlayerActionManager), typeof(PlayerProposeVoteEventHelper));
            Injection.InstallCreateAtomicAction(typeof(ReceiveGovernmentFundsPlayerActionManager), typeof(PlayerReceiveGovernmentFundsEventHelper));
            Injection.InstallCreateAtomicAction(typeof(RunForElectionPlayerActionManager), typeof(PlayerRunForElectionEventHelper));
            Injection.InstallCreateAtomicAction(typeof(SellPlayerActionManager), typeof(PlayerSellEventHelper));
            Injection.InstallCreateAtomicAction(typeof(MessagePlayerActionManager), typeof(PlayerSendMessageEventHelper));
            Injection.InstallWithOriginalHelperPublicInstance(typeof(Player), typeof(PlayerTeleportEventHelper), "SetPosition");
            Injection.InstallCreateAtomicAction(typeof(UnlearnSkillPlayerActionManager), typeof(PlayerUnlearnSkillEventHelper));
            Injection.InstallCreateAtomicAction(typeof(VotePlayerActionManager), typeof(PlayerVoteEventHelper));

            Injection.InstallCreateAtomicAction(typeof(PolluteAirPlayerActionManager), typeof(WorldPolluteEventHelper));

            IsInitialized = true;
        }


        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public string GetStatus()
        {
            return IsInitialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
