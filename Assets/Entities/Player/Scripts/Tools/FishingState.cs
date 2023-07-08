using UnityEngine;

public class FishingState : ToolBaseState
{
    public SkillHandler _skills;
    public RuleTileWithData _water;

    public override void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool)
    {
        RuleTile ruleTile = toolSM.GetRuleTile();

        if (ruleTile != null && tool != null && tool.toolType == ToolType.FishingRod)
        {
            toolSM._stamina.LowerStatAmount(toolSM._baseStamina - SaveData.fishingEfficiencyLevel * toolSM.GetEfficiencyModifier());

            if (ruleTile ==  _water)
            {
                _skills.GainExperience(Skills.fishing, toolSM._baseExp);
            }
        }
    }
}
