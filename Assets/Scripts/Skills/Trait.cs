using UnityEngine;
using UnityEngine.UI;

public class Trait : MonoBehaviour
{
    public ChangeText _text;
    public Image _image;

    private int _traitLevel = 0;
    private int _levelsToUnlock = 3;
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

    public bool CheckLevelUpStatus()
    {
        return _canLevelUp;
    }

    public void UnlockTrait()
    {
        _canLevelUp = true;
        GetComponent<TooltipTrigger>().SetColouredText(null, null);
        _image.color = Color.white;
    }

    public bool IsUnlocked()
    {
        return _canLevelUp;
    }

    public int GetLevelsToUnlock()
    {
        return _levelsToUnlock;
    }
}
