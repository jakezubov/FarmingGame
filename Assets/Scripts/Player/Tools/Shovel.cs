using UnityEngine;

public class Shovel : MonoBehaviour
{
    public Stat _stamina;
    public MiningTraits _mining;
    public FishingTraits _fishing;

    public RuleTile _grassTile;
    public RuleTile _dirtTile;  
    public Item _bait;
    
    
    private Tools _tools;

    private void Start()
    {
        _tools = GetComponent<Tools>();
    }

    public void Dig(Vector3Int currentCell, RuleTile ruleTile)
    {
        _stamina.LowerStatAmount(_tools._baseStamina);

        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _grassTile)
        {
            // random chance to get bait (determined by bait finder trait)
            if (_fishing.RollForExtras(20 - SaveData.baitFinderLevel))
            {
                _tools.Gather(currentCell, _bait, _tools._groundNCTilemap);
            }       
            
            // random chance to get artefact (determined by archaeologist trait)
            if (_mining.RollForExtras(100 - SaveData.archaeologistLevel * 2))
            {
                // if successful chooses random artefact
                int i = Random.Range(0, _mining.GetArtefactList().Length);
                _tools.Gather(currentCell, _mining.GetArtefactList()[i], _tools._groundNCTilemap);
            }

            _tools._groundNCTilemap.SetTile(currentCell, _dirtTile);
        }
        else if (ruleTile == _dirtTile)
        {
            _tools._groundNCTilemap.SetTile(currentCell, _grassTile);
        }
    }
}
