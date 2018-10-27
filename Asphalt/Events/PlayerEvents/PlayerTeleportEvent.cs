using Eco.Gameplay.Players;
using Eco.Shared.Math;
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

        public PlayerTeleportEvent(ref Player player, ref Vector3 position)
        {
            Player = player;
            Position = position;
        }
    }
    [EventPatchSite(typeof(Player), "SetPosition", CommonBindingFlags.Instance)]
    internal class PlayerTeleportEventEmitter : EventEmitter<PlayerTeleportEvent>
    {
        public static bool Prefix(ref Vector3 position, ref Player __instance)
        {
            var evt = new PlayerTeleportEvent(ref __instance, ref position);
            Emit(ref evt);

            return !evt.Cancel;
        }
    }
}
