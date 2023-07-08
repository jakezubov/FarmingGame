using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    public Image _icon;
    public Color _colour;

    private Spell _currentSpell;

    public void OnDrop(PointerEventData eventData)
    {
        OnDropBase(eventData.pointerDrag.GetComponent<DraggableSpell>().GetSpell());
    }

    public void OnDrop(Spell spell)
    {
        OnDropBase(spell);
    }

    private void OnDropBase(Spell spell)
    {
        _currentSpell = spell;
        _icon.sprite = spell.image;
        _icon.color = _colour;

        TooltipTrigger tooltip = GetComponentInChildren<TooltipTrigger>();
        tooltip.SetHeader(spell.name);
        tooltip.SetDescription(spell.description);
    }

    public Spell GetCurrentSpell()
    {
        return _currentSpell;
    }
}
