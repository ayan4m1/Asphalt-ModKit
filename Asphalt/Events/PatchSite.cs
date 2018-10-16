using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    class PatchSite
    {
        public MethodInfo Method;

        public PatchSite(Type patchType, string patchMethod, BindingFlags patchMethodType)
        {
            Method = patchType.GetMethod(patchMethod, patchMethodType);
        }
    }
}
