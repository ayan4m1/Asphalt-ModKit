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

    internal class PlayerLogoutEventHelper
    {
        public static void Prefix(User __instance)
        {
            PlayerLogoutEvent cEvent = new PlayerLogoutEvent(__instance);
            EventArgs EventArgs = cEvent;

            EventManager.CallEvent(ref EventArgs);
        }
    }
}
