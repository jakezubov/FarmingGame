using UnityEngine;

public class Forage : MonoBehaviour
{
    private Use _use;
    private int _fierceForagerModifier = 0;

    private void Start()
    {
        _use = GetComponent<Use>();
    }

    public void Foraging(Vector3Int currentCell, RuleTileWithData ruleTile)
    {
        PlayerManager._instance._stamina.LowerCurrentStatAmount(_use._baseStamina * 1 / 4);

        _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
        PlayerManager._instance._farming.GainExp(_use._baseExp * 1 / 2);

        if (_fierceForagerModifier > 0)
        {
            int randChance = Random.Range(1, 20 - _fierceForagerModifier);
            if (randChance == 1)
            {
                currentCell.x += 1;
                _use.Gather(currentCell, ruleTile.GetRandomItem(), _use._resourcesTilemap);
                PlayerManager._instance._farming.GainExp(_use._baseExp * 1 / 2);
            }
        }
    }

    public void AddToFierceForagerModifier(int amount)
    {
        _fierceForagerModifier += amount;
    }
}
