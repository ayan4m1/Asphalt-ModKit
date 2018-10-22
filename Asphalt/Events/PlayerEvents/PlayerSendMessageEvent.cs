using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
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

        public PlayerSendMessageEvent(ref User user, ref ChatMessage message)
        {
            User = user;
            Message = message;
        }
    }

    [AtomicActionEventPatchSite(typeof(MessagePlayerActionManager))]
    internal class PlayerSendMessageEventEmitter : EventEmitter<PlayerSendMessageEvent>
    {
        public static bool Prefix(ref User user, ref ChatMessage message, ref IAtomicAction __result)
        {
            var evt = new PlayerSendMessageEvent(ref user, ref message);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to send message!"));
            }

            return !evt.Cancel;
        }
    }
}
