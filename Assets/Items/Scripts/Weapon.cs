using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class Weapon : Item
{
    [Header("Gameplay")]
    public WeaponType weaponType;
    public DamageType damageType;
    public float damage;
    public int maxDurability;
    public int durability;
}

public enum WeaponType
{
    NA,
    Sword,  
    Dagger,
    Mace,
    Spear,
    Bow,
    Crossbow,
    Staff
}

public enum DamageType
{
    NA,
    Piercing,
    Slashing,
    Bludgeoning,
    Arcane,
    Fire,
    Lightning,
    Ice
}
