using UnityEngine;

public class Axe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public ForestryTraits _forestry;

    public RuleTileWithData _birchTile;
    public RuleTileWithData _chestnutTile;
    public RuleTileWithData _mapleTile;

    public RuleTile _birchRT;
    public RuleTile _chestnutRT;
    public RuleTile _mapleRT;

    private Tools _tools;

    private void Start()
    {
        _tools = GetComponent<Tools>();
    }

    public void Chop(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        // needs to be completely remade

        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _birchTile || ruleTile == _chestnutTile || ruleTile == _mapleTile)
        {
            _tools.Gather(currentCell, ruleTile.GetRandomItem(), _tools._resourcesCTilemap);
            TryGetExtraWood(currentCell, ruleTile);

            currentCell.y += 3;
            _tools.Gather(currentCell, ruleTile.GetRandomItem(), _tools._resourcesCTilemap);
            TryGetExtraWood(currentCell, ruleTile);

            _stamina.LowerStatAmount(_tools._baseStamina * 3 - SaveData.axeEfficiencyLevel * _forestry.GetEfficiencyModifier());
            _skills.GainExperience(Skills.forestry, _tools._baseExp * 3);
        }
    }

    private void TryGetExtraWood(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        // random chance to get extra wood (determined by lumberjack trait)
        if (_forestry.RollForExtras(11 - SaveData.lumberjackLevel))
        {
            _tools.Gather(currentCell, ruleTile.GetRandomItem(), _tools._resourcesCTilemap);
            _skills.GainExperience(Skills.forestry, _tools._baseExp);
        }
        else { _tools.Gather(currentCell, null, _tools._resourcesCTilemap); }
    }
}
