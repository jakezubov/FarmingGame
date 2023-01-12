using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public RuleTileWithData _rockTile;
    public RuleTileWithData _nodeTile;

    private Use _use;
    private int _oreMinerModifier = 0;
    private int _pickaxeEfficiencyModifier = 0;
    

    private void Start()
    {
        _use = GetComponent<Use>();
    }

    public void Mine(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        PlayerManager._instance._stamina.LowerCurrentStatAmount(_use._baseStamina - _pickaxeEfficiencyModifier);

        if (ruleTile == _rockTile)
        {
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            currentCell.y += 1;
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _rockTile) 
            { 
                _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
                
            }
            currentCell.y -= 2; 
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _rockTile)
            {
                _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            }
            PlayerManager._instance._mining.GainExp(_use._baseExp);
        }
        else if (ruleTile == _nodeTile)
        {
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            currentCell.y += 1;
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _nodeTile) 
            {
                RollForExtraOre(currentCell, ruleTile);
                
            }
            currentCell.y -= 2;
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _nodeTile)
            {
                RollForExtraOre(currentCell, ruleTile);
            }
            PlayerManager._instance._mining.GainExp(_use._baseExp * 3);
        }
    }

    public void RollForExtraOre(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        int randChance = Random.Range(1, 15 - _oreMinerModifier);
        if (randChance == 1)
        {
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            PlayerManager._instance._mining.GainExp(_use._baseExp);
        }
        else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
    }

    public void AddToOreMinerModifier(int amount)
    {
        _oreMinerModifier += amount;
    }
    public void AddToPickaxeEfficiencyModifier(int amount)
    {
        _pickaxeEfficiencyModifier += amount;
    }
}
