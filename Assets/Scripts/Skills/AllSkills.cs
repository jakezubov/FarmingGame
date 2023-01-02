using UnityEngine;

public class AllSkills : MonoBehaviour
{
    public Skill _combat, _magic, _farming, _mining, _woodcutting, _fishing, _crafting;   
    private readonly int _requiredExp = 100;  

    public void GainExp(int experience, Skill skill)
    {
        skill.AddToCurrentExp(experience);
        if (skill.GetCurrentExp() >= _requiredExp * skill.GetLevel())
        {
            int leftoverExperience = skill.GetCurrentExp() - (_requiredExp * skill.GetLevel());
            skill.LevelUp();           
            skill.SetExpBarMax(_requiredExp * skill.GetLevel());
            skill.SetCurrentExp(leftoverExperience);
        }
    }

    public int GetSkillLevel(Skill skill)
    {
        return skill.GetLevel();
    }

    public void FreeExp()
    {
        // For debugging purposes
        GainExp(_requiredExp * _combat.GetLevel(), _combat);
        GainExp(_requiredExp * _magic.GetLevel(), _magic);
        GainExp(_requiredExp * _farming.GetLevel(), _farming);
        GainExp(_requiredExp * _mining.GetLevel(), _mining);
        GainExp(_requiredExp * _woodcutting.GetLevel(), _woodcutting);
        GainExp(_requiredExp * _fishing.GetLevel(), _fishing);
        GainExp(_requiredExp * _crafting.GetLevel(), _crafting);
    }
}
