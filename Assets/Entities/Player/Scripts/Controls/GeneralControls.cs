using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GeneralControls : ControlsBaseState
{
    public DropItem _dropItem;
    public PlayerAnimations _anim;

    private Tilemap _buildingsTilemap;
    private int _activeSlot = 0;

    private void Awake() { SceneManager.sceneLoaded += OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Startup") { _buildingsTilemap = GameObject.Find("Building Bottoms C").GetComponent<Tilemap>(); }
    }

    public override void UseControls(PlayerController controller, SpellbookAnimations spellbook, Map map)
    {
        Vector3Int currentCell = controller.GetCurrentCell();

        // trigger for opening and closing map menu
        if (PlayerActions.GetControls().General.Map.triggered) 
        { 
            map.SwitchMapMenuState();
        }

        if (!PlayerActions.IsInteracting() && !PlayerActions.IsMapOpen())
        {
            // trigger to open spellbook
            if (PlayerActions.GetControls().General.OpenSpellBook.triggered)
            {
                spellbook.OpenSpellbookStart();
                _activeSlot = -1;
                InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
            }

            // trigger for interacting with environment
            if (PlayerActions.GetControls().General.UseToolbar.triggered)
            {
                _anim.StartToolAnimation(currentCell);
            }

            // trigger for dropping items from toolbar
            if (PlayerActions.GetControls().General.Drop.triggered)
            {
                _dropItem.Drop(transform.position);
            }

            // show 'E' above workbench to interact
            if (_buildingsTilemap.GetTile<RuleTileWithData>(currentCell) != null &&
                _buildingsTilemap.GetTile<RuleTileWithData>(currentCell).ruleTiletag == RuleTileTags.Workbench)
            {
                
                // trigger for interacting with world objects eg. doors, workbenches etc
                if (PlayerActions.GetControls().General.Interact.triggered)
                {
                    
                }
            }

            // triggers for changing toolbar slots
            if (PlayerActions.GetControls().General.ChangeSlots.triggered)
            {
                Vector2 value = PlayerActions.GetControls().General.ChangeSlots.ReadValue<Vector2>();
                if (value.y < 0)
                {
                    _activeSlot++;
                    if (_activeSlot == 10) { _activeSlot = 0; };
                }
                else if (value.y > 0)
                {
                    _activeSlot--;
                    if (_activeSlot == -1) { _activeSlot = 9; };
                }
                InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
            }
            else if (PlayerActions.GetControls().General.Slot1.triggered) { _activeSlot = 0; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot2.triggered) { _activeSlot = 1; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot3.triggered) { _activeSlot = 2; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot4.triggered) { _activeSlot = 3; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot5.triggered) { _activeSlot = 4; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot6.triggered) { _activeSlot = 5; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot7.triggered) { _activeSlot = 6; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot8.triggered) { _activeSlot = 7; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot9.triggered) { _activeSlot = 8; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (PlayerActions.GetControls().General.Slot10.triggered) { _activeSlot = 9; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
        }
    }
    
}
