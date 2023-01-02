using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string _header;
    public string _traitLocked;
    public string _content;
    public string _skill;
    public Sprite _repTierUnlock;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(_content, _header, _skill, _traitLocked, _repTierUnlock);       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
