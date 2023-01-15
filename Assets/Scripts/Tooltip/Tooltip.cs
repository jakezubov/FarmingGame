using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public Text _header;
    public Text _subHeading;
    public Text _description;
    public Text _extraText;
    public Image _image;

    public LayoutElement _layoutElement;
    public int _characterWrapLimit;

    private RectTransform _rectTransform;

    private void Awake() 
    { 
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string description, string header = "", string subHeading = "", string colour = "", string extraText = "", Sprite image = null)
    {
        _description.text = description;       

        // Header check
        if (string.IsNullOrEmpty(header)) { _header.gameObject.SetActive(false); }
        else { _header.gameObject.SetActive(true); _header.text = header; }

        // Sub Heading check
        if (string.IsNullOrEmpty(subHeading)) { _subHeading.gameObject.SetActive(false); }
        else 
        {
            _subHeading.gameObject.SetActive(true);
            _subHeading.text = subHeading;
            _subHeading.color = (Color)typeof(Color).GetProperty(colour.ToLowerInvariant()).GetValue(null, null);
        }

        // Extra Text check
        if (string.IsNullOrEmpty(extraText)) { _extraText.gameObject.SetActive(false); }
        else { _extraText.gameObject.SetActive(true); _extraText.text = extraText; }

        // Image check
        if (image == null) { _image.gameObject.SetActive(false); }
        else { _image.gameObject.SetActive(true); _image.sprite = image; }
      

        // checking for wraping text
        int headerLength = _header.text.Length;
        int contentLength = _description.text.Length;
        int subHeadingLength = _subHeading.text.Length;
        int extraTextLength = _extraText.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit ||
            subHeadingLength > _characterWrapLimit || extraTextLength > _characterWrapLimit) ? true : false;
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
