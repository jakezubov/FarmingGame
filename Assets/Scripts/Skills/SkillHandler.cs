using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    public Skill _combat, _magic, _farming, _mining, _forestry, _fishing, _crafting;

    void Start()
    {
        _combat.SetupSkills(SaveData.combatLevel, SaveData.combatSP, SaveData.combatExp);
        _magic.SetupSkills(SaveData.magicLevel, SaveData.magicSP, SaveData.magicExp);
        _farming.SetupSkills(SaveData.farmingLevel, SaveData.farmingSP, SaveData.farmingExp);
        _mining.SetupSkills(SaveData.miningLevel, SaveData.miningSP, SaveData.miningExp);
        _forestry.SetupSkills(SaveData.forestryLevel, SaveData.forestrySP, SaveData.forestryExp);
        _fishing.SetupSkills(SaveData.fishingLevel, SaveData.fishingSP, SaveData.fishingExp);
        _crafting.SetupSkills(SaveData.craftingLevel, SaveData.craftingSP, SaveData.craftingExp);       
    }

    public void GainExperience(Skills skill, int amount)
    {
        bool levelUp;
        int currentExp;
        switch (skill)
        {           
            case Skills.combat:             
                (levelUp, currentExp) = _combat.AddToCurrentExp(amount);
                SaveData.combatExp = currentExp;
                if (levelUp) { SaveData.combatLevel++; SaveData.combatSP++; }
                break;

            case Skills.magic:               
                (levelUp, currentExp) = _magic.AddToCurrentExp(amount);
                SaveData.magicExp = currentExp;
                if (levelUp) { SaveData.magicLevel++; SaveData.magicSP++; }
                break;

            case Skills.farming:              
                (levelUp, currentExp) = _farming.AddToCurrentExp(amount);
                SaveData.farmingExp = currentExp;
                if (levelUp) { SaveData.farmingLevel++; SaveData.farmingSP++; }
                break;

            case Skills.mining:              
                (levelUp, currentExp) = _mining.AddToCurrentExp(amount);
                SaveData.miningExp = currentExp;
                if (levelUp) { SaveData.miningLevel++; SaveData.miningSP++; }
                break;

            case Skills.forestry:
                (levelUp, currentExp) = _forestry.AddToCurrentExp(amount);
                SaveData.forestryExp = currentExp;
                if (levelUp) { SaveData.forestryLevel++; SaveData.forestrySP++; }
                break;

            case Skills.fishing:              
                (levelUp, currentExp) = _fishing.AddToCurrentExp(amount);
                SaveData.fishingExp = currentExp;
                if (levelUp) { SaveData.fishingLevel++; SaveData.fishingSP++; }
                break;

            case Skills.crafting:           
                (levelUp, currentExp) = _crafting.AddToCurrentExp(amount);
                SaveData.craftingExp = currentExp;
                if (levelUp) { SaveData.craftingLevel++; SaveData.craftingSP++; }
                break;
        }
    }

    public void FreeExp()
    {
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
