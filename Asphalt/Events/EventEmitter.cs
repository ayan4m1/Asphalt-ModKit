using System;
using System.Reflection;

namespace Asphalt.Events
{
    public abstract class EventEmitter<E> where E : EventArgs
    {
        public abstract MethodBase PatchSite { get; }

        protected static void Emit(E rawEvent)
        {
            var e = rawEvent as EventArgs;
            EventManager.CallEvent(ref e);
        }
    }
}
