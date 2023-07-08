using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    public Image _image;
    public Color _selectedColour, _notSelectedColour;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        // for toolbar selected slot
        _image.color = _selectedColour;
    }

    public void Deselect()
    {
        // for toolbar selected slot
        _image.color = _notSelectedColour;
    }

    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (transform.childCount == 0) // dropping on slot with nothing in it
        {
            newInventoryItem.SetParentAfterDrag(transform);
            _currentInventoryItem = newInventoryItem;
        }
        else if (transform.childCount == 1) // dropping on slot that already has item
        {
            // checks if item can be stacked onto the current item in the slot else it checks if
            // the slot the dragged item has come from is compatible to swap with the item in the current slot
            if (_currentInventoryItem.GetItem().maxStack > 1 && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                _currentInventoryItem.GetCount() + newInventoryItem.GetCount() <= _currentInventoryItem.GetItem().maxStack)
            {
                // for stacking items
                _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                Destroy(newInventoryItem.gameObject);
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>() && _currentInventoryItem.GetItem().type == Type.SpellComponent)
            {
                // for spell components
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>().SetCurrentItem(_currentInventoryItem);
                _currentInventoryItem = newInventoryItem;
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>() && _currentInventoryItem.GetItem().type == Type.Equipment)
            {
                // for equipment
                Equipment currentEquipment = (Equipment)_currentInventoryItem.GetItem();

                if (currentEquipment.equipmentType == newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().GetSlotType())
                {
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                    newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().SetCurrentItem(_currentInventoryItem);
                    _currentInventoryItem = newInventoryItem;
                }
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>())
            {
                // for inventory slots
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>().SetCurrentItem(_currentInventoryItem);
                _currentInventoryItem = newInventoryItem;
            }
        }
    }
}
