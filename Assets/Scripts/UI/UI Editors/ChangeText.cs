using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text _text;

    public string GetText()
    {
        return _text.text;
    }

    public void SetText(string value)
    {
        _text.text = value;
    }

    public void SetText(string value, Color color)
    {
        _text.text = value;
        _text.color = color;
    }
}
