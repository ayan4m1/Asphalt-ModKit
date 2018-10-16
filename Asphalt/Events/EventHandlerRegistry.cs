using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    public static class EventHandlerRegistry
    {
        private static readonly Dictionary<Type, List<EventHandlerData>> handlers = new Dictionary<Type, List<EventHandlerData>>();
    }
}
