using Eco.Gameplay.Players;
using Eco.Shared.Networking;
using System;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    ///  Called when the loading screen of a user appears
    /// </summary>
    public class PlayerLoginEvent : EventArgs
    {
        public Player Player { get; protected set; }
        public INetClient Client { get; protected set; }

        public PlayerLoginEvent(Player player, INetClient netClient)
        {
            Player = player;
            Client = netClient;
        }
    }

    [EventPatchSite(typeof(User), "Login")]
    internal class PlayerLoginEventEmitter : EventEmitter<PlayerLoginEvent>
    {
        public static void Prefix(Player player, INetClient client)
        {
            var evt = new PlayerLoginEvent(player, client);
            Emit(evt);
        }
    }
}
