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

        public RubbleSpawnEvent(ref RubbleObject obj) : base()
        {
            RubbleObject = obj;
        }
    }

    internal class RubbleSpawnEventHelper
    {
        public static void Postfix(ref IEcoObject ecoObject)
        {
            if (!(ecoObject is RubbleObject))
                return;

            RubbleObject rubble = (RubbleObject)ecoObject;

            RubbleSpawnEvent sre = new RubbleSpawnEvent(ref rubble);
            EventArgs sreEvent = sre;

            EventManager.CallEvent(ref sreEvent);

            if (sre.Cancel)
                typeof(RubbleObject).GetMethod("Destroy", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(rubble, new object[] { });

        }
    }
}
