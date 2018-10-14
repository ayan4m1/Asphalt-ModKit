using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
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
            this.Player = pPlayer;
            this.Skill = pSkill;
        }
    }

    internal class PlayerGainSkillEventHelper
    {
        public static bool Prefix(ref Player player, ref Skill skill, ref IAtomicAction __result)
        {
            PlayerGainSkillEvent cEvent = new PlayerGainSkillEvent(ref player, ref skill);
            EventArgs args = cEvent;

            EventManager.CallEvent(ref args);

            if (cEvent.Cancel)
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
