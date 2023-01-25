using UnityEngine;
using UnityEngine.UI;

public class MagicTraits : TraitHandler
{
    public Stats _stats;
    public GameObject _componentPouch;
    public Sprite _image;

    public override void PerformTraitChange(Trait trait)
    {
        SaveData.magicSP--;
        if (trait == _trait1)
        { // Need to be able to set slots on load game
            SaveData.bagOfHoldingLevel += 1;
            int slotNum = trait.GetLevel() + 1;
            Transform slot = _componentPouch.transform.GetChild(slotNum);
            slot.GetComponent<Image>().sprite = _image;
            slot.GetComponent<Image>().raycastTarget = true;
            slot.GetComponent<Button>().interactable = true;
        }
        else if (trait == _trait2) { SaveData.alchemistLevel += 1; }
        else if (trait == _trait3) { SaveData.manaOverloadLevel += 1; _stats.AddToMaxStatAmount(Stat.mana, 10); }
        else if (trait == _trait4) { SaveData.spellAffinityLevel += 1; }
        else if (trait == _trait5) { SaveData.willpowerLevel += 1; }
        else if (trait == _trait6) { SaveData.ritualAffinityLevel += 1; }
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
}
