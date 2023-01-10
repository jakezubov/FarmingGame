using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact : MonoBehaviour
{   
    public Tilemap _resourcesTilemap;
    public Tilemap _objectsNoCollideTilemap;
    public Tilemap _droppedTilemap;
    public GameObject _lootPrefab;
    public GameObject _parent;

    public RuleTileWithData _woodTile;
    public RuleTileWithData _logTile;
    public RuleTileWithData _stoneTile;
    public RuleTileWithData _longRockTile;
    public RuleTileWithData _boulderTile;
    public RuleTileWithData _oakTile;
    public RuleTileWithData _spruceTile;   
    
    private Stats _stamina;
    private readonly int _baseExp = 10;
    private readonly int _baseStamina = 1;

    private RuleTileWithData _ruleTile;
    private Item _item;
    private Vector3Int _currentCell;

    private void Start()
    {
        _stamina = PlayerManager._instance._stamina;
    }

    public void GetData(Vector3Int position)
    {        
        if (_resourcesTilemap.GetTile<RuleTileWithData>(position) != null)
        {            
            _ruleTile = _resourcesTilemap.GetTile<RuleTileWithData>(position);
            _item = InventoryManager._instance.GetSelectedToolbarItem(false);
            _currentCell = position;
        }
        else
        {
            _ruleTile = null;
            _item = null;
        }
    }

    public void InteractNow()
    {
        if (_ruleTile != null && _item != null)
        {
            if (_item.itemType == ItemType.BuildingBlock)
            {
                Place();
            }
            else if (_stamina.GetCurrentStatAmount() > 0)
            {
                _stamina.LowerCurrentStatAmount(_baseStamina);

                if (_ruleTile == _woodTile && _item.toolType == ToolType.Axe)
                {
                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);
                    PlayerManager._instance._woodcutting.GainExp(_baseExp);
                }
                else if (_ruleTile == _logTile && _item.toolType == ToolType.Axe)
                {
                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);

                    // horizontal logs
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(new Vector3Int(_currentCell.x + 1, _currentCell.y, _currentCell.z)) == _logTile ||
                        _resourcesTilemap.GetTile<RuleTileWithData>(new Vector3Int(_currentCell.x - 1, _currentCell.y, _currentCell.z)) == _logTile)
                    {
                        _currentCell.x += 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.x += 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.x -= 3;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.x -= 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    }
                    else // vertical logs
                    {
                        _currentCell.y += 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.y += 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.y -= 3;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                        _currentCell.y -= 1;
                        if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _logTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    }

                    PlayerManager._instance._woodcutting.GainExp(_baseExp * 3);
                }
                else if (_ruleTile == _spruceTile || _ruleTile == _oakTile && _item.toolType == ToolType.Axe)
                {
                    int treeHeight = 4;

                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { _currentCell.y += 1; Gather(); _objectsNoCollideTilemap.SetTile(_currentCell, null); }

                    _currentCell.x += 1; _objectsNoCollideTilemap.SetTile(_currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { _currentCell.y -= 1; _objectsNoCollideTilemap.SetTile(_currentCell, null); }

                    _currentCell.x -= 2; _objectsNoCollideTilemap.SetTile(_currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { _currentCell.y += 1; _objectsNoCollideTilemap.SetTile(_currentCell, null); }

                    PlayerManager._instance._woodcutting.GainExp(treeHeight * _baseExp);
                    _stamina.LowerCurrentStatAmount(treeHeight);
                }
                else if (_ruleTile == _stoneTile && _item.toolType == ToolType.Pickaxe)
                {
                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);
                    PlayerManager._instance._mining.GainExp(_baseExp);
                }
                else if (_ruleTile == _longRockTile && _item.toolType == ToolType.Pickaxe)
                {
                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);
                    _currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _longRockTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.x -= 2;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _longRockTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }

                    PlayerManager._instance._mining.GainExp(_baseExp * 2);
                }
                else if (_ruleTile == _boulderTile && _item.toolType == ToolType.Pickaxe)
                {
                    Gather(); _resourcesTilemap.SetTile(_currentCell, null);

                    _currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.y += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.x -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.x -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.y -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.y -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }
                    _currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(_currentCell) == _boulderTile) { Gather(); _resourcesTilemap.SetTile(_currentCell, null); }

                    PlayerManager._instance._mining.GainExp(_baseExp * 4);
                }             
            }
        }       
    }

    private void Place()
    {
        Item itemToPlace = InventoryManager._instance.GetSelectedToolbarItem(true);
        _resourcesTilemap.SetTile(_currentCell, itemToPlace.tile);
    }

    private void Gather()
    {
        Vector3 pos = _droppedTilemap.GetCellCenterWorld(_currentCell);
        GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);

        loot.GetComponent<LootItem>().Initialise(_ruleTile.GetItem());
        loot.transform.SetParent(_parent.transform);
    }
}
