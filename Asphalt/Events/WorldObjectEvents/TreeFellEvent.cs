using Eco.Shared.Networking;
using System;
using System.ComponentModel;

namespace Asphalt.Events.WorldObjectEvents
{
    /// <summary>
    /// Called when a Tree is felled.
    /// </summary>
    public class TreeFellEvent : CancelEventArgs
    {
        public TreeEntity TreeEntity { get; set; }
        public INetObject Killer { get; set; }

        public TreeFellEvent(ref TreeEntity tree, ref INetObject killer) : base()
        {
            TreeEntity = tree;
            Killer = killer;
        }
    }

    internal class TreeFellEventHelper
    {
        public static bool Prefix(ref TreeEntity __instance, ref INetObject killer)
        {
            var tfe = new TreeFellEvent(ref __instance, ref killer);
            var tfeEvent = (EventArgs)tfe;

            EventManager.CallEvent(ref tfeEvent);

            if (tfe.Cancel)
            {
                __instance.RPC("UpdateHP", __instance.Species.TreeHealth);
                return false;
            }

            return true;
        }
    }
}
