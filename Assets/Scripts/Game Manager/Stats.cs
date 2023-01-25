using UnityEngine;

public class Stats : MonoBehaviour
{
    public ChangeSlider _healthSlider, _manaSlider, _staminaSlider;
    public ChangeText _healthTextCS, _manaTextCS, _staminaTextCS; // Character sheet text

    private void Start()
    {
        LoadStats();
    }

    public void LowerCurrentStatAmount(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.health:
                SaveData.currentHealth -= value;
                _healthSlider.SetValue(Mathf.RoundToInt(SaveData.currentHealth));
                break;

            case Stat.mana:
                SaveData.currentMana -= value;
                _manaSlider.SetValue(Mathf.RoundToInt(SaveData.currentMana));
                break;

            case Stat.stamina:
                SaveData.currentStamina -= value;
                _staminaSlider.SetValue(Mathf.RoundToInt(SaveData.currentStamina));
                break;
        }
        UpdateCStext(stat);
    }

    public void ReplenishCurrentStatAmount(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.health:
                SaveData.currentHealth += value;
                if (SaveData.currentHealth > SaveData.maxHealth) { SaveData.currentHealth = SaveData.maxHealth; }
                _healthSlider.SetValue(Mathf.RoundToInt(SaveData.currentHealth));
                break;

            case Stat.mana:
                SaveData.currentMana += value;
                if (SaveData.currentMana > SaveData.maxMana) { SaveData.currentMana = SaveData.maxMana; }
                _manaSlider.SetValue(Mathf.RoundToInt(SaveData.currentMana));
                break;

            case Stat.stamina:
                SaveData.currentStamina += value;
                if (SaveData.currentStamina > SaveData.maxStamina) { SaveData.currentStamina = SaveData.maxStamina; }
                _staminaSlider.SetValue(Mathf.RoundToInt(SaveData.currentStamina));
                break;
        }
        UpdateCStext(stat);
    }

    public void AddToMaxStatAmount(Stat stat, float value)
    {
        switch (stat)
        {
            case Stat.health:
                SaveData.maxHealth += value;
                _healthSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxHealth));
                _healthSlider.SetValue(Mathf.RoundToInt(SaveData.currentHealth));
                break;

            case Stat.mana:
                SaveData.maxMana += value;
                _manaSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxMana));
                _manaSlider.SetValue(Mathf.RoundToInt(SaveData.currentMana));
                break;

            case Stat.stamina:
                SaveData.maxStamina += value;
                _staminaSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxStamina));
                _staminaSlider.SetValue(Mathf.RoundToInt(SaveData.currentStamina));
                 break;
        }
        UpdateCStext(stat);
    }

    public void UpdateCStext(Stat stat)
    {
        switch (stat)
        {
            case Stat.health: _healthTextCS.SetText($"Health: {Mathf.RoundToInt(SaveData.currentHealth)} / {Mathf.RoundToInt(SaveData.maxHealth)}"); break;
            case Stat.mana: _manaTextCS.SetText($"Mana: {Mathf.RoundToInt(SaveData.currentMana)} / {Mathf.RoundToInt(SaveData.maxMana)}"); break;
            case Stat.stamina: _staminaTextCS.SetText($"Stamina: {Mathf.RoundToInt(SaveData.currentStamina)} / {Mathf.RoundToInt(SaveData.maxStamina)}"); break;
        }        
    }

    public void LoadStats()
    {
        _healthSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxHealth));
        _healthSlider.SetValue(Mathf.RoundToInt(SaveData.currentHealth));
        UpdateCStext(Stat.health);

        _manaSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxMana));
        _manaSlider.SetValue(Mathf.RoundToInt(SaveData.currentMana));
        UpdateCStext(Stat.mana);

        _staminaSlider.SetMaxValue(Mathf.RoundToInt(SaveData.maxStamina));
        _staminaSlider.SetValue(Mathf.RoundToInt(SaveData.currentStamina));
        UpdateCStext(Stat.stamina);
    }
}

public enum Stat
{
    health,
    mana,
    stamina
}
