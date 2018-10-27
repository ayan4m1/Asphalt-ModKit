using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Localization;
using System.ComponentModel;

namespace Asphalt.Events.PlayerEvents
{
    /// <summary>
    /// Called when a player unlearns a skill
    /// </summary>
    public class PlayerUnlearnSkillEvent : CancelEventArgs
    {
        public Player Player { get; set; }
        public Skill Skill { get; set; }

        public PlayerUnlearnSkillEvent(ref Player player, ref Skill skill)
        {
            Player = player;
            Skill = skill;
        }
    }

    [AtomicActionEventPatchSite(typeof(UnlearnSkillPlayerActionManager))]
    internal class PlayerUnlearnSkillEventEmitter : EventEmitter<PlayerUnlearnSkillEvent>
    {
        public static bool Prefix(ref Player player, ref Skill skill, ref IAtomicAction __result)
        {
            var evt = new PlayerUnlearnSkillEvent(ref player, ref skill);
            Emit(ref evt);

            if (evt.Cancel)
            {
                __result = new FailedAtomicAction(new LocString("Failed to unlearn skill!"));
            }

            return !evt.Cancel;
        }
    }
}
