using System;
using Eco.Gameplay.Players;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    ///  Called when the user logs out
    /// </summary>
    public class PlayerLogoutEvent : EventArgs
    {
        public User User { get; protected set; }  //protected because we can't change it

        public PlayerLogoutEvent(User user)
        {
            User = user;
        }
    }

    [EventPatchSite(typeof(User), "Logout")]
    internal class PlayerLogoutEventEmitter : EventEmitter<PlayerLogoutEvent>
    {
        public static void Prefix(User __instance)
        {
            var evt = new PlayerLogoutEvent(__instance);
            Emit(evt);
        }
    }
}
