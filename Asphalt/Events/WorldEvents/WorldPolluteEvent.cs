using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.WorldEvents
{
    /// <summary>
    /// Called when something pollutes
    /// </summary>
    public class WorldPolluteEvent : CancelEventArgs
    {
        public User User { get; set; }
        public AirPollutionComponent Component { get; set; }
        public float Value { get; set; }

        public WorldPolluteEvent(ref User pUser, ref AirPollutionComponent pAirPollutionComponent, ref float pValue) : base()
        {
            User = pUser;
            Component = pAirPollutionComponent;
            Value = pValue;
        }
    }

    internal class WorldPolluteEventHelper
    {
        public static bool Prefix(ref User user, ref AirPollutionComponent obj, ref float value, ref IAtomicAction __result)
        {
            WorldPolluteEvent wpe = new WorldPolluteEvent(ref user, ref obj, ref value);
            EventArgs wpeEvent = wpe;

            EventManager.CallEvent(ref wpeEvent);

            if (wpe.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
                return false;
            }

            return true;
        }
    }
}
