using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact : MonoBehaviour
{
    public Stat _stamina;
    public AllSkills _skills;
    public Tilemap _foliageTilemap;
    public Tilemap _treeTopTilemap;
    public GameObject _lootPrefab;

    public RuleTileWithData _woodTile;
    public RuleTileWithData _logTile;
    public RuleTileWithData _stoneTile;
    public RuleTileWithData _longRockTile;
    public RuleTileWithData _boulderTile;
    public RuleTileWithData _oakTile;
    public RuleTileWithData _spruceTile;   
    
    private readonly int _baseExp = 10;
    private readonly int _baseStamina = 1;

    public void TryInteract(Vector3Int currentCell)
    {
        if (_stamina.GetCurrentStatAmount() > 0)
        {
            _stamina.LowerCurrentStatAmount(_baseStamina);      

            if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) != null)
            {
                RuleTileWithData ruleTile = _foliageTilemap.GetTile<RuleTileWithData>(currentCell);
                Item item = InventoryManager._instance.GetSelectedToolbarItem(false);

                if (item._itemType == ItemType.BuildingBlock)
                {
                    Build(currentCell);
                }
                else if (item._itemType == ItemType.Tool)
                {
                    if (ruleTile == _woodTile && item._actionType == ActionType.Chop)
                    {
                        Destroy(currentCell);
                        _skills.GainExp(_baseExp, _skills._woodcutting);
                    }
                    else if (ruleTile == _logTile && item._actionType == ActionType.Chop)
                    {
                        Destroy(currentCell); 

                        currentCell.y += 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.y += 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.y -= 3;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.y -= 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.y += 2;

                        currentCell.x += 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.x += 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.x -= 3;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }
                        currentCell.x -= 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { Destroy(currentCell); }

                        _skills.GainExp(_baseExp*3, _skills._woodcutting);                       
                    }
                    else if (ruleTile == _stoneTile && item._actionType == ActionType.Mine) 
                    { 
                        Destroy(currentCell); 
                        _skills.GainExp(_baseExp, _skills._mining); 
                    }                   
                    else if (ruleTile == _longRockTile && item._actionType == ActionType.Mine)
                    {
                        Destroy(currentCell);                       
                        currentCell.x += 1;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _longRockTile) { Destroy(currentCell); }
                        currentCell.x -= 2;
                        if (_foliageTilemap.GetTile<RuleTileWithData>(currentCell) == _longRockTile) { Destroy(currentCell); }

                        _skills.GainExp(_baseExp * 2, _skills._mining);
                    }
                    else if (ruleTile == _boulderTile && item._actionType == ActionType.Mine)
                    {
                        
                    }
                }
            }            

            /*            
            if (_foliageTilemap.GetTile(currentCell) == _spruceTile || _foliageTilemap.GetTile(currentCell) == _oakTile)
            {
                int treeHeight = 5;
                _foliageTilemap.SetTile(currentCell, null);
                for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _treeTopTilemap.SetTile(currentCell, null); }
                currentCell.x += 1; _treeTopTilemap.SetTile(currentCell, null);
                for (int i = 0; i < treeHeight; i++) { currentCell.y -= 1; _treeTopTilemap.SetTile(currentCell, null); }
                currentCell.x -= 2; _treeTopTilemap.SetTile(currentCell, null);
                for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _treeTopTilemap.SetTile(currentCell, null); }

                //for (int i = 0; i < treeHeight; i++) { _inventory.GainWood(); }
                _skills.GainExp(treeHeight * _baseExp, _skills._woodcutting);
                _stamina.LowerCurrentStatAmount(treeHeight);
            } */
        }
    }

    private void Build(Vector3Int position)
    {
        Item itemToBuild = InventoryManager._instance.GetSelectedToolbarItem(true);

        _foliageTilemap.SetTile(position, itemToBuild._tile);
    }

    private void Destroy(Vector3Int position)
    {
        RuleTileWithData tile = _foliageTilemap.GetTile<RuleTileWithData>(position);
        _foliageTilemap.SetTile(position, null);

        Vector3 pos = _foliageTilemap.GetCellCenterWorld(position);
        GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);
        loot.GetComponent<LootItem>().Initialise(tile.item);
    }

    private bool CheckCondition(RuleTileWithData tile, Item currentItem)
    {
        if (currentItem._itemType == ItemType.BuildingBlock)
        {
            if (!tile)
            {
                return false;
            }
        }
        else if (currentItem._itemType == ItemType.Tool)
        {
            if (tile && tile.item._actionType == currentItem._actionType)
            {
                return true;
            }
        }

        return false;
    }
}
