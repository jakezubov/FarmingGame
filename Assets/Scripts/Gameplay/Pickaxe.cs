using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public MiningTraits _mining;

    public RuleTileWithData _rockTile;
    public RuleTileWithData[] _OreTiles;

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
            if (_mining.RollForExtras(30 - _mining.GetGemologistModifier()))
            {
                _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
                _skills.GainExperience(Skills.mining, _use._baseExp);
            }
            _skills.GainExperience(Skills.mining, _use._baseExp);
        }
        else 
        {
            foreach (RuleTileWithData ore in _OreTiles)
            {
                if (ruleTile == ore)
                {
                    // gather one ore and rock
                    _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
                    _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
                    _skills.GainExperience(Skills.mining, _use._baseExp);

                    // random chance to get extra ore (determined by prospector trait)
                    if (_mining.RollForExtras(15 - _mining.GetProspectorModifier()))
                    {
                        _use.Gather(currentCell, ruleTile.GetMainItem(), _use._resourcesTilemap);
                        _skills.GainExperience(Skills.mining, _use._baseExp);
                    }

                    // random chance to get gem (determined by gemmologist trait)
                    if (_mining.RollForExtras(30 - _mining.GetGemologistModifier()))
                    {
                        _use.Gather(currentCell, ruleTile.GetSecondaryItem(), _use._resourcesTilemap);
                        _skills.GainExperience(Skills.mining, _use._baseExp);
                    }

                    // random chance to get artefact (determined by archaeologist trait)
                    if (_mining.RollForExtras(100 - _mining.GetArchaeologistModifier()))
                    {
                        // if successful chooses random artefact
                        int i = Random.Range(0, _mining.GetArtefactList().Length);
                        _use.Gather(currentCell, _mining.GetArtefactList()[i], _use._groundTilemap);
                    }
                }
            }
        }
    }
}
