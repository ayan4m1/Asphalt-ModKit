﻿using Asphalt.Utils;

namespace Asphalt.Storeable.Items
{
    public class SkillStoreable
    {
        public SkillLevelStoreable[] Levels;

        public SkillStoreable()
        {
        }

        public SkillStoreable(SkillLevelStoreable[] levels)
        {
            Levels = levels;
        }

        public T[] GetValues<T>(string pfName)
        {
            T[] result = new T[Levels.Length + 1];
            for (int i = 0; i < Levels.Length; i++)
            {
                result[i] = Injection.GetPropertyFieldValue<T>(Levels[i], pfName);
            }
            return result;
        }

        public int[] GetSkillPointCost()
        {
            int[] result = new int[Levels.Length];
            for (int i = 0; i < Levels.Length; i++)
            {
                result[i] = Levels[i].SkillPointCost;
            }
            return result;
        }
    }

    public class SkillLevelStoreable
    {
        public SkillLevelStoreable()
        {
        }

        public int SkillPointCost { get; set; }

        public SkillLevelStoreable(int skillPointCost)
        {
            SkillPointCost = skillPointCost;
        }
    }
}
