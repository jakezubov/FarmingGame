using UnityEngine;

public class WoodcuttingTraitPage : TraitPages
{
    /* Trait 1 = Axe Efficiency
     * Trait 2 = Leaf Clearing
     * Trait 3 = Greater Chopper
     * Trait 4 = Tree Fertiliser
     * Trait 5 = Lumberjack
     * Trait 6 = Wood Farmer
     */

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }

    /*
    public override void SetAllModifiers()
    {
        _trait1.SetModifier(3);
        _trait2.SetModifier(2);
        _trait3.SetModifier(2);
        _trait4.SetModifier(2);
        _trait5.SetModifier(2);
        _trait6.SetModifier(2);
    }

    public override void ImprovementsPerLevel(Trait trait)
    {    
        if (trait == _trait1) { _trait1.GetComponent<TooltipTrigger>().SetDescription($"" +
            $"This level {_trait1.GetLevel() * _trait1.GetModifier()}\nNext level { (1+_trait1.GetLevel()) * _trait1.GetModifier()} "); }
        else if (trait == _trait2) { _trait2.GetComponent<TooltipTrigger>().SetDescription(""); }
        else if (trait == _trait3) { _trait3.GetComponent<TooltipTrigger>().SetDescription(""); }
        else if (trait == _trait4) { _trait4.GetComponent<TooltipTrigger>().SetDescription(""); }
        else if (trait == _trait5) { _trait5.GetComponent<TooltipTrigger>().SetDescription(""); }
        else if (trait == _trait6) { _trait6.GetComponent<TooltipTrigger>().SetDescription(""); }
    } */
}
