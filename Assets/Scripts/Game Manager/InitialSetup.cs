using UnityEngine;

public class InitialSetup : MonoBehaviour
{
    public StatHandler _stats;
    public ReputationHandler _reputation;
    public SkillHandler _skills;
    public CombatTraits _combat;
    public MagicTraits _magic;
    public FarmingTraits _farming;
    public MiningTraits _mining;
    public ForestryTraits _forestry;
    public FishingTraits _fishing;
    public CraftingTraits _crafting;

    void Awake()
    { 
        if (!SaveData.ranInitalSetup)
        {
            if (SaveData.maxHealth == 0) { SaveData.maxHealth = 100; }
            if (SaveData.currentHealth == 0) { SaveData.currentHealth = SaveData.maxHealth; }
            if (SaveData.maxMana == 0) { SaveData.maxMana = 100; }
            if (SaveData.currentMana == 0) { SaveData.currentMana = SaveData.maxMana; }
            if (SaveData.maxStamina == 0) { SaveData.maxStamina = 100; }
            if (SaveData.currentStamina == 0) { SaveData.currentStamina = SaveData.maxStamina; }
            if (SaveData.moveSpeed < 5) { SaveData.moveSpeed = 5; }

            SaveData.ranInitalSetup = true;
        }

        _stats.LoadAllStats();
        _reputation.LoadAllReputation();
        _skills.LoadAllSkills();
        _combat.LoadTraitLevels();
        _magic.LoadTraitLevels();
        _farming.LoadTraitLevels();
        _mining.LoadTraitLevels();
        _forestry.LoadTraitLevels();
        _fishing.LoadTraitLevels();
        _crafting.LoadTraitLevels();
    }
}
