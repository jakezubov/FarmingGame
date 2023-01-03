using UnityEngine;
using UnityEngine.UI;

public class TraitButton : MonoBehaviour
{
    public Button _button;

    public void ButtonAvaliableTrue()
    {
        Image image = (Image)_button.targetGraphic;
        image.color = Color.white;
    }
}
