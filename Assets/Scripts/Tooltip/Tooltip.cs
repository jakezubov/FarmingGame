using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public Text _header;
    public Text _description;
    public Text _colouredText; 
    public Image _image;

    public LayoutElement _layoutElement;
    public int _characterWrapLimit;

    private RectTransform _rectTransform;

    private void Awake() 
    { 
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string description, string header = "", string colouredText = "", string colour = "", Sprite image = null)
    {
        _description.text = description;       

        // Header check
        if (string.IsNullOrEmpty(header)) { _header.gameObject.SetActive(false); }
        else { _header.gameObject.SetActive(true); _header.text = header; }

        // Coloured Text check 
        if (string.IsNullOrEmpty(colouredText)) { _colouredText.gameObject.SetActive(false); }
        else 
        { 
            _colouredText.gameObject.SetActive(true); 
            _colouredText.text = colouredText; 
            _colouredText.color = (Color)typeof(Color).GetProperty(colour.ToLowerInvariant()).GetValue(null, null); 
        }   

        // Image check
        if (image == null) { _image.gameObject.SetActive(false); }
        else { _image.gameObject.SetActive(true); _image.sprite = image; }
      
        int headerLength = _header.text.Length;
        int contentLength = _description.text.Length;
        int colouredTextLength = _colouredText.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit ||
            colouredTextLength > _characterWrapLimit) ? true : false;
    }

    void Update()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());     

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        if (pivotX >= 0.0) { pivotX += 1; }
        if (pivotY >= 0.0) { pivotY += 1; }

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;                  
    }
}
