using UnityEngine;

public class Reputation : MonoBehaviour
{
    public RepEarner _queen;
    public RepEarner _wizard;
    public RepEarner _blacksmith;
    public RepEarner _carpenter;
    public RepEarner _knights;
    public RepEarner _merchants;
    public RepEarner _farmers;

    private readonly int _totalRep = 10000;
    private int _acquaintanceRepAmount;
    private int _friendsRepAmount;
    private int _closeFriendsRepAmount;

    private void Start()
    {
        _acquaintanceRepAmount = Mathf.RoundToInt(_totalRep * 1 / 4);
        _friendsRepAmount = Mathf.RoundToInt(_totalRep * 1 / 2);
        _closeFriendsRepAmount = Mathf.RoundToInt(_totalRep * 3 / 4);
    }

    public void GainRep(int rep, RepEarner repEarner)
    {
        repEarner.AddToCurrentRep(rep);
        if (repEarner.GetCurrentRep() > _totalRep) { repEarner.SetCurrentRep(_totalRep); }
        repEarner.SetSlider(repEarner.GetCurrentRep());

        switch (repEarner.GetRepTier())
        {
            case RepEarner.RepTier.Stranger: if (repEarner.GetCurrentRep() >= _acquaintanceRepAmount) { repEarner.SetRepTier(RepEarner.RepTier.Acquaintance); } break;
            case RepEarner.RepTier.Acquaintance: if (repEarner.GetCurrentRep() >= _friendsRepAmount) { repEarner.SetRepTier(RepEarner.RepTier.Friends); } break;
            case RepEarner.RepTier.Friends: if (repEarner.GetCurrentRep() >= _closeFriendsRepAmount) { repEarner.SetRepTier(RepEarner.RepTier.CloseFriends); } break;
            case RepEarner.RepTier.CloseFriends: break;
            default: repEarner.SetRepTier(RepEarner.RepTier.Stranger); break;
        }
    }

    public void FreeReputation()
    {
        int gainedRep = 1000;
        GainRep(gainedRep, _queen);
        GainRep(gainedRep, _wizard);
        GainRep(gainedRep, _blacksmith);
        GainRep(gainedRep, _carpenter);
        GainRep(gainedRep, _knights);
        GainRep(gainedRep, _merchants);
        GainRep(gainedRep, _farmers);
    }
}
