using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _header;  
    [SerializeField] private string _description;
    [SerializeField] private string _colouredText;
    [SerializeField] private string _colour;
    [SerializeField] private Sprite _image;  

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(_description, _header, _colouredText, _colour, _image);       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    public void SetDescription(string newText)
    {
        _description = newText;
    }

    public void SetHeader(string newText)
    {
        _header = newText;
    }

    public void SetColouredText(string newText, string newColor)
    {
        _colouredText = newText;
        _colour = newColor;
    }
}
