using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    public class EventPatch<T> where T : EventArgs
    {
        public T Event;
        public bool Patched { get; private set; }
        public MethodBase PatchMethod;
        public MethodInfo Prefix;
        public MethodInfo Postfix;

        public EventPatch(MethodBase patchMethod, MethodInfo prefix, MethodInfo postfix)
        {
            Patched = false;
            PatchMethod = patchMethod;
            Prefix = prefix;
            Postfix = postfix;
        }

        public void Patch()
        {
            if (Patched) return;

            AsphaltPlugin.Harmony.Patch(
                PatchMethod,
                new HarmonyMethod(Prefix),
                new HarmonyMethod(Postfix)
            );

            Patched = true;
        }
    }
}
