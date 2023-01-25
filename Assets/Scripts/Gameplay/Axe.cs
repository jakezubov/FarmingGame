using UnityEngine;

public class Axe : MonoBehaviour
{
    public Stats _stats;
    public SkillHandler _skills;

    public RuleTileWithData _logTile;
    public RuleTileWithData _oakTile;
    public RuleTileWithData _spruceTile;

    private UseToolbar _use;
    private int _lumberjackModifier = 0;
    private int _axeEfficiencyModifier = 0;

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
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
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.x += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.x -= 3;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.x -= 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
            }
            else // vertical logs
            {
                currentCell.y += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.y += 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.y -= 3;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
                currentCell.y -= 1;
                if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _logTile) { _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap); }
            }

            _stats.LowerCurrentStatAmount(Stat.stamina, _use._baseStamina * 3 - _axeEfficiencyModifier);
            _skills.GainExperience(Skills.forestry, _use._baseExp * 3);
        }
        else if (ruleTile == _spruceTile || ruleTile == _oakTile)
        {
            int treeHeight = 4;

            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);
            
            currentCell.x += 1; _use.Gather(currentCell, ruleTile.GetMainItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);

            currentCell.x -= 2; _use.Gather(currentCell, ruleTile.GetMainItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            RollForExtraWood(currentCell, ruleTile);

            _stats.LowerCurrentStatAmount(Stat.stamina, _use._baseStamina * 3 - _axeEfficiencyModifier);
            _skills.GainExperience(Skills.forestry, _use._baseExp * 3);
        }
    }

    public void RollForExtraWood(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        if (_lumberjackModifier > 0)
        {
            Debug.Log("test");
            int randChance = Random.Range(1, 11 - _lumberjackModifier);
            if (randChance == 1)
            {
                currentCell.y += 1;
                _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
                _skills.GainExperience(Skills.forestry, _use._baseExp);
            }
            else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
        } 
    }

    public void SetLumberjackModifier(int amount)
    {
        _lumberjackModifier = amount;
    }

    public void SetAxeEfficiencyModifier(int amount)
    {
        _axeEfficiencyModifier = amount;
    }
}
