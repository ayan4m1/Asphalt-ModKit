using System;
using System.Reflection;

namespace Asphalt.Events
{
    public struct EventBinding
    {
        public bool AllowCancel;
        public EventPriority Priority;
        public MethodInfo Handler;
        public Type HandlerType => Handler.DeclaringType;
    }
}
