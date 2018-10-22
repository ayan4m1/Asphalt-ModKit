using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
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

        public WorldPolluteEvent(ref User user, ref AirPollutionComponent component, ref float value) : base()
        {
            User = user;
            Component = component;
            Value = value;
        }
    }

    [AtomicActionEventPatchSite(typeof(PolluteAirPlayerActionManager))]
    internal class WorldPolluteEventEmitter : EventEmitter<WorldPolluteEvent>
    {
        public static bool Prefix(ref User user, ref AirPollutionComponent obj, ref float value, ref IAtomicAction __result)
        {
            var evt = new WorldPolluteEvent(ref user, ref obj, ref value);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
            }

            return !evt.Cancel;
        }
    }
}
