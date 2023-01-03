using UnityEngine;
using UnityEngine.Tilemaps;
using System.ComponentModel;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase _tile;  
    public Vector2Int _range = new Vector2Int(5, 4);
    [SerializeField] private ActionType _actionType;

    [Header("Only UI")]
    [SerializeField] private string _description;
    [SerializeField] private string _textColour;
    [SerializeField] private bool _stackable = false;
    [SerializeField] private int _maxStack = 0;  

    [Header("Both")]
    [SerializeField] private Sprite _image;
    [SerializeField] private ItemType _itemType;

    public ItemType GetItemType()
    {
        return _itemType;
    }
    public ActionType GetActionType()
    {
        return _actionType;
    }

    public bool IsStackable()
    {
        return _stackable;
    }

    public int GetMaxStackAmount()
    {
        return _maxStack;
    }

    public void SetMaxStackAmount(int amount)
    {
        _maxStack = amount;
    }

    public Sprite GetImage()
    {
        return _image;
    }

    public void SetImage(Sprite sprite)
    {
        _image = sprite;
    }

    public string GetDescription()
    {
        return _description;
    }

    public string GetTextColour()
    {
        return _textColour;
    }
}

public enum ItemType
{   
    Default,
    Tool,
    Ring,
    Belt,
    Necklace,
    Arrows,
    ArcaneFocus,
    [Description("Spell Component")] SpellComponent,
    Resource,
    BuildingBlock
}

public enum ActionType
{
    NoAction,
    Mine,
    Fish,
    Chop,
    Gather
}
