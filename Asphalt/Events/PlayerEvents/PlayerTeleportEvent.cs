using Eco.Gameplay.Players;
using Eco.Shared.Math;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    ///  Called when a player teleports
    /// </summary>
    public class PlayerTeleportEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public Vector3 Position { get; set; }

        public PlayerTeleportEvent(ref Player pPlayer, ref Vector3 pPosition) : base()
        {
            this.Player = pPlayer;
            this.Position = pPosition;
        }
    }

    internal class PlayerTeleportEventHelper
    {
        public static bool Prefix(ref Vector3 position, ref Player __instance)
        {
            PlayerTeleportEvent cEvent = new PlayerTeleportEvent(ref __instance, ref position);
            EventArgs args = cEvent;

            EventManager.CallEvent(ref args);

            return !cEvent.Cancel;
        }
    }
}
