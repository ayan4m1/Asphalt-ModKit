using System;
using System.Collections.Concurrent;

namespace Asphalt.Events
{
    public class EventHandlerQueue
    {
        private ConcurrentQueue<EventHandler> backingQueue = new ConcurrentQueue<EventHandler>();

        public void Raise(EventArgs e)
        {
            while (backingQueue.TryDequeue(out EventHandler handler))
            {
                handler.Invoke(this, e);
            }
        }
    }
}
