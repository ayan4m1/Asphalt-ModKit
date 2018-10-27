using Eco.Gameplay.Objects;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Asphalt.Events.WorldObjectEvents
{
    /// <summary>
    /// Called when an RubbleObject is added to the World
    /// </summary>
    public class RubbleSpawnEvent : CancelEventArgs
    {
        public RubbleObject RubbleObject { get; set; }

        public RubbleSpawnEvent(ref RubbleObject rubbleObject)
        {
            RubbleObject = rubbleObject;
        }
    }

    [EventPatchSite(typeof(EcoObjectManager), "Add", CommonBindingFlags.Static)]
    internal class RubbleSpawnEventEmitter : EventEmitter<RubbleSpawnEvent>
    {
        public static void Postfix(ref IEcoObject ecoObject)
        {
            if (!(ecoObject is RubbleObject rubble))
                return;

            var evt = new RubbleSpawnEvent(ref rubble);
            Emit(ref evt);

            if (evt.Cancel)
            {
                typeof(RubbleObject).GetMethod("Destroy", CommonBindingFlags.PrivateInstance).Invoke(rubble, new object[] { });
            }
        }
    }
}
