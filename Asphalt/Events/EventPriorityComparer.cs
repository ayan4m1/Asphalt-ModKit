using System.Collections.Generic;

namespace Asphalt.Events
{
    public class EventPriorityComparer : IComparer<EventBinding>
    {
        public int Compare(EventBinding x, EventBinding y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
