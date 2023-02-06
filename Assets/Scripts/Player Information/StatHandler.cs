using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public Stat _health, _mana, _stamina;

    public void LoadAllStats()
    {
        _health.LoadStat(SaveData.maxHealth, SaveData.currentHealth);
        _mana.LoadStat(SaveData.maxMana, SaveData.currentMana);
        _stamina.LoadStat(SaveData.maxStamina, SaveData.currentStamina);
    }

    public void SaveAllStats()
    {
        (SaveData.maxHealth, SaveData.currentHealth) = _health.SaveStat();
        (SaveData.maxMana, SaveData.currentMana) = _mana.SaveStat();
        (SaveData.maxStamina, SaveData.currentStamina) = _stamina.SaveStat();
    }
}

