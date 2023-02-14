using UnityEngine;

public class Shovel : MonoBehaviour
{
    public Stat _stamina;
    public MiningTraits _mining;
    public FishingTraits _fishing;

    public RuleTile _grassTile;
    public RuleTile _dirtTile;  
    public Item _bait;
    public Item[] _artefacts;
    
    private UseToolbar _use;

    private void Start()
    {
        _use = GetComponent<UseToolbar>();
    }

    public void Dig(Vector3Int currentCell, RuleTile ruleTile)
    {
        _stamina.LowerStatAmount(_use._baseStamina);

        // checks what tile is being interacted with and acts accordingly
        if (ruleTile == _grassTile)
        {
            // random chance to get bait (determined by bait finder trait)
            if (_fishing.RollForBait())
            {
                _use.Gather(currentCell, _bait, _use._groundTilemap);
            }       
            
            // random chance to get artefact (determined by archaeologist trait)
            if (_mining.RollForArtifact())
            {
                // if successful chooses random artefact
                int i = Random.Range(0, _artefacts.Length);
                _use.Gather(currentCell, _artefacts[i], _use._groundTilemap);
            }
            _use._groundTilemap.SetTile(currentCell, _dirtTile);
        }
        else if (ruleTile == _dirtTile)
        {
            _use._groundTilemap.SetTile(currentCell, _grassTile);
        }
    }
}
