using UnityEngine;

public class Forage : MonoBehaviour
{
    public Stats _stats;
    public SkillHandler _skills;

    private UseToolbar _use;
    private int _fierceForagerModifier = 0;

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Foraging(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        _stats.LowerCurrentStatAmount(Stat.stamina, _use._baseStamina * 1 / 4);

        _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
        _skills.GainExperience(Skills.forestry, _use._baseExp * 1 / 2);

        if (_fierceForagerModifier > 0)
        {
            int randChance = Random.Range(1, 20 - _fierceForagerModifier);
            if (randChance == 1)
            {
                currentCell.x += 1;
                _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
                _skills.GainExperience(Skills.forestry, _use._baseExp * 1 / 2);
            }
        }
    }

    public void SetFierceForagerModifier(int amount)
    {
        _fierceForagerModifier = amount;
    }
}
