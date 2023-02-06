using UnityEngine;

public class Axe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public ForestryTraits _forestry;

    public RuleTileWithData _logTile;
    public RuleTileWithData _oakTile;
    public RuleTileWithData _spruceTile;

    private UseToolbar _use;

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

            _stamina.LowerStatAmount(_use._baseStamina * 3 - _forestry.GetAxeEfficiencyModifier());
            _skills.GainExperience(Skills.forestry, _use._baseExp * 3);
        }
        else if (ruleTile == _spruceTile || ruleTile == _oakTile)
        {
            int treeHeight = 4;

            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);
            
            currentCell.x += 1; _use.Gather(currentCell, ruleTile.GetMainItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);

            currentCell.x -= 2; _use.Gather(currentCell, ruleTile.GetMainItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);

            _stamina.LowerStatAmount(_use._baseStamina * 3 - _forestry.GetAxeEfficiencyModifier());
            _skills.GainExperience(Skills.forestry, _use._baseExp * 3);
        }
    }

    private void TryGetExtraWood(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        bool result = _forestry.RollForExtraWood();
        if (result)
        {
            currentCell.y += 1;
            _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.forestry, _use._baseExp);
        }
        else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
        
    }
}
