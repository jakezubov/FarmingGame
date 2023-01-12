using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Tilemap _groundTilemap;
    public Spellbook _spellbook;
    public Use _use;
    public DropItem _dropItem;

    private static PlayerActionControls _playerActionControls;
    private Rigidbody2D _rb;
    private Animator _animator;

    private float _speed = 5;
    private readonly int _reach = 1;
    private int _activeSlot = 0;    
    private bool _isInteracting = false;
    private bool _isItemSelected = false;
    private InventoryItem _selectedItem = null;
    private Vector3Int _previousCell, _currentCell;

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
    } 

    private void FixedUpdate()
    {   
        if (!_spellbook.CheckSpellbookActive())
        {
            (float movementInputHoriztonal, float movementInputVertical) = GetPlayerMovements();
            _currentCell = _groundTilemap.WorldToCell(transform.position);

            if (movementInputHoriztonal == -1) { _currentCell.x -= _reach; _previousCell = _currentCell; }
            else if (movementInputHoriztonal == 1) { _currentCell.x += _reach; _previousCell = _currentCell; }
            else if (movementInputVertical == -1) { _currentCell.y -= _reach; _previousCell = _currentCell; }
            else if (movementInputVertical == 1) { _currentCell.y += _reach; _previousCell = _currentCell; }

            if (!_isInteracting)
            {               
                Move(movementInputHoriztonal, movementInputVertical);
            }
        }        
    }   

    public static (float, float) GetPlayerMovements()
    {
        float movementHoriztonal = _playerActionControls.General.MoveHorizontal.ReadValue<float>();
        float movementVertical = _playerActionControls.General.MoveVertical.ReadValue<float>();

        return (movementHoriztonal, movementVertical);
    }

    // For the endurance trait
    public void AddToSpeed(float value)
    {
        _speed += value;
    }

    private void SpellbookControls()
    {
        // trigger to close spellbook
        if (_playerActionControls.SpellBook.CloseSpellBook.triggered)
        {
            _spellbook.CloseSpellbook();
            _activeSlot = 0;
            InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
        }

        // triggers for turning pages
        if (_playerActionControls.SpellBook.PageLeft.triggered) { _spellbook.ChangeSectionLeft(); }
        if (_playerActionControls.SpellBook.PageRight.triggered) { _spellbook.ChangeSectionRight(); }

        // move mouse with navigation buttons
        if (_playerActionControls.SpellBook.NavigateDown.IsPressed() || _playerActionControls.SpellBook.NavigateUp.IsPressed() ||
            _playerActionControls.SpellBook.NavigateLeft.IsPressed() || _playerActionControls.SpellBook.NavigateRight.IsPressed())
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                TooltipSystem.MoveMouse();
            }
        }

        if (_playerActionControls.SpellBook.Select.triggered && !_isItemSelected &&
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventoryItem>() != null)
        {
            _selectedItem = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventoryItem>();
            _selectedItem.OnBeginNav();
            _isItemSelected = true;
        }
        else if (_playerActionControls.SpellBook.Select.triggered && _isItemSelected)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<BinSlot>() != null)
            {
                BinSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<BinSlot>();
                slot.OnDrop(_selectedItem);
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>() != null)
            {
                InventorySlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>();
                slot.OnDrop(_selectedItem);
                _selectedItem.OnEndNav();
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>() != null)
            {
                EquipmentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>();
                slot.OnDrop(_selectedItem);
                _selectedItem.OnEndNav();
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>() != null)
            {
                ComponentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>();
                slot.OnDrop(_selectedItem);
                _selectedItem.OnEndNav();
            }
                       
            _isItemSelected = false;
        }

        if (_isItemSelected)
        {
            Vector3 position = Mouse.current.position.ReadValue();
            position.z = Camera.main.nearClipPlane;
            _selectedItem.transform.position = Camera.main.ScreenToWorldPoint(position);
        }

        // for debug
        if (_playerActionControls.SpellBook.FreeExp.triggered) { PlayerManager._instance.FreeExp(); }
        if (_playerActionControls.SpellBook.FreeReputation.triggered) { PlayerManager._instance.FreeReputation(); }
    }

    private void GeneralControls()
    {
        // trigger to open spellbook
        if (_playerActionControls.General.OpenSpellBook.triggered)
        {
            _spellbook.OpenSpellbook();
            _activeSlot = -1;
            InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
        }

        // trigger for interacting with environment
        if (_playerActionControls.General.Interact.triggered && !_isInteracting)
        {
            (float movementInputHoriztonal, float movementInputVertical) = GetPlayerMovements();
            if (movementInputHoriztonal == 0 && movementInputVertical == 0) { _currentCell = _previousCell; }

            _use.GetData(_currentCell);
            StartCoroutine(PlaySwingAnimation());           
        }          

        // trigger for dropping items from toolbar
        if (_playerActionControls.General.Drop.triggered)
        {
            _dropItem.Drop(transform.position);
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
            CheckForAxeBool();
        }   
    }

    private IEnumerator PlaySwingAnimation()
    {
        _animator.SetBool("Swing", true);
        _isInteracting = true;

        //AnimatorClipInfo[] clip = _animator.GetCurrentAnimatorClipInfo(_animator.GetLayerIndex("Base Layer"));
        //float timer = clip[0].clip.length;

        yield return new WaitForSeconds(0.5f);
        
        _animator.SetBool("Swing", false);
        _isInteracting = false;
        _use.UseItemInSlot();
    }

    private void Move(float movementInputHoriztonal, float movementInputVertical)
    {    
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInputHoriztonal * _speed * Time.deltaTime;
        currentPosition.y += movementInputVertical * _speed * Time.deltaTime;
        _rb.MovePosition(currentPosition);

        // Animations
        if (movementInputHoriztonal == -1 && movementInputVertical == 0) // start horizontal checks
        {
            SetAllAnimationsFalse();
            _animator.SetBool("MoveSide", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movementInputHoriztonal == 1 && movementInputVertical == 0)
        {
            SetAllAnimationsFalse();
            _animator.SetBool("MoveSide", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movementInputVertical == -1 && movementInputHoriztonal == 0) // start vertical checks
        {
            SetAllAnimationsFalse();
            _animator.SetBool("MoveDown", true);
        }
        else if (movementInputVertical == 1 && movementInputHoriztonal == 0)
        {
            SetAllAnimationsFalse();
            _animator.SetBool("MoveUp", true);
        }
        else if (movementInputHoriztonal == 0 && movementInputVertical == 0) // check if no movement
        {
            SetAllAnimationsFalse();
        }  
    }

    private void SetAllAnimationsFalse()
    {
        _animator.SetBool("MoveSide", false);
        _animator.SetBool("MoveUp", false);
        _animator.SetBool("MoveDown", false);
    }

    public void CheckForAxeBool()
    {
        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null &&
            InventoryManager._instance.GetSelectedToolbarItem(false).toolType == ToolType.Axe)
        {
            _animator.SetBool("HasAxe", true);
        }
        else { _animator.SetBool("HasAxe", false); }
    }
}
