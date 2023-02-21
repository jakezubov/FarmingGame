using UnityEngine;

public class Axe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public ForestryTraits _forestry;

    public RuleTileWithData _birchTile;
    public RuleTileWithData _chestnutTile;
    public RuleTileWithData _mapleTile;

    private UseToolbar _use;

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Chop(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _birchTile || ruleTile == _chestnutTile || ruleTile == _mapleTile)
        {
            int treeHeight = 4;

            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);
            
            currentCell.x += 1; _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);

            currentCell.x -= 2; _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._objectsNoCollideTilemap);
            for (int i = 0; i < treeHeight; i++) { currentCell.y += 1; _use.Gather(currentCell, null, _use._objectsNoCollideTilemap); }
            currentCell.y -= treeHeight;
            TryGetExtraWood(currentCell, ruleTile);

            _stamina.LowerStatAmount(_use._baseStamina * 3 - _forestry.GetAxeEfficiencyModifier());
            _skills.GainExperience(Skills.forestry, _use._baseExp * 3);
        }
    }

    private void TryGetExtraWood(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        // random chance to get extra wood (determined by lumberjack trait)
        bool result = _forestry.RollForExtraWood();
        if (result)
        {
            currentCell.y += 1;
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.forestry, _use._baseExp);
        }
        else { _use.Gather(currentCell, null, _use._resourcesTilemap); }
    }
}
