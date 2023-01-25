using UnityEngine;
using UnityEngine.UI;

public class Trait : MonoBehaviour
{
    public ChangeText _text;
    public Image _image;

    private int _traitLevel, _maxTraitLevel = 10;
    private bool _canLevelUp = false;

    public int GetLevel()
    {
        return _traitLevel;
    }

    // used for loading game
    public void SetLevel(int newLevel)
    {
        _traitLevel = newLevel;
        _text.SetText($"{_traitLevel}/{_maxTraitLevel}");
    }

    public void IncreaseLevel()
    {
        _traitLevel++;
        _text.SetText($"{_traitLevel}/{_maxTraitLevel}");
    }  

    public bool CheckLevelUpStatus()
    {
        return _canLevelUp;
    }

    public void UnlockTrait()
    {
        _canLevelUp = true;
        GetComponent<TooltipTrigger>().SetSubHeading(null, null);
        _image.color = Color.white;
    }
}
