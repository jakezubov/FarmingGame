using UnityEngine;
using UnityEngine.UI;

public class MagicTraits : TraitHandler
{
    public Stat _mana;
    public GameObject _componentPouch;
    public Sprite _slotBackground;

    private int _spellAffinityModifier;
    private int _willpowerModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1)
        { // Need to be able to set slots on load game
            SaveData.bagOfHoldingLevel += 1;
            int slotNum = trait.GetLevel() + 1;
            Transform slot = _componentPouch.transform.GetChild(slotNum);
            slot.GetComponent<Image>().sprite = _slotBackground;
            slot.GetComponent<Image>().raycastTarget = true;
            slot.GetComponent<Button>().interactable = true;
        }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { _mana.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { _spellAffinityModifier += 2; }
        else if (trait == _trait5) { _willpowerModifier += 1; }
        else if (trait == _trait6) { }
    }

    public int GetSpellAffinityExtraDamagePercentage()
    {
        return _spellAffinityModifier * 2;
        // need to create spells first
    }

    public int GetWillpowerDamageReduction()
    {
        return _willpowerModifier;
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.bagOfHoldingLevel);
        _trait2.SetLevel(SaveData.alchemistLevel);
        _trait3.SetLevel(SaveData.manaOverloadLevel);
        _trait4.SetLevel(SaveData.spellAffinityLevel);
        _trait5.SetLevel(SaveData.willpowerLevel);
        _trait6.SetLevel(SaveData.ritualAffinityLevel);
    }

    public override void SaveTraitLevels()
    {
        SaveData.bagOfHoldingLevel = _trait1.GetLevel();
        SaveData.alchemistLevel = _trait2.GetLevel();
        SaveData.manaOverloadLevel = _trait3.GetLevel();
        SaveData.spellAffinityLevel = _trait4.GetLevel();
        SaveData.willpowerLevel = _trait5.GetLevel();
        SaveData.ritualAffinityLevel = _trait6.GetLevel();
    }
}
