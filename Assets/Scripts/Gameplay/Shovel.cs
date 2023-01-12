using UnityEngine;

public class Shovel : MonoBehaviour
{
    public RuleTile _grassTile;
    public RuleTile _sandTile;  
    public Item _bait;
    public Item[] _artefacts;
    
    private Use _use;
    private int _baitFinderModifier = 0;
    private int _archaeologistModifier = 0;

    private void Start()
    {
        _use = GetComponent<Use>();
    }

    public void Dig(Vector3Int currentCell, RuleTile ruleTile)
    {
        PlayerManager._instance._stamina.LowerCurrentStatAmount(_use._baseStamina);

        if (ruleTile == _grassTile)
        {
            int randChance = Random.Range(1, 15 - _baitFinderModifier);
            if (randChance == 1)
            {
                _use.Gather(currentCell, _bait, _use._groundTilemap);
            }       
            
            randChance = Random.Range(1, 100 - _archaeologistModifier);
            if (randChance == 1)
            {
                int i = Random.Range(0, _artefacts.Length);
                _use.Gather(currentCell, _artefacts[i], _use._groundTilemap);
            }

            _use._groundTilemap.SetTile(currentCell, _sandTile);
        }
        else if (ruleTile == _sandTile)
        {
            _use._groundTilemap.SetTile(currentCell, _grassTile);
        }
    }

    public void AddToBaitFinderModifier(int amount)
    {
        _baitFinderModifier += amount;
    }

    public void AddToArchaeologistModifier(int amount)
    {
        _archaeologistModifier += amount;
    }
}
