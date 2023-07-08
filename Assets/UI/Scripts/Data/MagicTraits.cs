using UnityEngine;
using UnityEngine.UI;

public class MagicTraits : TraitHandler
{
    public Stat _mana;
    public GameObject _componentPouch;
    public Sprite _slotBackground;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1)
        { // Need to be able to set slots on load game
            SaveData.bagOfHoldingLevel++;
            int slotNum = trait.GetLevel() + 1;
            Transform slot = _componentPouch.transform.GetChild(slotNum);
            slot.GetComponent<Image>().sprite = _slotBackground;
            slot.GetComponent<Image>().raycastTarget = true;
            slot.GetComponent<Button>().interactable = true;
        }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { SaveData.manaOverloadLevel++; _mana.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { SaveData.spellAffinityLevel++; }
        else if (trait == _trait5) { SaveData.willpowerLevel++; }
        else if (trait == _trait6) { }
    }
}
