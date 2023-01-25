using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public Stats _stats;
    public SkillHandler _skills;
    public RuleTileWithData _rockTile;
    public RuleTileWithData _TinTile;
    public RuleTileWithData _CopperTile;
    public RuleTileWithData _IronTile;
    public RuleTileWithData _GoldTile;

    private UseToolbar _use;
    private int _prospectorModifier = 0;
    private int _pickaxeEfficiencyModifier = 0;
    

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Mine(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stats.LowerCurrentStatAmount(Stat.stamina, _use._baseStamina - _pickaxeEfficiencyModifier);

        if (ruleTile == _rockTile)
        {
            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            currentCell.y += 1;
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _rockTile) 
            { 
                _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
                
            }
            currentCell.y -= 2; 
            if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == _rockTile)
            {
                _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            }
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
        else if (ruleTile == _TinTile)
        {
            CheckForOres(currentCell, ruleTile, _TinTile);
        }
        else if (ruleTile == _CopperTile)
        {
            CheckForOres(currentCell, ruleTile, _CopperTile);
        }
        else if (ruleTile == _IronTile)
        {
            CheckForOres(currentCell, ruleTile, _IronTile);
        }
        else if (ruleTile == _GoldTile)
        {
            CheckForOres(currentCell, ruleTile, _GoldTile);
        }
    }

    private void CheckForOres(Vector3Int currentCell, RuleTileWithData ruleTile, RuleTileWithData target)
    {
        _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
        _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
        currentCell.y += 1;
        if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == target)
        {
            RollForExtraOre(currentCell, ruleTile);

        }
        currentCell.y -= 2;
        if (_use._resourcesTilemap.GetTile<RuleTileWithData>(currentCell) == target)
        {
            RollForExtraOre(currentCell, ruleTile);
        }
        _skills.GainExperience(Skills.mining, _use._baseExp * 3);
    }

    private void RollForExtraOre(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        int randChance = Random.Range(1, 15 - _prospectorModifier);
        if (randChance == 1)
        {
            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
        else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
    }

    public void SetProspectorModifier(int amount)
    {
        _prospectorModifier = amount;
    }
    public void SetPickaxeEfficiencyModifier(int amount)
    {
        _pickaxeEfficiencyModifier = amount;
    }
}
