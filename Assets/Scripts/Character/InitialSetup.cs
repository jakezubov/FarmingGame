using UnityEngine;

public class InitialSetup : MonoBehaviour
{
    void Start()
    { 
        if (SaveData.maxHealth == 0)
        {
            SaveData.maxHealth = 100;           
        }
        if (SaveData.currentHealth == 0)
        {
            SaveData.currentHealth = SaveData.maxHealth;
        }
        if (SaveData.maxMana == 0)
        {
            SaveData.maxMana = 100;
        }
        if (SaveData.currentMana == 0)
        {
            SaveData.currentMana = SaveData.maxMana;
        }
        if (SaveData.maxStamina == 0)
        {
            SaveData.maxStamina = 100;           
        }
        if (SaveData.currentStamina == 0)
        {
            SaveData.currentStamina = SaveData.maxStamina;
        }
        if (SaveData.moveSpeed == 0)
        {
            SaveData.moveSpeed = 5;
        }
        if (SaveData.combatLevel == 0)
        {
            SaveData.combatLevel = 1;
        }
        if (SaveData.magicLevel == 0)
        {
            SaveData.magicLevel = 1;
        }
        if (SaveData.farmingLevel == 0)
        {
            SaveData.farmingLevel = 1;
        }
        if (SaveData.miningLevel == 0)
        {
            SaveData.miningLevel = 1;
        }
        if (SaveData.forestryLevel == 0)
        {
            SaveData.forestryLevel = 1;
        }
        if (SaveData.fishingLevel == 0)
        {
            SaveData.fishingLevel = 1;
        }
        if (SaveData.craftingLevel == 0)
        {
            SaveData.craftingLevel = 1;
        }
    }
}
