using Eco.Gameplay.Interactions;
using Eco.Shared.Networking;
using System;
using System.ComponentModel;

namespace Asphalt.Events.WorldObjectEvents
{
    /// <summary>
    /// Called when a tree (or stump, branch, slice) is hit with an axe.
    /// </summary>
    public class TreeChopEvent : CancelEventArgs
    {
        public TreeEntity TreeEntity { get; set; }
        public INetObject Damager { get; set; }
        public float DamageAmount { get; set; }
        public InteractionContext Context { get; set; }

        public TreeChopEvent(ref TreeEntity tree, ref INetObject damager, ref float amount, ref InteractionContext context)
        {
            TreeEntity = tree;
            Damager = damager;
            DamageAmount = amount;
            Context = context;
        }
    }

    [EventPatchSite(typeof(TreeEntity), "TryApplyDamage", CommonBindingFlags.Instance)]
    internal class TreeChopEventEmitter : EventEmitter<TreeChopEvent>
    {
        public static bool Prefix(ref TreeEntity __instance, INetObject damager, float amount, InteractionContext context)
        {
            var evt = new TreeChopEvent(ref __instance, ref damager, ref amount, ref context);
            Emit(ref evt);

            return !evt.Cancel;
        }
    }
}
