using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Gameplay")]
    public TileBase tile;  
    public float damage;
    public DamageType damageType;

    [Header("UI")]
    public string description;  
    public int maxStack;
    public int value;

    [Header("Both")]
    public Sprite image;
    public int maxDurability;
    public int durability;

    [Header("Tags")]
    public Type type;
    public SubType subType; 
}

public enum Type
{
    BuildingBlock,
    Resource,
    SpellComponent,
    Equipment,
    Tool,
    Weapon,
    Artefact
}

public enum SubType
{
    NA,
    Ring, // Equipment sub types
    Belt,
    Necklace,
    Arrows,
    ArcaneFocus,
    Axe, // Tool subtypes
    FishingRod,
    Hammer,
    Hoe,
    Pickaxe,
    Shovel,
    WateringCan,
    Bow, // Weapon sub types
    Crossbow,
    Dagger,
    Mace,
    Spear,
    Staff,
    Sword
}



