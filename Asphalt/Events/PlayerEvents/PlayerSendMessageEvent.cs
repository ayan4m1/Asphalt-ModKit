﻿using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player sends a chat message
    /// </summary>
    public class PlayerSendMessageEvent : CancelEventArgs
    {
        public User User { get; set; }

        public ChatMessage Message { get; set; }

        public PlayerSendMessageEvent(ref User user, ref ChatMessage message) : base()
        {
            this.User = user;
            this.Message = message;
        }
    }

    internal class PlayerSendMessageEventHelper
    {
        public static bool Prefix(ref User user, ref ChatMessage message, ref IAtomicAction __result)
        {
            PlayerSendMessageEvent cEvent = new PlayerSendMessageEvent(ref user, ref message);
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
