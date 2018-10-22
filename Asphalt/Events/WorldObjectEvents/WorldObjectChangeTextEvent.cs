using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using System;

namespace Asphalt.Events.WorldObjectEvents
{
    /// <summary>
    /// Called when an SignText gets set
    /// </summary>
    public class WorldObjectChangeTextEvent : EventArgs
    {
        public Player Player { get; protected set; }
        public WorldObject WorldObject { get; protected set; }
        public string Text { get; protected set; }

        public WorldObjectChangeTextEvent(Player player, WorldObject obj, string text)
        {
            Player = player;
            WorldObject = obj;
            Text = text;
        }
    }

    [EventPatchSite(typeof(CustomTextComponent), "SetText", CommonBindingFlags.Instance)]
    internal class WorldObjectChangeTextEventEmitter : EventEmitter<WorldObjectChangeTextEvent>
    {
        public static void Prefix(ref Player player, string text, ref CustomTextComponent __instance)
        {
            Emit(new WorldObjectChangeTextEvent(player, __instance.Parent, text));
        }
    }
}
