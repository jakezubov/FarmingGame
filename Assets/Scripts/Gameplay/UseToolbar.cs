using UnityEngine;
using UnityEngine.Tilemaps;

public class UseToolbar : MonoBehaviour
{
    public Tilemap _groundTilemap;   
    public Tilemap _droppedTilemap;
    public Tilemap _resourcesTilemap;
    public Tilemap _objectsNoCollideTilemap;
    public GameObject _lootPrefab;
    public GameObject _parentAfterDrop;
    public Stat _stamina;

    public readonly int _baseStamina = 10;
    public readonly int _baseExp = 10;

    private Shovel _shovel;
    private Pickaxe _pickaxe;
    private Axe _axe;
    private Forage _forage;

    private RuleTileWithData _ruleTileWithData;
    private RuleTile _ruleTile;
    private Item _toolbarItem;
    private Tool _toolbarTool;
    private Vector3Int _currentCell;

    private void Start()
    {
        _shovel = GetComponent<Shovel>();
        _pickaxe = GetComponent<Pickaxe>();
        _axe = GetComponent<Axe>();
        _forage = GetComponent<Forage>();
    }

    public void GetData(Vector3Int position)
    {
        // checks for default state
        _ruleTile = null;
        _ruleTileWithData = null;
        _toolbarItem = null;
        _toolbarTool = null;

        // assigns values based on what the highlight tile is on and what is the current toolbar item
        // GetData and UseItemInSlot are seperate so the animation has time to play before anything happens
        if (_resourcesTilemap.GetTile<RuleTileWithData>(position) != null)
        {
            _ruleTileWithData = _resourcesTilemap.GetTile<RuleTileWithData>(position);
            _currentCell = position;    

            if (InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
            {
                _toolbarTool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);
            }
            else { _toolbarItem = InventoryManager._instance.GetSelectedToolbarItem(false); }
        }
        else
        {
            _ruleTile = _groundTilemap.GetTile<RuleTile>(position);
            _currentCell = position; 

            if (InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
            {
                _toolbarTool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);
            }
            else { _toolbarItem = InventoryManager._instance.GetSelectedToolbarItem(false); }
        }
    }

    public bool UseItemInSlot()
    {
        // does an action based on what tool is active 
        if (_toolbarItem != null && _toolbarItem.type == Type.BuildingBlock)
        {
            Place();
            return true;
        }
        else if (_stamina.GetCurrentValue() > 0)
        {
            if (_ruleTile != null)
            {
                if (_toolbarTool != null && _toolbarTool.toolType == ToolType.Shovel)
                {
                    _shovel.Dig(_currentCell, _ruleTile);
                    return true;
                }
            }
            else if (_ruleTileWithData != null)
            {
                if (_ruleTileWithData.ruleTiletag == RuleTileTags.Foragable)
                {
                    _forage.Foraging(_currentCell, _ruleTileWithData);
                    return true;
                }  
                else if (_toolbarTool != null && _toolbarTool.type == Type.Tool)
                {
                    if (_toolbarTool.toolType == ToolType.Axe && _ruleTileWithData.ruleTiletag == RuleTileTags.Forestry)
                    {
                        _axe.Chop(_currentCell, _ruleTileWithData);
                        return true;
                    }
                    else if (_toolbarTool.toolType == ToolType.Pickaxe && _ruleTileWithData.ruleTiletag == RuleTileTags.Mining)
                    {
                        _pickaxe.Mine(_currentCell, _ruleTileWithData);
                        return true;
                    }    
                }                       
            }
        }
        return false;
    }

    private void Place()
    {
        // places an item directly to the tilemap
        Item itemToPlace = InventoryManager._instance.GetSelectedToolbarItem(true);
        _resourcesTilemap.SetTile(_currentCell, itemToPlace.mapTile);
    }

    public void Gather(Vector3Int position, Item item, Tilemap tilemap)
    {
        // drops an item onto the ground (seperate game object)
        tilemap.SetTile(position, null);
        if (item != null)
        {
            Vector3 pos = _droppedTilemap.GetCellCenterWorld(position);
            GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);
            loot.GetComponent<LootItem>().Initialise(item);
            loot.transform.SetParent(_parentAfterDrop.transform);
        }    
    }
}
