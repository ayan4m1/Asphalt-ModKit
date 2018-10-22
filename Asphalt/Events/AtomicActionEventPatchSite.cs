using System;

namespace Asphalt.Events
{
    public class AtomicActionEventPatchSite : EventPatchSite
    {
        public AtomicActionEventPatchSite(Type eventType) : base(eventType, "CreateAtomicAction", CommonBindingFlags.Instance) { }
    }
}
