using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public MiningTraits _mining;

    public RuleTileWithData _rockTile;
    public RuleTileWithData[] _OreTiles;

    private Tools _tools;
    
    private void Start()
    {
        _tools = GetComponent<Tools>();
    }

    public void Mine(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stamina.LowerStatAmount(_tools._baseStamina - SaveData.pickaxeEfficiencyLevel * _mining.GetEfficiencyModifier());

        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _rockTile)
        {
            _tools.Gather(currentCell, ruleTile.GetMainItem(), _tools._resourcesCTilemap);

            // random chance to get gem (determined by gemmologist trait)
            if (_mining.RollForExtras(30 - SaveData.gemologistLevel))
            {
                _tools.Gather(currentCell, ruleTile.GetSecondaryItem(), _tools._resourcesCTilemap);
                _skills.GainExperience(Skills.mining, _tools._baseExp);
            }
            _skills.GainExperience(Skills.mining, _tools._baseExp);
        }
        else 
        {
            foreach (RuleTileWithData ore in _OreTiles)
            {
                if (ruleTile == ore)
                {
                    // gather one ore and rock
                    _tools.Gather(currentCell, ruleTile.GetMainItem(), _tools._resourcesCTilemap);
                    _tools.Gather(currentCell, ruleTile.GetSecondaryItem(), _tools._resourcesCTilemap);
                    _skills.GainExperience(Skills.mining, _tools._baseExp);

                    // random chance to get extra ore (determined by prospector trait)
                    if (_mining.RollForExtras(15 - SaveData.prospectorLevel))
                    {
                        _tools.Gather(currentCell, ruleTile.GetMainItem(), _tools._resourcesCTilemap);
                        _skills.GainExperience(Skills.mining, _tools._baseExp);
                    }

                    // random chance to get gem (determined by gemmologist trait)
                    if (_mining.RollForExtras(30 - SaveData.gemologistLevel))
                    {
                        _tools.Gather(currentCell, ruleTile.GetSecondaryItem(), _tools._resourcesCTilemap);
                        _skills.GainExperience(Skills.mining, _tools._baseExp);
                    }

                    // random chance to get artefact (determined by archaeologist trait)
                    if (_mining.RollForExtras(100 - SaveData.archaeologistLevel * 2))
                    {
                        // if successful chooses random artefact
                        int i = Random.Range(0, _mining.GetArtefactList().Length);
                        _tools.Gather(currentCell, _mining.GetArtefactList()[i], _tools._resourcesCTilemap);
                    }
                }
            }
        }
    }
}
