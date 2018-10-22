using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player claims new property
    /// </summary>
    public class PlayerClaimPropertyEvent : CancelEventArgs
    {
        public Guid AuthId { get; set; }
        public User User { get; set; }
        public Vector2i Position { get; set; }

        public PlayerClaimPropertyEvent(ref Guid authId, ref User user, ref Vector2i position)
        {
            AuthId = authId;
            User = user;
            Position = position;
        }
    }

    [AtomicActionEventPatchSite(typeof(ClaimPropertyPlayerActionManager))]
    internal class PlayerClaimPropertyEventEmitter : EventEmitter<PlayerClaimPropertyEvent>
    {
        public static bool Prefix(ref Guid authId, ref User user, ref Vector2i position, ref IAtomicAction __result)
        {
            var evt = new PlayerClaimPropertyEvent(ref authId, ref user, ref position);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to claim property!"));
            }

            return !evt.Cancel;
        }
    }
}
