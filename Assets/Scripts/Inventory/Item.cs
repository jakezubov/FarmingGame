using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase _tile;    
    public ActionType _actionType;
    public Vector2Int _range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public string _description;
    public bool _stackable = false;
    [SerializeField]private int _maxStack = 0;

    [Header("Both")]
    public Sprite _image;
    public ItemType _itemType;

    public int GetMaxStack()
    {
        return _maxStack;
    }

    public void SetMaxStack(int amount)
    {
        _maxStack = amount;
    }
}

public enum ItemType
{   
    Tool,
    Ring,
    Belt,
    Necklace,
    Arrows,
    ArcaneFocus,
    SpellComponent,
    Resource,
    BuildingBlock
}

public enum ActionType
{
    Dig,
    Mine,
    Fish,
    Chop,
    Equipment,
    Crafting
}
