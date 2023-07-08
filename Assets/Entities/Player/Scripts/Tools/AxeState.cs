using UnityEngine;

public class AxeState : ToolBaseState
{
    public SkillHandler _skills;
    public RuleTileWithData[] _stumpTiles;

    public override void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool)
    {
        RuleTileWithData ruleTile = toolSM.GetRuleTileWithData();

        if (ruleTile != null && tool != null && tool.toolType == ToolType.Axe)
        {
            // checks what tile is being interacted with and acts accordingly
            foreach (RuleTileWithData stump in _stumpTiles)
            {
                if (ruleTile == stump)
                {
                    ruleTile.LowerHealth(Mathf.RoundToInt(tool.damage));

                    if (ruleTile.health <= 0)
                    {
                        toolSM.Gather(currentCell, ruleTile.GetRandomItem(), toolSM._resourcesCTilemap);
                        ChanceForExtraWood(toolSM, currentCell, ruleTile);

                        toolSM.Gather(currentCell, ruleTile.GetRandomItem(), toolSM._environmentNCTilemap);
                        ChanceForExtraWood(toolSM, currentCell, ruleTile);

                        toolSM._stamina.LowerStatAmount(toolSM._baseStamina * 3 - SaveData.axeEfficiencyLevel * toolSM.GetEfficiencyModifier());
                        _skills.GainExperience(Skills.forestry, toolSM._baseExp * 3);
                    }
                }
            }
        }
    }

    private void ChanceForExtraWood(ToolStateManager toolSM, Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        // random chance to get extra wood (determined by lumberjack trait)
        if (toolSM.ChanceForExtraResources(11 - SaveData.lumberjackLevel))
        {
            toolSM.Gather(currentCell, ruleTile.GetRandomItem(), toolSM._resourcesCTilemap);
            _skills.GainExperience(Skills.forestry, toolSM._baseExp);
        }
        else { toolSM.Gather(currentCell, null, toolSM._resourcesCTilemap); }
    }
}
