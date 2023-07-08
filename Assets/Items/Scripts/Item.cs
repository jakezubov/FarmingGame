using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Item")]
public class Item : ScriptableObject
{
    [Header("General")]
    public string description;
    public Type type;
    public TileBase mapTile;
    public Sprite inventoryImage;
    public int maxStack;
    public int value;
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



