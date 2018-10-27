using Eco.Shared.Networking;
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

        public TreeFellEvent(ref TreeEntity tree, ref INetObject killer)
        {
            TreeEntity = tree;
            Killer = killer;
        }
    }

    [EventPatchSite(typeof(TreeEntity), "FellTree", CommonBindingFlags.PrivateInstance)]
    internal class TreeFellEventEmitter : EventEmitter<TreeFellEvent>
    {
        public static bool Prefix(ref TreeEntity __instance, ref INetObject killer)
        {
            var evt = new TreeFellEvent(ref __instance, ref killer);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __instance.RPC("UpdateHP", __instance.Species.TreeHealth);
            }

            return !evt.Cancel;
        }
    }
}
