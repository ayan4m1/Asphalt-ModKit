using Eco.Gameplay.Skills;

namespace AsphaltFindEasterEggsPlugin
{
    public class TestSkill : Skill
    {
        public TestSkill()
        {
            AsphaltFindEasterEggsPlugin.ConfigStorage.Get("test");
        }
    }
}
