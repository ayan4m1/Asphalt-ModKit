using Eco.Shared.Serialization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.RpcEvents
{
    /// <summary>
    /// Called when an RPC-Event gets invoked
    /// </summary>
    public class RpcInvokeEvent : CancelEventArgs
    {
        public string Methodname { get; protected set; }
        public BSONObject Bson { get; protected set; }

        public RpcInvokeEvent(string methodname, BSONObject bson) : base()
        {
            Methodname = methodname;
            Bson = bson;
        }
    }

    internal class RpcInvokeEventHelper
    {
        public static bool Prefix(ref string methodname, ref BSONObject bson, object __result)
        {
            RpcInvokeEvent rie = new RpcInvokeEvent(methodname, bson);
            EventArgs rieEvent = rie;

            EventManager.CallEvent(ref rieEvent);

            if (rie.Cancel)
            {
                __result = null;
                return false;
            }

            return true;
        }
    }
}
