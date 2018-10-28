using System;

namespace Asphalt.Events
{
    public class AtomicActionEventPatchSiteAttribute : EventPatchSiteAttribute
    {
        public AtomicActionEventPatchSiteAttribute(Type eventType) : base(eventType, "CreateAtomicAction", CommonBindingFlags.Instance) { }
    }
}
