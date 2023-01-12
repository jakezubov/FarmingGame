using UnityEngine;

public class Axe : MonoBehaviour
{
    public RuleTileWithData _logTile;
    public RuleTileWithData _oakTile;
    public RuleTileWithData _spruceTile;

    private Use _use;
    private int _woodFarmerModifier = 0;
    private int _axeEfficiencyModifier = 0;

    private void Start()
    {
        _use = GetComponent<Use>();
    }

    public void Chop(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        if (ruleTile == _logTile)
        {
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);

            // horizontal logs
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(new Vector3Int(currentCell.x + 1, currentCell.y, currentCell.z)) == _logTile ||
                _use._resourcesTilemap.GetTile<RuleTileWithData>(new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z)) == _logTile)
            {
                currentCell.x += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.x += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.x -= 3;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.x -= 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
            }
            else // vertical logs
            {
                currentCell.y += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.y += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.y -= 3;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
                currentCell.y -= 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap); }
            }

            PlayerManager._instance._stamina.LowerCurrentStatAmount(_use._baseStamina * 3 - _axeEfficiencyModifier);
            PlayerManager._instance._woodcutting.GainExp(_use._baseExp * 3);
        }
        else if (ruleTile == _spruceTile || ruleTile == _oakTile)
        {
            int treeHeight = 4;

            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);
            
            currentCell.x += 1; _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);

            currentCell.x -= 2; _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);

            PlayerManager._instance._stamina.LowerCurrentStatAmount(_use._baseStamina * 3 - _axeEfficiencyModifier);
            PlayerManager._instance._woodcutting.GainExp(_use._baseExp * 3);
        }
    }

    public void RollForExtraWood(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        if (_woodFarmerModifier > 0)
        {
            Debug.Log("test");
            int randChance = Random.Range(1, 11 - _woodFarmerModifier);
            if (randChance == 1)
            {
                currentCell.y += 1;
                _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
                PlayerManager._instance._mining.GainExp(_use._baseExp);
            }
            else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
        } 
    }

    public void AddToWoodFarmerModifier(int amount)
    {
        _woodFarmerModifier += amount;
    }

    public void AddToAxeEfficiencyModifier(int amount)
    {
        _axeEfficiencyModifier += amount;
    }
}
