using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.ComponentModel;
using System.Linq;

namespace Asphalt.Events.RpcEvents
{
    /// <summary>
    /// Called when an RPC-Event gets invoked
    /// </summary>
    public class RpcInvokeEvent : CancelEventArgs
    {
        public string MethodName { get; protected set; }
        public BSONObject Bson { get; protected set; }

        public RpcInvokeEvent(string methodName, BSONObject bson)
        {
            MethodName = methodName;
            Bson = bson;
        }
    }

    [EventPatchSite(typeof(RPCManager), "InvokeOn", CommonBindingFlags.Static, 5)]
    internal class RpcInvokeEventEmitter : EventEmitter<RpcInvokeEvent>
    {
        public static bool Prefix(ref string methodname, ref BSONObject bson, object __result)
        {
            var evt = new RpcInvokeEvent(methodname, bson);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = null;
            }

            return !evt.Cancel;
        }
    }
}
