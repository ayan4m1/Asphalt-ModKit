using System;

namespace Asphalt.Events
{
    public abstract class EventEmitter<E> where E : EventArgs
    {
        protected static void Emit(E rawEvent)
        {
            EventHandlerRegistry.Handle(ref rawEvent);
        }

        protected static void Emit(ref E rawEvent)
        {
            EventHandlerRegistry.Handle(ref rawEvent);
        }
    }
}
