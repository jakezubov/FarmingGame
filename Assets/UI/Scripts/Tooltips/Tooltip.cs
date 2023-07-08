using UnityEngine;
using UnityEngine.UI;

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
            subHeadingLength > _characterWrapLimit || extraTextLength > _characterWrapLimit);
    }
}
