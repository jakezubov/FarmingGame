using UnityEngine;

public class PickaxeState : ToolBaseState
{
    public SkillHandler _skills;
    public MiningTraits _mining;

    public RuleTileWithData _rockTile;
    public RuleTileWithData[] _OreTiles;

    public override void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool)
    {
        RuleTileWithData ruleTile = toolSM.GetRuleTileWithData();

        if (ruleTile != null && tool != null && tool.toolType == ToolType.Pickaxe)
        {
            toolSM._stamina.LowerStatAmount(toolSM._baseStamina - SaveData.pickaxeEfficiencyLevel * toolSM.GetEfficiencyModifier());

            // checks what tile is being interacted with and acts accordingly
            if (ruleTile == _rockTile)
            {
                ruleTile.LowerHealth(Mathf.RoundToInt(tool.damage));

                if (ruleTile.health <= 0)
                {
                    toolSM.Gather(currentCell, ruleTile.GetMainItem(), toolSM._resourcesCTilemap);

                    // random chance to get gem (determined by gemmologist trait)
                    if (toolSM.ChanceForExtraResources(30 - SaveData.gemologistLevel))
                    {
                        toolSM.Gather(currentCell, ruleTile.GetSecondaryItem(), toolSM._resourcesCTilemap);
                        _skills.GainExperience(Skills.mining, toolSM._baseExp);
                    }
                    _skills.GainExperience(Skills.mining, toolSM._baseExp);
                }
            }
            else
            {
                foreach (RuleTileWithData ore in _OreTiles)
                {
                    if (ruleTile == ore)
                    {
                        ruleTile.LowerHealth(Mathf.RoundToInt(tool.damage));

                        if (ruleTile.health <= 0)
                        {
                            // gather one ore and rock
                            toolSM.Gather(currentCell, ruleTile.GetMainItem(), toolSM._resourcesCTilemap);
                            toolSM.Gather(currentCell, ruleTile.GetSecondaryItem(), toolSM._resourcesCTilemap);
                            _skills.GainExperience(Skills.mining, toolSM._baseExp);

                            // random chance to get extra ore (determined by prospector trait)
                            if (toolSM.ChanceForExtraResources(15 - SaveData.prospectorLevel))
                            {
                                toolSM.Gather(currentCell, ruleTile.GetMainItem(), toolSM._resourcesCTilemap);
                                _skills.GainExperience(Skills.mining, toolSM._baseExp);
                            }

                            // random chance to get gem (determined by gemmologist trait)
                            if (toolSM.ChanceForExtraResources(30 - SaveData.gemologistLevel))
                            {
                                toolSM.Gather(currentCell, ruleTile.GetSecondaryItem(), toolSM._resourcesCTilemap);
                                _skills.GainExperience(Skills.mining, toolSM._baseExp);
                            }

                            // random chance to get artefact (determined by archaeologist trait)
                            if (toolSM.ChanceForExtraResources(100 - SaveData.archaeologistLevel * 2))
                            {
                                // if successful chooses random artefact
                                int i = Random.Range(0, _mining.GetArtefactList().Length);
                                toolSM.Gather(currentCell, _mining.GetArtefactList()[i], toolSM._resourcesCTilemap);
                            }
                        }
                    }
                }
            }
        }
    }
}
