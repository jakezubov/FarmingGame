using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public Text _headerField;
    public Text _contentField;
    public Text _traitChanges;
    public Text _traitLocked;
    public Image _repTierImage;   
    public LayoutElement _layoutElement;
    public int _characterWrapLimit;

    private RectTransform _rectTransform;

    private void Awake() 
    { 
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "", string traitChanges = "", string traitLocked = "", Sprite repTierUnlock = null)
    {
        // Header check
        if (string.IsNullOrEmpty(header)) { _headerField.gameObject.SetActive(false); }
        else { _headerField.gameObject.SetActive(true); _headerField.text = header; }

        // Trait changes check
        if (string.IsNullOrEmpty(traitChanges)) { _traitChanges.gameObject.SetActive(false); }
        else 
        {
            traitChanges = traitChanges.Replace("---", "\n");
            _traitChanges.gameObject.SetActive(true);
            _traitChanges.text = traitChanges; 
        }

        // Trait locked check
        if (string.IsNullOrEmpty(traitLocked)) { _traitLocked.gameObject.SetActive(false); }
        else { _traitLocked.gameObject.SetActive(true); _traitLocked.text = traitLocked; }

        // RepTierUnlock check
        if (repTierUnlock == null) { _repTierImage.gameObject.SetActive(false); }
        else { _repTierImage.gameObject.SetActive(true); _repTierImage.sprite = repTierUnlock; }

        _contentField.text = content;

        int headerLength = _headerField.text.Length;
        int contentLength = _contentField.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;
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
