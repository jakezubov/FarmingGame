using UnityEngine;

public class Forage : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public ForestryTraits _forestry;

    private Tools _tools;

    private void Start()
    {
        _tools = GetComponent<Tools>();
    }

    public void Foraging(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stamina.LowerStatAmount(_tools._baseStamina * 1 / 4);

        _tools.Gather(currentCell, ruleTile.GetRandomItem(), _tools._resourcesCTilemap);
        _skills.GainExperience(Skills.forestry, _tools._baseExp * 1 / 2);

        // random chance to get extra foragable item (determined by fierce forager trait)
        if (_forestry.RollForExtras(20 - SaveData.fierceForagerLevel))
        {
            currentCell.x += 1;
            _tools.Gather(currentCell, ruleTile.GetRandomItem(), _tools._resourcesCTilemap);
            _skills.GainExperience(Skills.forestry, _tools._baseExp * 1 / 2);
        }
    }
}
