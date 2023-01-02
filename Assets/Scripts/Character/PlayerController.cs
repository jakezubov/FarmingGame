using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Interact _interact;
    public Tilemap _foliageTilemap;
    public Spellbook _spellbook;
    public Stat _health, _mana, _stamina;

    // for debug
    [HideInInspector] public Reputation _reputation;
    [HideInInspector]public AllSkills _skills;

    private static PlayerActionControls _playerActionControls;   
    private Rigidbody2D _rb;
    private Animator _animator;   

    [SerializeField] private float _speed = 5;
    private readonly int _reach = 1;
    private Vector3Int _previousCell, _currentCell;

    private int _activeSlot = 0;
    private bool _isItemSelected = false;
    private InventoryItem _selectedItem = null;

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
                if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>() != null)
                {
                    InventorySlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InventorySlot>();
                    slot.NavOnDrop(_selectedItem);
                }
                else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>() != null)
                {
                    EquipmentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<EquipmentSlot>();
                    slot.NavOnDrop(_selectedItem);
                }
                else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>() != null)
                {
                    ComponentSlot slot = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<ComponentSlot>();
                    slot.NavOnDrop(_selectedItem);
                }

                _selectedItem.OnEndNav();
                _isItemSelected = false;
            }

            if (_isItemSelected)
            {
                Vector3 position = Mouse.current.position.ReadValue();
                position.z = Camera.main.nearClipPlane;
                _selectedItem.transform.position = Camera.main.ScreenToWorldPoint(position);
                
            }     

            // for debug
            if (_playerActionControls.SpellBook.FreeExp.triggered) { _skills.FreeExp(); }
            if (_playerActionControls.SpellBook.FreeReputation.triggered) { _reputation.FreeReputation(); }
        }

        // if general action map is enabled
        if (_playerActionControls.General.enabled)
        {
            // trigger to open spellbook
            if (_playerActionControls.General.OpenSpellBook.triggered) 
            { 
                _spellbook.OpenSpellbook(); 
                _activeSlot = -1;
                InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); 
            }

            // trigger for interacting with environment
            if (_playerActionControls.General.Interact.triggered && !_spellbook.CheckSpellbookActive())
            {
                (float movementInputHoriztonal, float movementInputVertical) = GetPlayerMovements();
                if (movementInputHoriztonal == 0 && movementInputVertical == 0) { _currentCell = _previousCell; }

                _interact.TryInteract(_currentCell);
            }

            // triggers for changing toolbar slots
            if (_playerActionControls.General.Slot0.triggered) { _activeSlot = 0; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot1.triggered) { _activeSlot = 1; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot2.triggered) { _activeSlot = 2; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot3.triggered) { _activeSlot = 3; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot4.triggered) { _activeSlot = 4; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot5.triggered) { _activeSlot = 5; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot6.triggered) { _activeSlot = 6; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.Slot7.triggered) { _activeSlot = 7; InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot); }
            if (_playerActionControls.General.ChangeSlots.triggered) 
            {
                Vector2 value = _playerActionControls.General.ChangeSlots.ReadValue<Vector2>();
                if (value.y < 0)
                {
                    _activeSlot++;
                    if (_activeSlot == 8) { _activeSlot = 0; };
                    InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
                }
                if (value.y > 0)
                {
                    _activeSlot--;
                    if (_activeSlot == -1) { _activeSlot = 7; };
                    InventoryManager._instance.ChangeToolbarSelectedSlot(_activeSlot);
                }
            }           

            // for debug
            if (_playerActionControls.General.FreeExp.triggered) { _skills.FreeExp(); }
            if (_playerActionControls.General.FreeReputation.triggered) { _reputation.FreeReputation(); }
        }      
    } 

    private void FixedUpdate()
    {   
        if (!_spellbook.CheckSpellbookActive())
        {
            (float movementInputHoriztonal, float movementInputVertical) = GetPlayerMovements();
            _currentCell = _foliageTilemap.WorldToCell(transform.position);

            if (movementInputHoriztonal == -1) { _currentCell.x -= _reach; _previousCell = _currentCell; }
            else if (movementInputHoriztonal == 1) { _currentCell.x += _reach; _previousCell = _currentCell; }
            else if (movementInputVertical == -1) { _currentCell.y -= _reach; _previousCell = _currentCell; }
            else if (movementInputVertical == 1) { _currentCell.y += _reach; _previousCell = _currentCell; }

            Move(movementInputHoriztonal,movementInputVertical);
        }        
    }

    private void Move(float movementInputHoriztonal, float movementInputVertical)
    {
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInputHoriztonal * _speed * Time.deltaTime;
        currentPosition.y += movementInputVertical * _speed * Time.deltaTime;
        _rb.MovePosition(currentPosition);

        // Animations
        if (movementInputHoriztonal == -1) 
        { 
            _animator.SetBool("MoveSide", true); 
            _animator.SetBool("Idle", false); 
            GetComponent<SpriteRenderer>().flipX = true; 
        }
        else if (movementInputHoriztonal == 1) 
        { 
            _animator.SetBool("MoveSide", true); 
            _animator.SetBool("Idle", false); 
            GetComponent<SpriteRenderer>().flipX = false; 
        }
        else 
        { 
            _animator.SetBool("Idle", true); 
            _animator.SetBool("MoveSide", false); 
        }

        if (movementInputVertical == -1) 
        { 
            _animator.SetBool("MoveUp", false);
            _animator.SetBool("MoveDown", true); 
            _animator.SetBool("Idle", false); 
        }
        else if (movementInputVertical == 1) 
        { 
            _animator.SetBool("MoveUp", true); 
            _animator.SetBool("MoveDown", false); 
            _animator.SetBool("Idle", false); 
        }
        else 
        {            
            _animator.SetBool("MoveUp", false); 
            _animator.SetBool("MoveDown", false);
            _animator.SetBool("Idle", true);
        }
    }

    public static (float, float) GetPlayerMovements()
    {
        float movementHoriztonal = _playerActionControls.General.MoveHorizontal.ReadValue<float>();
        float movementVertical = _playerActionControls.General.MoveVertical.ReadValue<float>();

        return (movementHoriztonal, movementVertical);
    }

    // For the endurance trait unlock
    public void AddToSpeed(float value)
    {
        _speed += value;
    }
}
