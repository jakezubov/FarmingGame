using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _header;  
    [SerializeField] private string _description;
    [SerializeField] private string _colouredText;
    [SerializeField] private string _colour;
    [SerializeField] private string _extraText;
    [SerializeField] private Sprite _image;  

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(_description, _header, _colouredText, _colour, _extraText, _image);       
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

    public void SetExtraText(string newText)
    {
        _extraText = newText;
    }

    public void SetSubHeading(string text, string colour)
    {
        _colouredText = text;
        _colour = colour;
    }
}
