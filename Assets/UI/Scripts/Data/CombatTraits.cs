public class CombatTraits : TraitHandler
{
    public PlayerController _player;
    public Stat _health;
    public Stat _stamina;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { SaveData.moveSpeed += 0.1f; }
        else if (trait == _trait2) { SaveData.heartyLevel++; _health.AddToMaxStatAmount(10); }
        else if (trait == _trait3) { SaveData.enduranceLevel++; _stamina.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { SaveData.meleeAffinityLevel++; }
        else if (trait == _trait5) { SaveData.sturdyLevel++; }
        else if (trait == _trait6) { SaveData.rangedAffinityLevel++; }
    }
}
