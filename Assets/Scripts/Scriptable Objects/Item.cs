using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase tile;  
    public int damage;    

    [Header("Only UI")]
    public string description;  
    public bool stackable = false;
    public int maxStack;
    public int value;

    [Header("Both")]
    public Sprite image;
    public int durability;

    [Header("Tags")]
    public ItemType itemType;
    public EquipmentType equipmentType;
    public ToolType toolType;
    public WeaponType weaponType;
}

public enum ItemType
{
    NA,
    BuildingBlock,  
    Resource,
    SpellComponent,
    Equipment,
    Tool,
    Weapon,
    Artefact
}

public enum EquipmentType
{
    NA,
    Ring,
    Belt,
    Necklace,
    Arrows,
    ArcaneFocus
}

public enum ToolType
{
    NA,
    Axe,
    FishingRod,
    Hammer,
    Hoe,
    Pickaxe,
    Shovel,
    WateringCan
}

public enum WeaponType
{
    NA,
    Axe,
    Bow,
    Crossbow,
    Dagger,
    Mace,
    Spear,
    Staff,
    Sword
}

