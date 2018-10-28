using System;
using System.Reflection;

namespace Asphalt.Events
{
    public struct EventBinding
    {
        public Type EventType { get; private set; }
        public bool AllowCancel { get; private set; }
        public EventPriority Priority { get; private set; }
        public MethodInfo Handler { get; private set; }
        public object HandlerInstance { get; private set; }

        public static EventBinding FromHandler(object instance, MethodInfo handler)
        {
            var instanceType = instance.GetType();
            var eventHandlerAttribute = handler.GetCustomAttribute<EventHandlerAttribute>();
            if (eventHandlerAttribute == null)
            {
                throw new ArgumentException($"EventHandlerAttribute missing on {instanceType.FullName}!");
            }

            var paramList = handler.GetParameters();
            if (paramList.Length == 0)
            {
                throw new ArgumentException($"{instanceType.FullName}.{handler.Name} does not accept a single argument!");
            }

            var eventType = paramList[0].ParameterType;
            return new EventBinding()
            {
                EventType = eventType,
                HandlerInstance = instance,
                Handler = handler,
                AllowCancel = eventHandlerAttribute.AllowCancel,
                Priority = eventHandlerAttribute.Priority
            };
        }
    }
}
