using UnityEngine;
using UnityEngine.UI;

public class Trait : MonoBehaviour
{
    public ChangeText _text;
    public Image _image;

    private int _traitLevel = 0;
    private int _levelsToUnlock = 3;
    private int _modifierPerLevel = 0;
    private bool _canLevelUp = false;

    public int GetLevel()
    {
        return _traitLevel;
    }

    public void IncreaseLevel()
    {
        _traitLevel++;
        _text.SetText(_traitLevel.ToString());
        if (_levelsToUnlock > 0)
        {
            _levelsToUnlock--;
        }
    }

    public void MakeButtonAvaliable()
    {
        _image.color = Color.white;
    }

    public bool CheckLevelUpStatus()
    {
        return _canLevelUp;
    }

    public void SetLevelUpStatus(bool b)
    {
        _canLevelUp = b;
        if (b)
        {
            GetComponent<TooltipTrigger>().SetColouredText(null, null);
        }
    }

    public bool IsUnlocked()
    {
        return _canLevelUp;
    }

    public int GetLevelsToUnlock()
    {
        return _levelsToUnlock;
    }

    public int GetModifier()
    {
        return _modifierPerLevel;
    }

    public void SetModifier(int modifier)
    {
        _modifierPerLevel = modifier;
    }
}
