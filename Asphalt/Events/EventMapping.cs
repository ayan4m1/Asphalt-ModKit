using System;
using System.Reflection;

namespace Asphalt.Events
{
    public struct EventMapping
    {
        public Type EventType;
        public object Instance;
        public bool AllowCancel;
        public MethodInfo Handler;
        public EventPriority Priority;
    }
}
