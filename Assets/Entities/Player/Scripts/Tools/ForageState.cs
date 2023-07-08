using UnityEngine;

public class ForageState : ToolBaseState
{
    public SkillHandler _skills;

    public override void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool)
    {
        RuleTileWithData ruleTile = toolSM.GetRuleTileWithData();

        if (ruleTile != null)
        {
            toolSM._stamina.LowerStatAmount(toolSM._baseStamina * 1 / 4);

            toolSM.Gather(currentCell, ruleTile.GetRandomItem(), toolSM._resourcesCTilemap);
            _skills.GainExperience(Skills.forestry, toolSM._baseExp * 1 / 2);

            // random chance to get extra foragable item (determined by fierce forager trait)
            if (toolSM.ChanceForExtraResources(20 - SaveData.fierceForagerLevel))
            {
                currentCell.x += 1;
                toolSM.Gather(currentCell, ruleTile.GetRandomItem(), toolSM._resourcesCTilemap);
                _skills.GainExperience(Skills.forestry, toolSM._baseExp * 1 / 2);
            }
        }     
    }
}
