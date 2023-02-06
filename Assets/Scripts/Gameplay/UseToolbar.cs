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

    public RuleTileWithData _fernTile;
    public RuleTileWithData _redMushroomTile;
    public RuleTileWithData _pinkFlowerTile;
    public RuleTileWithData _whiteFlowerTile;
    public RuleTileWithData _candlenutTile;

    public readonly int _baseStamina = 10;
    public readonly int _baseExp = 10;

    private Shovel _shovel;
    private Pickaxe _pickaxe;
    private Axe _axe;
    private Forage _forage;

    private RuleTileWithData _ruleTileWithData;
    private RuleTile _ruleTile;
    private Item _toolbarItem;
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
        if (_resourcesTilemap.GetTile<RuleTileWithData>(position) != null)
        {
            _ruleTileWithData = _resourcesTilemap.GetTile<RuleTileWithData>(position);
            _toolbarItem = InventoryManager._instance.GetSelectedToolbarItem(false);
            _currentCell = position;
            _ruleTile = null;
        }
        else
        {
            _ruleTile = _groundTilemap.GetTile<RuleTile>(position);
            _toolbarItem = InventoryManager._instance.GetSelectedToolbarItem(false);
            _currentCell = position;
            _ruleTileWithData = null;
        }
    }

    public bool UseItemInSlot()
    {
        if (_toolbarItem != null && _toolbarItem.type == Type.BuildingBlock)
        {
            Place();
            return true;
        }
        else if (SaveData.currentStamina > 0)
        {
            if (_ruleTile != null)
            {
                if (_toolbarItem != null && _toolbarItem.subType == SubType.Shovel)
                {
                    _shovel.Dig(_currentCell, _ruleTile);
                    return true;
                }
            }
            else if (_ruleTileWithData != null)
            {
                if (_ruleTileWithData == _fernTile || _ruleTileWithData == _candlenutTile || _ruleTileWithData == _redMushroomTile ||
                    _ruleTileWithData == _pinkFlowerTile || _ruleTileWithData == _whiteFlowerTile)
                {
                    _forage.Foraging(_currentCell, _ruleTileWithData);
                    return true;
                }  
                else if (_toolbarItem != null && _toolbarItem.type == Type.Tool)
                {
                    if (_toolbarItem.subType == SubType.Axe)
                    {
                        _axe.Chop(_currentCell, _ruleTileWithData);
                        return true;
                    }
                    else if (_toolbarItem.subType == SubType.Pickaxe)
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
        Item itemToPlace = InventoryManager._instance.GetSelectedToolbarItem(true);
        _resourcesTilemap.SetTile(_currentCell, itemToPlace.tile);
    }

    public void Gather(Vector3Int position, Item item, Tilemap tilemap)
    {
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
