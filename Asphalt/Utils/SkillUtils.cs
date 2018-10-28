/*
 * Copyright (c) 2018 [Kirthos]
 * 
 * Created by Kirthos 04/29/2018
 */

using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using System;
using System.Linq;

namespace Asphalt.Utils
{
    public static class SkillUtils
    {
        /// <summary>
        /// Return true if the user has the level of the skillm return false if the user don't have the skill
        /// </summary>
        public static bool HasSkillLevel(User user, Type skillType, int level)
        {
            return user.Skillset.Skills.Any(s => s.Type == skillType && s.Level >= level);
        }

        /// <summary>
        /// Return the level of the skillType for the user
        /// </summary>
        public static int GetSkillLevel(User user, Type skillType)
        {
            return user.Skillset.Skills.FirstOrDefault(skill => skill.Type == skillType)?.Level ?? 0;
        }
    }

}
