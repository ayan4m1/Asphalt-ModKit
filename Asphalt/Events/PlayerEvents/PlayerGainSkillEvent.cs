using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player gains a skill
    /// </summary>
    public class PlayerGainSkillEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public Skill Skill { get; set; }

        public PlayerGainSkillEvent(ref Player pPlayer, ref Skill pSkill) : base()
        {
            Player = pPlayer;
            Skill = pSkill;
        }
    }

    [AtomicActionEventPatchSite(typeof(GainSkillPlayerActionManager))]
    internal class PlayerGainSkillEventEmitter : EventEmitter<PlayerGainSkillEvent>
    {
        public static bool Prefix(ref Player player, ref Skill skill, ref IAtomicAction __result)
        {
            var evt = new PlayerGainSkillEvent(ref player, ref skill);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to gain skill!"));
            }

            return !evt.Cancel;
        }
    }
}
