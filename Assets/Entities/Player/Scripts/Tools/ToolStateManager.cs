using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ToolStateManager : MonoBehaviour
{
    public Tilemap _groundNCTilemap;
    public Tilemap _droppedNCTilemap;
    public Tilemap _resourcesCTilemap;
    public Tilemap _environmentNCTilemap;

    public GameObject _lootPrefab;
    public GameObject _parentAfterDrop;
    public Stat _stamina;

    public readonly int _baseStamina = 10;
    public readonly int _baseExp = 10;
    
    private ShovelState _shovel;
    private PickaxeState _pickaxe;
    private AxeState _axe;
    private ForageState _forage;
    private FishingState _fish;

    private RuleTileWithData _resourceTile;
    private RuleTile _groundTile;
    private Item _toolbarItem;
    private Tool _toolbarTool;
    private Vector3Int _currentCell;
    private readonly float _efficiencyModifier = 0.2f;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _shovel = GetComponent<ShovelState>();
        _pickaxe = GetComponent<PickaxeState>();
        _axe = GetComponent<AxeState>();
        _forage = GetComponent<ForageState>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Startup")
        {
            _groundNCTilemap = GameObject.Find("Ground NC").GetComponent<Tilemap>();
            _droppedNCTilemap = GameObject.Find("Dropped Objects NC").GetComponent<Tilemap>();
            _resourcesCTilemap = GameObject.Find("Resources C").GetComponent<Tilemap>();
            _environmentNCTilemap = GameObject.Find("Environment Tops NC").GetComponent<Tilemap>();
        }
    }

    public void GetData(Vector3Int position)
    {
        // resets to default state
        _groundTile = null;
        _resourceTile = null;
        _toolbarItem = null;
        _toolbarTool = null;

        // assigns values based on what the highlight tile is on and what is the current toolbar item
        // GetData and UseItemInSlot are seperate so the animation has time to play before anything happens
        if (_resourcesCTilemap.GetTile<RuleTileWithData>(position) != null)
        {
            _resourceTile = _resourcesCTilemap.GetTile<RuleTileWithData>(position);
            _currentCell = position;     
        }
        else
        {
            _groundTile = _groundNCTilemap.GetTile<RuleTile>(position);
            _currentCell = position; 
        }

        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null &&
           InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
        {
            _toolbarTool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);
        }
        else { _toolbarItem = InventoryManager._instance.GetSelectedToolbarItem(false); }
    }

    public void UseItemInSlot()
    {
        // does an action based on what tool is active 
        if (_toolbarItem != null && _toolbarItem.type == Type.BuildingBlock)
        {
            Place();
        }
        else if (_stamina.GetCurrentValue() > 0)
        {
            if (_groundTile != null)
            {
                _shovel.UseTool(this, _currentCell, _toolbarTool);
            }
            else if (_resourceTile != null)
            {
                if (_resourceTile.ruleTiletag == RuleTileTags.Foragable)
                {
                    _forage.UseTool(this, _currentCell, _toolbarTool);
                }  
                else if (_toolbarTool.type == Type.Tool)
                {
                    if (_resourceTile.ruleTiletag == RuleTileTags.Forestry)
                    {
                        _axe.UseTool(this, _currentCell, _toolbarTool);
                    }
                    else if (_resourceTile.ruleTiletag == RuleTileTags.Mining)
                    {
                        _pickaxe.UseTool(this, _currentCell, _toolbarTool);
                    }    
                    else if (_resourceTile.ruleTiletag == RuleTileTags.Fishing)
                    {
                        _fish.UseTool(this, _currentCell, _toolbarTool);
                    }
                }                       
            }
        }
    }

    public bool ChanceForExtraResources(int range)
    {
        int randChance = Random.Range(1, range + 1);
        if (randChance == 1)
        {
            return true;
        }
        else { return false; }
    }

    public void Gather(Vector3Int position, Item item, Tilemap tilemap)
    {
        // drops an item onto the ground (seperate game object)
        tilemap.SetTile(position, null);
        if (item != null)
        {
            Vector3 pos = _droppedNCTilemap.GetCellCenterWorld(position);
            GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);
            loot.GetComponent<LootItem>().Initialise(item);
            loot.transform.SetParent(_parentAfterDrop.transform);
        }    
    }

    public float GetEfficiencyModifier()
    {
        return _efficiencyModifier;
    }

    public RuleTile GetRuleTile()
    {
        return _groundTile;
    }

    public RuleTileWithData GetRuleTileWithData()
    {
        return _resourceTile;
    }

    private void Place()
    {
        // places an item directly to the tilemap
        Item itemToPlace = InventoryManager._instance.GetSelectedToolbarItem(true);
        _resourcesCTilemap.SetTile(_currentCell, itemToPlace.mapTile);
    }
}

