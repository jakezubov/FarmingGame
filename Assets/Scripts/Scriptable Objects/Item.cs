using UnityEngine;
using UnityEngine.Tilemaps;
using System.ComponentModel;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase tile;  
    public Vector2Int range = new Vector2Int(5, 4);
    public int damage;

    [Header("Only UI")]
    public string description;
    public string textColour;
    public bool stackable = false;
    public int maxStack = 0;  

    [Header("Both")]
    public Sprite image;

    [Header("Type")]
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
    [Description("Spell Component")] SpellComponent
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

