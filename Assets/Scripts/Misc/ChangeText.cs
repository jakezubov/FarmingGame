using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text _text;

    public void SetText(string value)
    {
        _text.text = value;
    }

    public int GetNum()
    {
        return Convert.ToInt32(_text.text);
    }
}
