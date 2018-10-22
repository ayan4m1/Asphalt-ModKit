using Eco.Gameplay.Interactions;
using Eco.Gameplay.Players;
using Eco.Shared.Items;
using Eco.Simulation.WorldLayers;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player interacts with something
    /// </summary>
    public class PlayerInteractEvent : CancelEventArgs
    {
        public InteractionContext Context { get; set; }

        public PlayerInteractEvent(ref InteractionContext context)
        {
            Context = context;
        }
    }

    [EventPatchSite(typeof(InteractionExtensions), "MakeContext", CommonBindingFlags.Static)]
    internal class PlayerInteractEventEmitter : EventEmitter<PlayerInteractEvent>
    {
        public static void Postfix(InteractionInfo info, ref InteractionContext __result)
        {
            var evt = new PlayerInteractEvent(ref __result);

            Emit(ref evt);

            if (evt.Cancel)
            {
                //we can not really cancel the event, but we remove all targets ;)

                //context.Target, context.SelectedItem, context.InteractableBlock, context.CarriedItem                    

                __result.Target = null;
                __result.SelectedItem = null;
                __result.Block = null;  // InteractableBlock
                __result.CarriedItem = null;

                if (info.BlockPosition.HasValue)
                    __result.Player.SendCorrection(info);

                //remove exp, because eco will add it
                __result.Player.User.XP -= DifficultySettings.Obj.Config.SkillPointsPerAction * (__result.Player.User.SkillRate / DifficultySettings.BaselineSkillpoints);

                //remove activity, because eco will add it
                WorldLayerManager.GetLayer(LayerNames.PlayerActivity)?.FuncAtWorldPos(__result.Player.Position.XZi, (pos, val) => val = Math.Max(0, val - 0.001f));
            }
        }
    }
}
