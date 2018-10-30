using System.Collections.Generic;

namespace Asphalt.Events
{
    internal class EventMappingComparer : IComparer<EventMapping>
    {
        public int Compare(EventMapping x, EventMapping y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
