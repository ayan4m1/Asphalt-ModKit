﻿using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    public class PlayerEatEvent : CancelEventArgs
    {
        public User User { get; set; }

        public FoodItem FoodItem { get; set; }

        public Stomach Stomach { get; protected set; }  //protected because we can't change it

        public PlayerEatEvent(ref User pUser, ref FoodItem pFoodIten, ref Stomach pStomach) : base()
        {
            this.User = pUser;
            this.FoodItem = pFoodIten;
            this.Stomach = pStomach;
        }
    }

    internal class PlayerEatEventHelper
    {
        public static bool Prefix(ref FoodItem food, ref Stomach __instance)
        {
            PlayerEatEvent cEvent = new PlayerEatEvent(ref __instance.Owner, ref food, ref __instance);
            EventArgs args = cEvent;

            EventManager.CallEvent(ref args);

            return !cEvent.Cancel;
        }
    }
}
