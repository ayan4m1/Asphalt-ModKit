using System.Collections.Generic;

namespace Asphalt.Events
{
    internal class EventBindingComparer : IComparer<EventBinding>
    {
        public int Compare(EventBinding x, EventBinding y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
