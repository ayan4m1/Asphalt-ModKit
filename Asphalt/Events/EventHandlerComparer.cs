using System.Collections.Generic;

namespace Asphalt.Events
{
    internal class EventHandlerComparer : IComparer<EventHandlerData>
    {
        public int Compare(EventHandlerData x, EventHandlerData y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
