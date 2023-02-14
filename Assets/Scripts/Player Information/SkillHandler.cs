using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    public Skill _combat, _magic, _farming, _mining, _forestry, _fishing, _crafting;

    public void GainExperience(Skills skill, int amount)
    {
        // gain experience in the supplied skill
        switch (skill)
        {           
            case Skills.combat: _combat.AddToCurrentExp(amount); break;
            case Skills.magic: _magic.AddToCurrentExp(amount); break;
            case Skills.farming:_farming.AddToCurrentExp(amount); break;
            case Skills.mining: _mining.AddToCurrentExp(amount); break;
            case Skills.forestry: _forestry.AddToCurrentExp(amount); break;
            case Skills.fishing: _fishing.AddToCurrentExp(amount); break;
            case Skills.crafting: _crafting.AddToCurrentExp(amount); break;
        }
        SaveAllSkills();
    }

    public void LoadAllSkills()
    {
        _combat.LoadSkill(SaveData.combatLevel, SaveData.combatSP, SaveData.combatExp);
        _magic.LoadSkill(SaveData.magicLevel, SaveData.magicSP, SaveData.magicExp);
        _farming.LoadSkill(SaveData.farmingLevel, SaveData.farmingSP, SaveData.farmingExp);
        _mining.LoadSkill(SaveData.miningLevel, SaveData.miningSP, SaveData.miningExp);
        _forestry.LoadSkill(SaveData.forestryLevel, SaveData.forestrySP, SaveData.forestryExp);
        _fishing.LoadSkill(SaveData.fishingLevel, SaveData.fishingSP, SaveData.fishingExp);
        _crafting.LoadSkill(SaveData.craftingLevel, SaveData.craftingSP, SaveData.craftingExp);
    }

    public void SaveAllSkills()
    {
        (SaveData.combatLevel, SaveData.combatSP, SaveData.combatExp) = _combat.SaveSkill();
        (SaveData.magicLevel, SaveData.magicSP, SaveData.magicExp) = _magic.SaveSkill();
        (SaveData.farmingLevel, SaveData.farmingSP, SaveData.farmingExp) = _farming.SaveSkill();
        (SaveData.miningLevel, SaveData.miningSP, SaveData.miningExp) = _mining.SaveSkill();
        (SaveData.forestryLevel, SaveData.forestrySP, SaveData.forestryExp) = _forestry.SaveSkill();
        (SaveData.fishingLevel, SaveData.fishingSP, SaveData.fishingExp) = _fishing.SaveSkill();
        (SaveData.craftingLevel, SaveData.craftingSP, SaveData.craftingExp) = _crafting.SaveSkill();
    }

    public void FreeExp()
    {
        // used for debugging purposed to quickly gain exp
        int _requiredExpBase = 100;
        GainExperience(Skills.combat, _requiredExpBase * SaveData.combatLevel); 
        GainExperience(Skills.magic, _requiredExpBase * SaveData.magicLevel);
        GainExperience(Skills.farming, _requiredExpBase * SaveData.farmingLevel);
        GainExperience(Skills.mining, _requiredExpBase * SaveData.miningLevel);
        GainExperience(Skills.forestry, _requiredExpBase * SaveData.forestryLevel);
        GainExperience(Skills.fishing, _requiredExpBase * SaveData.fishingLevel);
        GainExperience(Skills.crafting, _requiredExpBase * SaveData.craftingLevel);
    }
}

public enum Skills
{
    combat,
    magic,
    farming,
    mining,
    forestry,
    fishing,
    crafting
}
