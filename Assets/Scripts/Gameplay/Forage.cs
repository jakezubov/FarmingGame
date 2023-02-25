using UnityEngine;

public class Forage : MonoBehaviour
{
    public Stat _stamina;
    public SkillHandler _skills;
    public ForestryTraits _forestry;

    private UseToolbar _use;

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Foraging(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stamina.LowerStatAmount(_use._baseStamina * 1 / 4);

        _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
        _skills.GainExperience(Skills.forestry, _use._baseExp * 1 / 2);

        // random chance to get extra foragable item (determined by fierce forager trait)
        if (_forestry.RollForExtras(20 - _forestry.GetFierceForagerModifier()))
        {
            currentCell.x += 1;
            _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
            _skills.GainExperience(Skills.forestry, _use._baseExp * 1 / 2);
        }
    }
}
