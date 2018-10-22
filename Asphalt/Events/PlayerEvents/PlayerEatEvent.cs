using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerEatEvent : CancelEventArgs
    {
        public User User { get; set; }
        public FoodItem FoodItem { get; set; }
        public Stomach Stomach { get; protected set; } //protected because we can't change it

        public PlayerEatEvent(ref User user, ref FoodItem foodItem, ref Stomach stomach)
        {
            User = user;
            FoodItem = foodItem;
            Stomach = stomach;
        }
    }

    [EventPatchSite(typeof(Stomach), "Eat", CommonBindingFlags.Instance)]
    internal class PlayerEatEventEmitter : EventEmitter<PlayerEatEvent>
    {
        public static bool Prefix(ref FoodItem food, ref Stomach __instance)
        {
            var evt = new PlayerEatEvent(ref __instance.Owner, ref food, ref __instance);
            Emit(ref evt);
            return !evt.Cancel;
        }
    }
}
