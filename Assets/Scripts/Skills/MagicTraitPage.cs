using UnityEngine;
using UnityEngine.UI;

public class MagicTraitPage : TraitPages
{
    /* Trait 1 = Swiftness
     * Trait 2 = Pack Mule
     * Trait 3 = Mana Overload
     * Trait 4 = Spell Affinity
     * Trait 5 = Willpower
     * Trait 6 = Ritual Affinity
     */

    public GameObject _componentPouch;
    public Sprite _image;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) 
        {
            int slotNum = trait.GetLevel() + 1;
            Transform slot = _componentPouch.transform.GetChild(slotNum);
            slot.GetComponent<Image>().sprite = _image;
            slot.GetComponent<Image>().raycastTarget = true;
            slot.GetComponent<Button>().interactable = true;
        }
        else if (trait == _trait3) { PlayerManager._instance._mana.AddToMaxStatAmount(100); }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }
}
