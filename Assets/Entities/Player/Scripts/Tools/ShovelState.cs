using UnityEngine;

public class ShovelState : ToolBaseState
{
    public MiningTraits _mining;
    public RuleTile _grassTile;
    public RuleTile _dirtTile;  
    public Item _bait;

    public override void UseTool(ToolStateManager toolSM, Vector3Int currentCell, Tool tool)
    {
        RuleTile ruleTile = toolSM.GetRuleTile();

        if (ruleTile != null && tool != null && tool.toolType == ToolType.Shovel)
        {
            toolSM._stamina.LowerStatAmount(toolSM._baseStamina);

            // checks what tile is being interacted with and acts accordingly
            if (ruleTile == _grassTile)
            {
                // random chance to get bait (determined by bait finder trait)
                if (toolSM.ChanceForExtraResources(20 - SaveData.baitFinderLevel))
                {
                    toolSM.Gather(currentCell, _bait, toolSM._groundNCTilemap);
                }

                // random chance to get artefact (determined by archaeologist trait)
                if (toolSM.ChanceForExtraResources(100 - SaveData.archaeologistLevel * 2))
                {
                    // if successful chooses random artefact
                    int i = Random.Range(0, _mining.GetArtefactList().Length);
                    toolSM.Gather(currentCell, _mining.GetArtefactList()[i], toolSM._groundNCTilemap);
                }

                toolSM._groundNCTilemap.SetTile(currentCell, _dirtTile);
            }
            else if (ruleTile == _dirtTile)
            {
                toolSM._groundNCTilemap.SetTile(currentCell, _grassTile);
            }
        }
    }
}
