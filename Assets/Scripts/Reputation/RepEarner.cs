using UnityEngine;

public class RepEarner : MonoBehaviour
{
    public ChangeSlider _slider;
    private RepTier _repTier;
    private int _currentRep;

    public RepEarner()
    {
        _currentRep = 0;
        _repTier = RepTier.Stranger;
    }

    public enum RepTier
    {
        Stranger,
        Acquaintance,
        Friends,
        CloseFriends
    };

    public int GetCurrentRep()
    {
        return _currentRep;
    }

    public void SetCurrentRep(int amount)
    {
        _currentRep = amount;
    }

    public void AddToCurrentRep(int amount)
    {
        _currentRep += amount;
    }

    public RepTier GetRepTier()
    {
        return _repTier;
    }

    public void SetRepTier(RepTier newTier)
    {
        _repTier = newTier;
    }

    public void SetSlider(int value)
    {
        _slider.SetValue(value);
    }
}
