using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public MiningTraits _mining;

    public RuleTileWithData _rockTile;
    public RuleTileWithData _CopperTile;
    public RuleTileWithData _TinTile;  
    public RuleTileWithData _IronTile;
    public RuleTileWithData _CarbonTile;
    public RuleTileWithData _SilverTile;
    public RuleTileWithData _GoldTile;
    public RuleTileWithData _EtheriumTile;
    public RuleTileWithData _AdamantiteTile;

    private UseToolbar _use;
    
    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Mine(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stamina.LowerStatAmount(_use._baseStamina - _mining.GetPickaxeEfficiencyModifier());

        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _rockTile)
        {
            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);

            // random chance to get gem (determined by gemmologist trait)
            if (_mining.RollForGem())
            {
                _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
                _skills.GainExperience(Skills.mining, _use._baseExp);
            }
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
        else if (ruleTile == _CopperTile)
        {
            CheckForOres(currentCell, ruleTile, _CopperTile);
        }
        else if (ruleTile == _TinTile)
        {
            CheckForOres(currentCell, ruleTile, _TinTile);
        }
        else if (ruleTile == _IronTile)
        {
            CheckForOres(currentCell, ruleTile, _IronTile);
        }
        else if (ruleTile == _CarbonTile)
        {
            CheckForOres(currentCell, ruleTile, _CarbonTile);
        }
        else if (ruleTile == _SilverTile)
        {
            CheckForOres(currentCell, ruleTile, _SilverTile);
        }
        else if (ruleTile == _GoldTile)
        {
            CheckForOres(currentCell, ruleTile, _GoldTile);
        }
        else if (ruleTile == _EtheriumTile)
        {
            CheckForOres(currentCell, ruleTile, _EtheriumTile);
        }
        else if (ruleTile == _AdamantiteTile)
        {
            CheckForOres(currentCell, ruleTile, _AdamantiteTile);
        }
    }

    private void CheckForOres(Vector3Int currentCell, RuleTileWithData ruleTile, RuleTileWithData target)
    {
        _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
        _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);

        // random chance to get extra ore (determined by prospector trait)
        if (_mining.RollForExtraOre())
        {
            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
        _skills.GainExperience(Skills.mining, _use._baseExp);

        // random chance to get gem (determined by gemmologist trait)
        if (_mining.RollForGem())
        {
            _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
    }
}
