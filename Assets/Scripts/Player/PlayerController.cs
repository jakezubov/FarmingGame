using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Properties

    public Spellbook _spellbook;    
    public UseToolbar _use;
    public DropItem _dropItem;
    public GameObject _mapMenu, _mapMenuFirstButton;
    public Tilemap _groundTilemap, _buildingsTilemap;

    public SkillHandler _skills; // for debugging
    public ReputationHandler _reputation; // for debugging

    private static PlayerActionControls _playerActionControls;
    private Rigidbody2D _rb;
    private Animator _animator;

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
        _animator = GetComponent<Animator>();
        _spellbook.gameObject.SetActive(false);  
    }

    private void OnEnable() { _playerActionControls.Enable(); }
    private void OnDisable() { _playerActionControls.Disable(); }

    private void Update()
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

    private void FixedUpdate()
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

                SetAllAnimationsFalse();
                _animator.SetBool("MoveSide", true);
                GetComponentInParent<Transform>().localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (movementInputHoriztonal == 1 && movementInputVertical == 0)
            {
                _currentCell.x += _reach; 
                _previousCell = _currentCell;

                SetAllAnimationsFalse();
                _animator.SetBool("MoveSide", true);
                GetComponentInParent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            }
            else if (movementInputVertical == -1 && movementInputHoriztonal == 0) // start vertical checks
            {
                _currentCell.y -= _reach; 
                _previousCell = _currentCell;

                SetAllAnimationsFalse();
                _animator.SetBool("MoveDown", true);
            }
            else if (movementInputVertical == 1 && movementInputHoriztonal == 0)
            {
                _currentCell.y += _reach; 
                _previousCell = _currentCell;

                SetAllAnimationsFalse();
                _animator.SetBool("MoveUp", true);
            }
            else if (movementInputHoriztonal == 0 && movementInputVertical == 0) // check if no movement
            {
                _currentCell = _previousCell;
                SetAllAnimationsFalse();
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
                OpenMapMenu();
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
            if (_isMapOpen) { CloseMapMenu(); }
        }

        // trigger for opening and closing map menu
        if (_playerActionControls.General.Map.triggered && !_isMapOpen && !_spellbook.GetAnimationState()) { OpenMapMenu(); }
        else if (_playerActionControls.General.Map.triggered && _isMapOpen) { CloseMapMenu(); }

        if (!_isInteracting && !_isMapOpen)
        {
            // trigger for interacting with environment
            if (_playerActionControls.General.UseToolbar.triggered)
            {
                _use.GetData(_currentCell);
                StartCoroutine(PlaySwingAnimation());
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
            if (_playerActionControls.General.Slot0.triggered || _playerActionControls.General.Slot1.triggered || _playerActionControls.General.Slot2.triggered ||
                _playerActionControls.General.Slot3.triggered || _playerActionControls.General.Slot4.triggered || _playerActionControls.General.Slot5.triggered ||
                _playerActionControls.General.Slot6.triggered || _playerActionControls.General.Slot7.triggered || _playerActionControls.General.ChangeSlots.triggered)
            {
                if (_playerActionControls.General.ChangeSlots.triggered)
                {
                    Vector2 value = _playerActionControls.General.ChangeSlots.ReadValue<Vector2>();
                    if (value.y < 0)
                    {
                        _activeSlot++;
                        if (_activeSlot == 8) { _activeSlot = 0; };
                    }
                    else if (value.y > 0)
                    {
                        _activeSlot--;
                        if (_activeSlot == -1) { _activeSlot = 7; };
                    }
                }
                else if (_playerActionControls.General.Slot0.triggered) { _activeSlot = 0; }
                else if (_playerActionControls.General.Slot1.triggered) { _activeSlot = 1; }
                else if (_playerActionControls.General.Slot2.triggered) { _activeSlot = 2; }
                else if (_playerActionControls.General.Slot3.triggered) { _activeSlot = 3; }
                else if (_playerActionControls.General.Slot4.triggered) { _activeSlot = 4; }
                else if (_playerActionControls.General.Slot5.triggered) { _activeSlot = 5; }
                else if (_playerActionControls.General.Slot6.triggered) { _activeSlot = 6; }
                else if (_playerActionControls.General.Slot7.triggered) { _activeSlot = 7; }

                InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
                CheckForTools();
            }
        }    
    }

    private void OpenMapMenu()
    {
        _spellbook._toolbarObject.SetActive(false);
        _mapMenu.SetActive(true);
        _isMapOpen = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_mapMenuFirstButton);
        TooltipSystem.MoveMouse();
    }

    private void CloseMapMenu()
    {
        _spellbook._toolbarObject.SetActive(true);
        _mapMenu.SetActive(false);
        _isMapOpen = false;

        TooltipSystem.Hide();
    }

    private IEnumerator PlaySwingAnimation()
    {
        _animator.SetBool("Swing", true);
        _isInteracting = true;

        //AnimatorClipInfo[] clip = _animator.GetCurrentAnimatorClipInfo(_animator.GetLayerIndex("Base Layer"));
        //float timer = clip[0].clip.length;

        // base yield time on animation length
        yield return new WaitForSeconds(0.35f);
 
        bool used = _use.UseItemInSlot();
        if (used)
        {
            _isInteracting = false;
            _animator.SetBool("Swing", false);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            _isInteracting = false;
            _animator.SetBool("Swing", false);
        }
    }

    private void SetAllAnimationsFalse()
    {
        // default state for animations
        _animator.SetBool("MoveSide", false);
        _animator.SetBool("MoveUp", false);
        _animator.SetBool("MoveDown", false);
    }

    public void CheckForTools()
    {
        // changes the animation for what tool is held based on the currently selected toolbar item
        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null &&
           InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
        {
            Tool tool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);

            if (tool.toolType == ToolType.Axe)
            {
                _animator.SetBool("HasAxe", true);
                _animator.SetBool("HasPickaxe", false);
            }
            else if (tool.toolType == ToolType.Pickaxe)
            {
                _animator.SetBool("HasPickaxe", true);
                _animator.SetBool("HasAxe", false);
            }
            else
            {
                _animator.SetBool("HasAxe", false);
                _animator.SetBool("HasPickaxe", false);
            }
        }
    }
    
    public void EndDrag()
    {
        // ends dragging any objects when the menu is closed
        if (_isItemSelected) { _selectedItem.OnEndNav(); _isItemSelected = false; }
        else if (_isSpellSelected) { _selectedSpell.OnEndNav(); _isSpellSelected = false; }
    }
}
