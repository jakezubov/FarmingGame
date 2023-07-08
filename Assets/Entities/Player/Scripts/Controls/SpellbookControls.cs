using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;

public class SpellbookControls : ControlsBaseState
{
    public SkillHandler _skills; // for debugging
    public ReputationHandler _reputation; // for debugging

    private InventoryItem _selectedItem = null;
    private DraggableSpell _selectedSpell = null;
    private bool _isItemSelected = false, _isSpellSelected = false;

    public override void UseControls(PlayerController controller, SpellbookAnimations spellbook, Map map)
    {
        // trigger when opening the map menu
        if (PlayerActions.GetControls().SpellBook.Map.triggered)
        {
            EndDrag();
            map.SwitchMapMenuState();
        }

        if (!PlayerActions.IsMapOpen())
        {
            // trigger to close spellbook
            if (PlayerActions.GetControls().SpellBook.CloseSpellBook.triggered) 
            { 
                spellbook.CloseSpellbookStart(); 
                EndDrag(); 
            }

            // triggers for turning pages
            if (PlayerActions.GetControls().SpellBook.PageLeft.triggered) { spellbook.SectionLeftStart(true); }
            if (PlayerActions.GetControls().SpellBook.PageRight.triggered) { spellbook.SectionRightStart(true); }

            // move mouse with navigation buttons
            if (PlayerActions.GetControls().SpellBook.NavigateDown.IsPressed() || PlayerActions.GetControls().SpellBook.NavigateUp.IsPressed() ||
                PlayerActions.GetControls().SpellBook.NavigateLeft.IsPressed() || PlayerActions.GetControls().SpellBook.NavigateRight.IsPressed())
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    TooltipSystem.MoveMouse();
                }
            }

            // controls movement of items and spells while using keyboard
            if (PlayerActions.GetControls().SpellBook.Select.triggered && !_isItemSelected && !_isSpellSelected)
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
            else if (PlayerActions.GetControls().SpellBook.Select.triggered)
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
            if (PlayerActions.GetControls().SpellBook.FreeExp.triggered) { _skills.FreeExp(); }
            if (PlayerActions.GetControls().SpellBook.FreeReputation.triggered) { _reputation.FreeReputation(); }
        }    
    }

    private void EndDrag()
    {
        // ends dragging any objects when the menu is closed
        if (_isItemSelected) 
        { 
            _selectedItem.OnEndNav(); 
            _isItemSelected = false; 
        }
        else if (_isSpellSelected) 
        { 
            _selectedSpell.OnEndNav(); 
            _isSpellSelected = false; 
        }
    }
}
