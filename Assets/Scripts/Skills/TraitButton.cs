using UnityEngine;
using UnityEngine.UI;

public class TraitButton : MonoBehaviour
{
    public Button _button;
    private Image image;

    public void ButtonAvaliableTrue()
    {
        image = (Image)_button.targetGraphic;
        image.color = Color.white;
    }
}
