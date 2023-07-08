using UnityEngine;

public class ReputationHandler : MonoBehaviour
{
    public Reputation _queen, _wizard, _blacksmith, _carpenter, _knights, _merchants, _farmers;

    public void GainReputation(Rep rep, int amount)
    {
        // gain reputation for the specified individual/faction
        switch(rep)
        {
            case Rep.queen: _queen.AddToCurrentRep(amount); break;
            case Rep.wizard: _wizard.AddToCurrentRep(amount); break;
            case Rep.blacksmith: _blacksmith.AddToCurrentRep(amount); break;
            case Rep.carpenter: _carpenter.AddToCurrentRep(amount); break;
            case Rep.knights: _knights.AddToCurrentRep(amount); break;
            case Rep.merchants: _merchants.AddToCurrentRep(amount); break;
            case Rep.farmers: _farmers.AddToCurrentRep(amount); break;
        }
    }

    public void LoadAllReputation()
    {
        _queen.LoadRep(SaveData.queenRep);
        _wizard.LoadRep(SaveData.wizardRep);
        _blacksmith.LoadRep(SaveData.blacksmithRep);
        _carpenter.LoadRep(SaveData.carpenterRep);
        _knights.LoadRep(SaveData.knightsRep);
        _merchants.LoadRep(SaveData.merchantsRep);
        _farmers.LoadRep(SaveData.farmersRep);
    }

    public void SaveAllReputation()
    {
        SaveData.queenRep = _queen.GetCurrentRep();
        SaveData.wizardRep = _wizard.GetCurrentRep();
        SaveData.blacksmithRep = _blacksmith.GetCurrentRep();
        SaveData.carpenterRep = _carpenter.GetCurrentRep();
        SaveData.knightsRep = _knights.GetCurrentRep();
        SaveData.merchantsRep = _merchants.GetCurrentRep();
        SaveData.farmersRep = _farmers.GetCurrentRep();
    }

    public void FreeReputation()
    {
        // used for debugging purposes to quickly gain reputation
        int gainedRep = 1000;
        GainReputation(Rep.queen, gainedRep);
        GainReputation(Rep.wizard, gainedRep);
        GainReputation(Rep.blacksmith, gainedRep);
        GainReputation(Rep.carpenter, gainedRep);
        GainReputation(Rep.knights, gainedRep);
        GainReputation(Rep.merchants, gainedRep);
        GainReputation(Rep.farmers, gainedRep);
    }
}

public enum Rep
{ 
    queen,
    wizard,
    blacksmith,
    carpenter,
    knights,
    merchants,
    farmers
}

