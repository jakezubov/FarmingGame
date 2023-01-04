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

    private void Start()
    {
        _stamina = PlayerManager._instance._stamina;
    }

    public void TryInteract(Vector3Int currentCell)
    {
        if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) != null)
        {
            RuleTileWithData ruleTile = _resourcesTilemap.GetTile<RuleTileWithData>(currentCell);
            Item item = InventoryManager._instance.GetSelectedToolbarItem(false);

            if (item.GetItemType() == ItemType.BuildingBlock)
            {
                Place(currentCell);
            }
            else if (_stamina.GetCurrentStatAmount() > 0 && item.GetItemType() == ItemType.Tool)
            {
                _stamina.LowerCurrentStatAmount(_baseStamina);
                                    
                if (ruleTile == _woodTile && item.GetActionType() == ActionType.Chop)
                {
                    Gather(currentCell);
                    PlayerManager._instance._woodcutting.GainExp(_baseExp);
                }
                else if (ruleTile == _logTile && item.GetActionType() == ActionType.Chop)
                {
                    Gather(currentCell);

                    // Checks for vertical logs
                    currentCell.y += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.y += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.y -= 3;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.y -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.y += 2;

                    // Checks for horizontal logs
                    currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.x -= 3;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }
                    currentCell.x -= 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Gather(currentCell); }

                    PlayerManager._instance._woodcutting.GainExp(_baseExp * 3);
                }
                else if (ruleTile == _stoneTile && item.GetActionType() == ActionType.Mine)
                {
                    Gather(currentCell);
                    PlayerManager._instance._mining.GainExp(_baseExp);
                }
                else if (ruleTile == _longRockTile && item.GetActionType() == ActionType.Mine)
                {
                    Gather(currentCell);
                    currentCell.x += 1;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _longRockTile) { Gather(currentCell); }
                    currentCell.x -= 2;
                    if (_resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _longRockTile) { Gather(currentCell); }

                    PlayerManager._instance._mining.GainExp(_baseExp * 2);
                }
                else if (ruleTile == _boulderTile && item.GetActionType() == ActionType.Mine)
                {

                }  /*     
                else if (ruleTile == _spruceTile || ruleTile == _oakTile)
                {
                    int treeHeight = 5;

                    _resourcesTilemap.SetTile(currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _objectsNoCollideTilemap.SetTile(currentCell, null); }
                    currentCell.x += 1; _objectsNoCollideTilemap.SetTile(currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { currentCell.y -= 1; _objectsNoCollideTilemap.SetTile(currentCell, null); }
                    currentCell.x -= 2; _objectsNoCollideTilemap.SetTile(currentCell, null);
                    for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _objectsNoCollideTilemap.SetTile(currentCell, null); }

                    PlayerManager._instance._woodcutting.GainExp(treeHeight * _baseExp);
                    stamina.LowerCurrentStatAmount(treeHeight);
                } */
            }
        }
    }

    private void Place(Vector3Int position)
    {
        Item itemToPlace = InventoryManager._instance.GetSelectedToolbarItem(true);
        _resourcesTilemap.SetTile(position, itemToPlace._tile);
    }

    private void Gather(Vector3Int position)
    {
        RuleTileWithData tile = _resourcesTilemap.GetTile<RuleTileWithData>(position);
        _resourcesTilemap.SetTile(position, null);

        Vector3 pos = _droppedTilemap.GetCellCenterWorld(position);
        GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);
        loot.GetComponent<LootItem>().Initialise(tile.GetItem());
        loot.transform.SetParent(_parent.transform);
    }
}
