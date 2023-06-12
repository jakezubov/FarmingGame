using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Properties

    public Spellbook _spellbook;    
    public Tools _tools;
    public DropItem _dropItem;
    public Map _map;
    private Tilemap _groundTilemap, _buildingsTilemap;
    public Animator _baseAnimator, _toolAnimator;

    public SkillHandler _skills; // for debugging
    public ReputationHandler _reputation; // for debugging

    private static PlayerActionControls _playerActionControls;
    private Rigidbody2D _rb;
    private Scene _scene;

    private int _reach = 1, _activeSlot = 0;
    private bool _isMapOpen = false, _isInteracting = false;
    private bool _isItemSelected = false, _isSpellSelected = false;
    private InventoryItem _selectedItem = null;
    private DraggableSpell _selectedSpell = null;
    private Vector3Int _previousCell, _currentCell;

    #endregion

    private void Awake()
    {
        _playerActionControls = new PlayerActionControls();
        _rb = GetComponent<Rigidbody2D>();
        _spellbook.gameObject.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _scene = scene;
        if (scene.name != "Startup")
        {
            _groundTilemap = GameObject.Find("Ground NC").GetComponent<Tilemap>();
            _buildingsTilemap = GameObject.Find("Building Bottoms C").GetComponent<Tilemap>();
        }
    }

    private void OnEnable() { _playerActionControls.Enable(); }
    private void OnDisable() { _playerActionControls.Disable(); }

    private void Update()
    {
        if (_scene.name != "Startup")
        {
            // switch between action maps
            if (_spellbook.CheckSpellbookActive()) { _playerActionControls.SpellBook.Enable(); _playerActionControls.General.Disable(); }
            else { _playerActionControls.SpellBook.Disable(); _playerActionControls.General.Enable(); }

            // if spellbook action map is enabled
            if (_playerActionControls.SpellBook.enabled)
            {
                SpellbookControls();
            }

            // if general action map is enabled
            if (_playerActionControls.General.enabled)
            {
                GeneralControls();
            }

            if (_buildingsTilemap.GetTile<RuleTileWithData>(_currentCell) != null &&
                _buildingsTilemap.GetTile<RuleTileWithData>(_currentCell).ruleTiletag == RuleTileTags.Workbench)
            {
                // show 'E' above workbench to interact
            }
        }
    } 

    private void FixedUpdate()
    {
        if (_scene.name != "Startup")
        {
            if (!_spellbook.CheckSpellbookActive() && !_isInteracting && !_isMapOpen && !_spellbook.GetAnimationState())
            {
                (float movementInputHoriztonal, float movementInputVertical) = GetPlayerMovements();
                _currentCell = _groundTilemap.WorldToCell(transform.position);

                // Move the player
                Vector3 currentPosition = transform.position;
                currentPosition.x += movementInputHoriztonal * SaveData.moveSpeed * Time.deltaTime;
                currentPosition.y += movementInputVertical * SaveData.moveSpeed * Time.deltaTime;
                _rb.MovePosition(currentPosition);

                // controls all the movement animations
                if (movementInputHoriztonal == -1 && movementInputVertical == 0) // start horizontal checks
                {
                    _currentCell.x -= _reach;
                    _previousCell = _currentCell;

                    SetAllMoveAnimationsFalse();
                    _baseAnimator.SetBool("MoveLeft", true);
                    _toolAnimator.SetBool("MoveLeft", true);
                }
                else if (movementInputHoriztonal == 1 && movementInputVertical == 0)
                {
                    _currentCell.x += _reach;
                    _previousCell = _currentCell;

                    SetAllMoveAnimationsFalse();
                    _baseAnimator.SetBool("MoveRight", true);
                    _toolAnimator.SetBool("MoveRight", true);
                }
                else if (movementInputVertical == -1 && movementInputHoriztonal == 0) // start vertical checks
                {
                    _currentCell.y -= _reach;
                    _previousCell = _currentCell;

                    SetAllMoveAnimationsFalse();
                    _baseAnimator.SetBool("MoveDown", true);
                    _toolAnimator.SetBool("MoveDown", true);
                }
                else if (movementInputVertical == 1 && movementInputHoriztonal == 0)
                {
                    _currentCell.y += _reach;
                    _previousCell = _currentCell;

                    SetAllMoveAnimationsFalse();
                    _baseAnimator.SetBool("MoveUp", true);
                    _toolAnimator.SetBool("MoveUp", true);
                }
                else if (movementInputHoriztonal == 0 && movementInputVertical == 0) // check if no movement
                {
                    _currentCell = _previousCell;
                    SetAllMoveAnimationsFalse();
                }
            }
        }        
    }   

    public static (float, float) GetPlayerMovements()
    {
        float movementHoriztonal = _playerActionControls.General.MoveHorizontal.ReadValue<float>();
        float movementVertical = _playerActionControls.General.MoveVertical.ReadValue<float>();

        return (movementHoriztonal, movementVertical);
    }

    private void SpellbookControls()
    {
        if (!_spellbook.GetAnimationState())
        {
            // trigger to close spellbook
            if (_playerActionControls.SpellBook.CloseSpellBook.triggered) { StartCoroutine(_spellbook.CloseSpellbook()); EndDrag(); }

            // trigger when opening the options menu
            if (_playerActionControls.SpellBook.Map.triggered)
            {
                StartCoroutine(_spellbook.CloseSpellbookFast());
                EndDrag();
                _map.OpenMapMenu();
                _isMapOpen = true;
            }

            // triggers for turning pages
            if (_playerActionControls.SpellBook.PageLeft.triggered) { StartCoroutine(_spellbook.ChangeSectionLeft(true)); }
            if (_playerActionControls.SpellBook.PageRight.triggered) { StartCoroutine(_spellbook.ChangeSectionRight(true)); }

            // move mouse with navigation buttons
            if (_playerActionControls.SpellBook.NavigateDown.IsPressed() || _playerActionControls.SpellBook.NavigateUp.IsPressed() ||
                _playerActionControls.SpellBook.NavigateLeft.IsPressed() || _playerActionControls.SpellBook.NavigateRight.IsPressed())
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    TooltipSystem.MoveMouse();
                }
            }

            // controls movement of items and spells while using keyboard
            if (_playerActionControls.SpellBook.Select.triggered && !_isItemSelected && !_isSpellSelected)
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventoryItem>() != null)
                {
                    _selectedItem = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventoryItem>();
                    _selectedItem.OnBeginNav();
                    _isItemSelected = true;
                }
                else if (EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetComponent<DraggableSpell>() != null)
                {
                    _selectedSpell = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetComponent<DraggableSpell>();
                    _selectedSpell.OnBeginNav();
                    _isSpellSelected = true;
                }
            }
            else if (_playerActionControls.SpellBook.Select.triggered)
            {
                if (_isItemSelected)
                {
                    // checks which type of slot the item is placed in
                    if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<BinSlot>() != null)
                    {
                        BinSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<BinSlot>();
                        slot.OnDrop(_selectedItem);
                    }
                    else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>() != null)
                    {
                        InventorySlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>();
                        slot.OnDrop(_selectedItem);
                    }
                    else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>() != null)
                    {
                        EquipmentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>();
                        slot.OnDrop(_selectedItem);
                    }
                    else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>() != null)
                    {
                        ComponentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>();
                        slot.OnDrop(_selectedItem);
                    }

                    _selectedItem.OnEndNav();
                    _isItemSelected = false;
                }
                else if (_isSpellSelected)
                {
                    if (EventSystem.current.currentSelectedGameObject.GetComponentInParent<SpellSlot>() != null)
                    {
                        SpellSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInParent<SpellSlot>();
                        slot.OnDrop(_selectedSpell.GetSpell());
                    }

                    _selectedSpell.OnEndNav();
                    _isSpellSelected = false;
                }
            }

            // making the item or spell follow the cursor when selected
            if (_isItemSelected)
            {
                Vector3 position = Mouse.current.position.ReadValue();
                position.z = Camera.main.nearClipPlane;
                _selectedItem.transform.position = Camera.main.ScreenToWorldPoint(position);
            }
            else if (_isSpellSelected)
            {
                Vector3 position = Mouse.current.position.ReadValue();
                position.z = Camera.main.nearClipPlane;
                _selectedSpell._image.gameObject.transform.position = Camera.main.ScreenToWorldPoint(position);
            }

            // for debug
            if (_playerActionControls.SpellBook.FreeExp.triggered) { _skills.FreeExp(); }
            if (_playerActionControls.SpellBook.FreeReputation.triggered) { _reputation.FreeReputation(); }
        }
    }

    private void GeneralControls()
    {
        // trigger to open spellbook
        if (_playerActionControls.General.OpenSpellBook.triggered)
        {
            StartCoroutine(_spellbook.OpenSpellbook());
            _activeSlot = -1;
            InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
            if (_isMapOpen) { _map.CloseMapMenu(); _isMapOpen = false; }
        }

        // trigger for opening and closing map menu
        if (_playerActionControls.General.Map.triggered && !_isMapOpen && !_spellbook.GetAnimationState()) { _map.OpenMapMenu(); _isMapOpen = true; }
        else if (_playerActionControls.General.Map.triggered && _isMapOpen) { _map.CloseMapMenu(); _isMapOpen = false; }

        if (!_isInteracting && !_isMapOpen)
        {
            // trigger for interacting with environment
            if (_playerActionControls.General.UseToolbar.triggered)
            {
                _tools.GetData(_currentCell);
                StartCoroutine(PlayToolAnimation());
            }

            // trigger for dropping items from toolbar
            if (_playerActionControls.General.Drop.triggered)
            {
                _dropItem.Drop(transform.position);
            }

            // trigger for interacting with world objects eg. doors, workbenches etc
            if (_playerActionControls.General.Interact.triggered && _buildingsTilemap.GetTile<RuleTileWithData>(_currentCell) != null &&
                _buildingsTilemap.GetTile<RuleTileWithData>(_currentCell).ruleTiletag == RuleTileTags.Workbench)
            {

            }

            // triggers for changing toolbar slots
            if (_playerActionControls.General.ChangeSlots.triggered)
            {
                Vector2 value = _playerActionControls.General.ChangeSlots.ReadValue<Vector2>();
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
            else if (_playerActionControls.General.Slot1.triggered) { _activeSlot = 0; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot2.triggered) { _activeSlot = 1; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot3.triggered) { _activeSlot = 2; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot4.triggered) { _activeSlot = 3; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot5.triggered) { _activeSlot = 4; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot6.triggered) { _activeSlot = 5; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot7.triggered) { _activeSlot = 6; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot8.triggered) { _activeSlot = 7; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot9.triggered) { _activeSlot = 8; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            else if (_playerActionControls.General.Slot10.triggered) { _activeSlot = 9; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
        }    
    }

    private IEnumerator PlayToolAnimation()
    {
        _baseAnimator.SetBool("UseTool", true);
        _toolAnimator.SetBool("UseTool", true);
        _isInteracting = true;
        CheckForTools();

        // base yield time on animation length
        yield return new WaitForSeconds(0.35f);
        _tools.UseItemInSlot();
        yield return new WaitForSeconds(0.3f);

        _isInteracting = false;
        _baseAnimator.SetBool("UseTool", false);
        _toolAnimator.SetBool("UseTool", false);
    }

    private void CheckForTools()
    {
        // changes the animation for what tool is held based on the currently selected toolbar item
        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null &&
           InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
        {
            Tool tool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);

            _toolAnimator.SetBool("HasPick", false);
            _toolAnimator.SetBool("HasAxe", false);
            _toolAnimator.SetBool("HasHoe", false);

            if (tool.toolType == ToolType.Axe) { _toolAnimator.SetBool("HasAxe", true); }
            else if (tool.toolType == ToolType.Pickaxe) { _toolAnimator.SetBool("HasPick", true); }
            else if (tool.toolType == ToolType.Hoe) { _toolAnimator.SetBool("HasHoe", true); }
        }
    }

    private void SetAllMoveAnimationsFalse()
    {
        // default state for animations
        _baseAnimator.SetBool("MoveLeft", false);
        _baseAnimator.SetBool("MoveRight", false);
        _baseAnimator.SetBool("MoveUp", false);
        _baseAnimator.SetBool("MoveDown", false);
        _toolAnimator.SetBool("MoveLeft", false);
        _toolAnimator.SetBool("MoveRight", false);
        _toolAnimator.SetBool("MoveUp", false);
        _toolAnimator.SetBool("MoveDown", false);
    }
    
    private void EndDrag()
    {
        // ends dragging any objects when the menu is closed
        if (_isItemSelected) { _selectedItem.OnEndNav(); _isItemSelected = false; }
        else if (_isSpellSelected) { _selectedSpell.OnEndNav(); _isSpellSelected = false; }
    }
}
